using Funaf.Domain.Module.Lancamentos.Dominio;
using Funaf.Domain.Module.Lancamentos.Persistencia;
using Newtonsoft.Json;
using RemessaBB.FUNAF.Arquivo;
using RemessaBB.FUNAF.Helper;
using RemessaBB.FUNAF.Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemessaBB.FUNAF
{
    public class TesteRemessa
    {

        public void Iniciar()
        {
            var diretorio5480 = CriarArquivoDeRemessa();

            var arquivo5480 = GerarArquivoParaEnvio(diretorio5480);
        }

        private string CriarArquivoDeRemessa()
        {
            var diretorio = string.Empty;
            int ano = DateTime.Now.Year;
            string mes = string.Format("{0:MMM}", DateTime.Now);

            try
            {
                var dirRemessa = Properties.Settings.Default.PathToWatch + "\\Arquivos Processados\\BB\\Remessa\\";

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

        private string GerarArquivoParaEnvio(string diretorio)
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
            catch (Exception ex)
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
    }
}
