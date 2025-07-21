using Funaf.Domain.Module.Lancamentos.Dominio;
using Funaf.Domain.Module.Lancamentos.Persistencia;
using RemessaBB.FUNAF.Arquivo;
using RemessaBB.FUNAF.Domain;
using RemessaBB.FUNAF.Helper;
using RemessaBB.FUNAF.Persistence;
using System;
using System.IO;
using System.Linq;

namespace RemessaBB.FUNAF
{
    public static class Program
    {
        private static void Modelo()
        {
            try
            {
                string pathName = @"C:\INTEGRACAO\REMESSA\BB\ENVIO\5480";
                string fileName = string.Format("PGERN_MODELO_{0}.{1}.txt", DateTime.Now.ToString("ddMMyyyy"), DateTime.Now.ToString("mmss"));
                string file = Path.Combine(pathName, fileName);
                
                var remessa = new Domain.RemessaBB
                {
                    dtProcessamento = DateTime.Now,
                    txArquivo = fileName,
                    usuario = "000.000-0",
                    nuTipoRemessa = 1
                };

                DBRemessaBB.Save(remessa);
                
                var comando = new Comando { Id = 1, txComando = "" };

                var boletos = DBBoleto.GetAll();

                foreach (var iBoleto in boletos)
                {
                    var serventia = iBoleto.Declaracoes.FirstOrDefault().Serventia;

                    var novos = new RemessaBB_Boletos
                    {
                        Comando = comando,
                        Serventia = serventia,
                        RemessaBB = remessa,
                        nuDiasProtesto = 0,
                        Boleto = iBoleto,
                        dtRetorno = null,
                        nuCodRetorno = null
                    };

                    DBRemessaBoletoBB.Save(novos);
                }
            }
            catch (Exception ex)
            {
                var text = ex.Message;
            }
        }       

        public static string GerarArquivoParaEnvio(string diretorio)
        {
            var fileName = string.Empty;

            try
            {
                var remessa = DBRemessaBB.RecuperarProximaRemessa();
                var sequencial = remessa.Id;

                if (remessa == null)
                    throw new Exception("Remessa NULA");

                if (remessa != null && remessa.Id > 0)
                {
                    var boletos = DBRemessaBoletoBB.ListarParcelas(sequencial);

                    if (boletos.Count > 0)
                    {
                        fileName = string.Format("cbr641a.xproger1.{0}{1}.bco001", arg0: DateTime.Now.ToString("ddMMyyhhmmss"), arg1: sequencial.ToString("0000"));
                        fileName = diretorio + fileName;
                        string dadosProntos = Processar.Remessa(boletos, sequencial);
                        Util.GerarArquivo(dadosProntos, fileName);

                        DBRemessaBB.RegistrarEnvio(fileName, remessa);
                    }
                }
                else
                {
                    var log = new AppLog()
                    {
                        txMensagem = "RemessaBB.Program.GerarArquivoParaEnvio | Nenhum objeto do tipo EnvioBB encontrado!"
                    };

                    DBAppLog.Save(log);
                }
            }
            catch(Exception ex)
            {
                var log = new AppLog()
                {
                    txMensagem = "RemessaBB.Program.GerarArquivoParaEnvio" + ex.Message,
                    txStack = ex.StackTrace                    
                };

                DBAppLog.Save(log);
            }

            return fileName;
        }

        public static void Retorno(bool isPersistence)
        {
            try
            {
                string pathName = @"C:\INTEGRACAO\REMESSA\BB\RETORNO\5480";
                string fileName = string.Format("{0}.{1}.txt", DateTime.Now.ToString("ddMMyyyy"), DateTime.Now.ToString("HHmmss"));
                string file = Path.Combine(pathName, fileName);

                if (File.Exists(file))
                {
                    var DadosRetorno = Processar.Retorno(file);

                    if (isPersistence)
                    {
                        foreach (var detalhe in DadosRetorno.Detalhes)
                        {
                            RemessaBB_Retorno retorno = new RemessaBB_Retorno
                            {
                                Comando = DBRemessaComandoBB.GetById(Convert.ToInt32(detalhe.NuComando)),
                                nuCanalPagamentoUtilizado = Convert.ToInt32(detalhe.NuCanalPagamentoTitulo),
                                dtCredito = Convert.ToDateTime(detalhe.DtCredito),
                                dtVencimento = Convert.ToDateTime(detalhe.DtVencimento),
                                dtRetorno = DateTime.Now,
                                nuCodBancoRecebedor = Convert.ToInt32(detalhe.NuCodBancoRecebedora),
                                nuCodCobranca = Convert.ToInt32(detalhe.NuTipoCobranca),
                                nuCodRetorno = Convert.ToInt32(DadosRetorno.Header.NuSequenciaRetorno),
                                nuContaCaucao = Convert.ToInt32(detalhe.NuContaCaucao),
                                nuDiasCalculo = Convert.ToInt32(detalhe.NuDiasCalculo),
                                nuEspecieTitulo = Convert.ToInt32(detalhe.NuEspecieTitulo),
                                nuIndicativoAutorizacaoLiquidacao = Convert.ToInt32(detalhe.NuIndicativoAutoriazacaoLiquidacaoParcial),
                                nuIndicativoDebito = Convert.ToInt32(detalhe.NuIndicativoDebito),
                                nuIndicativoValor = Convert.ToInt32(detalhe.NuIndicativoValor),
                                nuNaturezaRecebimento = Convert.ToInt32(detalhe.NuNaturezaRecebimento),
                                nuPrefixoAgenciaRecebedora = Convert.ToInt32(detalhe.NuPrefixoAgenciaRecebedora),
                                txPrefixoTitulo = detalhe.TxPrefixoTitulo,
                                vaAjuste = Convert.ToDecimal(detalhe.VaAjuste),
                                vaTarifa = Convert.ToDecimal(detalhe.VaTarifa),
                                Boleto = DBBoleto.GetById(Convert.ToInt32(detalhe.NuControleParticipante)),
                            };

                            DBRemessaRetornoBB.Save(retorno);
                        }
                    }

                    Console.Write(DadosRetorno);
                    Console.ReadKey();
                }
                else
                {
                    var log = new AppLog()
                    {
                        txMensagem = "RemessaBB.Program.Retorno | O arquivo não foi encontrado!"
                    };

                    DBAppLog.Save(log);
                }
            }
            catch(Exception ex)
            {
                var log = new AppLog()
                {
                    txMensagem = "RemessaBB.Program.Retorno | EXCEPTION",
                    txStack = ex.StackTrace
                };

                DBAppLog.Save(log);
            }
        }

        private static string CriarArquivoDeRemessa()
        {
            var diretorio = string.Empty;
            int ano = DateTime.Now.Year;
            string mes = string.Format("{0:MMM}", DateTime.Now);

            try
            {
                var dirRemessa = Properties.Settings.Default.PathToWatch + "\\Arquivos Processados\\BB\\Remessa\\5480\\";

                diretorio = string.Format("{0}{1}\\{2}\\", dirRemessa, ano, mes);

                if (!Directory.Exists(diretorio))
                    Directory.CreateDirectory(diretorio);
            }
            catch (Exception)
            {
                /* 
                 * NÃO FAZER NADA. É APENAS UMA TENTATIVA DE EVITAR LOGS DESNECESSÁRIOS 
                 * NOS CASOS DE DUPLICAÇÃO DE ARQUIVOS DURANTE O REPROCESSAMENTO MANUAL 
                 * DE ARQUIVOS. 
                 */
            }

            return diretorio;
        }
    }
}