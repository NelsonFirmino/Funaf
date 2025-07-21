using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Globalization;
using RemessaBB.FUNAF.Domain;
using RemessaBB.FUNAF.Helper;
using Newtonsoft.Json;
using Funaf.Domain.Module.Lancamentos.Persistencia;

namespace RemessaBB.FUNAF.Arquivo
{
    public static class Processar
    {
        public static string Remessa(List<RemessaBB_Boletos> remessa, int numerosequencial)
        {
            var sequencialRegistro = 1;
            var arquivo = new StringBuilder();

            #region Definição do Header
            arquivo.Append("0"); // N1 - [01.0] Identificação do Registro Header FIXO=0
            arquivo.Append("1"); // N1 - [02.0] Tipo de Operação FIXO=1             
            arquivo.Append("REMESSA"); // X7 - [03.0] Identificação por extenso do Tipo de Operação REMESSA ou TESTE
            arquivo.Append("01"); // N2 - [04.0] Tipo de Serviço FIXO=01
            arquivo.Append("COBRANCA"); // X8 - [05.0] Identificação por Extenso Tipo de Serviço FIXO=COBRANCA
            arquivo.Append(Util.FormataCampo(" ", 7, ' ', "R")); // X7 - [06.0] Brancos
            arquivo.Append("3795"); // N4 - [07.0] Numero da Agencia
            arquivo.Append("8"); // X1 - [08.0] Digito Verificador do Prefixo da Agencia
            arquivo.Append("00005480"); // N8 - [09.0] Numero Conta Corrente
            arquivo.Append("1"); // X1 - [10.0] Digito Veriicador Conta Corrente
            arquivo.Append("000000"); // N6 - [11.0] Complemento do Registro FIXO=000000
            arquivo.Append(Util.FormataCampo("PROCURADORIA GERAL DO RN", 30, ' ', "R")); // X30 - [12.0] Nome do Cedente
            arquivo.Append(Util.FormataCampo("001BANCODOBRASIL", 18, ' ', "R")); // X18 - [13.0] FIXO=001BANCODOBRASIL
            arquivo.Append(DateTime.Now.ToString("ddMMyy")); // N6 - [14.0] Data da Gravação
            arquivo.Append(Util.FormataCampo(numerosequencial.ToString(), 7, '0', "L")); // N7 - [15.0] Sequencial da Remessa
            arquivo.Append(Util.FormataCampo(" ", 22, ' ', "R")); // X22 - [16.0] Complemento do Registro: Brancos
            arquivo.Append("2818310"); // N7 - [17.0] Convênio
            arquivo.Append(Util.FormataCampo(" ", 258, ' ', "R")); // X258 - [18.0] Complemento do Registro: Brancos
            arquivo.Append(Util.FormataCampo(sequencialRegistro.ToString(), 6, '0', "L")); // N6 - [19.0] Sequencial do Registro
            arquivo.Append("\n"); // Quebra de Linha
            #endregion

            #region Definição dos Detalhes
            sequencialRegistro++;

            foreach (var iRemessa in remessa)
            {
                iRemessa.Boleto.Declaracoes = DBDeclaracao.ListarPorBoleto(iRemessa.Boleto.Id);
                iRemessa.Boleto.txNossoNumero = DBBoleto.RegistrarBoleto(iRemessa.Boleto.Id);
                iRemessa.Serventia.Comarca = DBComarca.RecuperarPorServentia(iRemessa.Serventia.Id);

                var titulo = iRemessa.Boleto;
                decimal valorTitulo = titulo.vaDocumento;

                Console.Write(JsonConvert.SerializeObject(iRemessa.Serventia));

                #region Definição do Registro Detalhe da Remessa
                arquivo.Append("7"); // N1 - [01.7] Identificação do Registro FIXO=7
                arquivo.Append("02"); // N2 - [02.7] Tipo de Inscrição do Cedente 01:CPF | 02: CNPJ
                arquivo.Append("08286940000109"); // N14 - [03.7] Numero CPF ou CNPJ

                //  Dados da conta bancaria  //
                arquivo.Append("3795"); // N4 - [04.7]
                arquivo.Append("8"); // X1 - [05.7]
                arquivo.Append("00005480"); // N8 - [06.7]
                arquivo.Append("1"); // X1 - [07.7]
                arquivo.Append("2818310"); // N7 - [08.7]
                arquivo.Append(Util.FormataCampo(titulo.Id.ToString(), 25, ' ', "R")); // X25 - [09.7] Codigo Controle Empresa
                arquivo.Append("2818310" + Util.FormataCampo(titulo.txNossoNumero.ToString(), 10, '0', "L")); // N17 - [10.7] NossoNumero
                arquivo.Append("00"); // N2 - [11.7]
                arquivo.Append("00"); // N2 - [12.7]
                arquivo.Append(Util.FormataCampo(" ", 3, ' ', "R")); // X3 - [13.7]
                arquivo.Append(" "); // X1 - [14.7]
                arquivo.Append(Util.FormataCampo(" ", 3, ' ', "R")); // X3 - [15.7]
                arquivo.Append("019"); // N3 - [16.7]
                arquivo.Append("0"); // N1 - [17.7]
                arquivo.Append("000000"); // N6 - [18.7]
                arquivo.Append(Util.FormataCampo(" ", 5, ' ', "R")); // X5 - [19.7]
                arquivo.Append("17"); // N2 - [20.7]
                arquivo.Append(Util.FormataCampo("1", 2, '0', "L")); // N2 - [21.7]
                arquivo.Append(Util.FormataCampo(titulo.Id.ToString(), 10, ' ', "R")); // X10 - [22.7] // Código de identificação unico, tabela de remesssa
                arquivo.Append(iRemessa.dtVencimento.ToString("ddMMyy")); // N6 - [23.7] Data de Vencimento
                arquivo.Append(Util.FormataCampo(valorTitulo, 13, '0', "L", true)); // N11 - [24.7] Valor do Titulo
                arquivo.Append("001"); // N3 - [25.7] Numero Banco
                arquivo.Append("0000"); // N4 - [26.7]
                arquivo.Append(" "); // X1 - [27.7]
                arquivo.Append("26"); // N2 - [28.7] Tipo de Moeda
                arquivo.Append("N"); // X1 - [29.7]
                arquivo.Append(DateTime.Today.ToString("ddMMyy")); // N6 - [30.7] Data de Emissão
                arquivo.Append("00"); // N2 - [31.7]
                arquivo.Append("00"); // N2 - [32.7]
                arquivo.Append(Util.FormataCampo("0", 13, '0', "L", true)); // N11 - [33.7] Juros de Mora por dia de Atraso
                arquivo.Append(Util.FormataCampo("0", 6, '0', "L")); // N6 - [34.7]
                arquivo.Append(Util.FormataCampo("0", 13, '0', "L")); // N11 - [35.7]
                arquivo.Append(Util.FormataCampo("0", 13, '0', "L")); // N11 - [36.7]
                arquivo.Append(Util.FormataCampo("0", 13, '0', "L")); // N11 - [37.7]

                // Dados do Sacado
                var mensagem = " ";
                var contribuinte = iRemessa.Serventia;

                arquivo.Append(Util.FormataCampo("1", 2, '0', "L")); // N2 - [38.7]
                arquivo.Append(Util.FormataCampo(RemoverFormatacao(contribuinte.txCPF), 14, '0', "L")); // N14 - [39.7]
                arquivo.Append(Util.FormataCampo(RemoverFormatacao(contribuinte.txResponsavel), 37, ' ', "R")); // X37 - [40.7]
                arquivo.Append(Util.FormataCampo(" ", 3, ' ', "R")); // X3 - [41.7]
                arquivo.Append(Util.FormataCampo(RemoverFormatacao(contribuinte.txEndereco), 40, ' ', "R")); // X40 - [42.7]
                arquivo.Append(Util.FormataCampo(RemoverFormatacao(contribuinte.txBairro), 12, ' ', "R")); // X12 - [43.7]
                arquivo.Append(Util.FormataCampo(RemoverFormatacao(contribuinte.txCEP), 8, '0', "L")); // N8 - [44.7]
                arquivo.Append(Util.FormataCampo(RemoverFormatacao(contribuinte.Comarca.txComarca), 15, ' ', "R")); // X15 - [45.7]
                arquivo.Append(Util.FormataCampo("RN", 2, ' ', "R")); // X2 - [46.7]
                arquivo.Append(Util.FormataCampo(mensagem, 40, ' ', "R")); // X40 - [47.7]
                arquivo.Append("00"); // X2 - [48.7]
                arquivo.Append(" "); // X1 - [49.7]
                arquivo.Append(Util.FormataCampo(sequencialRegistro.ToString(), 6, '0', "L")); // N6 - [50.7]
                arquivo.Append("\n"); // Quebra de Linha
                #endregion
                sequencialRegistro++;
            }
            #endregion

            #region Definição do Trailer
            arquivo.Append("9"); // N1 - [01.9]
            arquivo.Append(Util.FormataCampo(" ", 393, ' ', "R")); // X393 - [02.9]
            arquivo.Append(Util.FormataCampo(sequencialRegistro.ToString(), 6, '0', "L")); // N6 - [03.9]
            #endregion

            return arquivo.ToString();
        }

        private static string RemoverFormatacao(string documento)
        {
            if (string.IsNullOrEmpty(documento))
                documento = "0";

            documento = documento.Replace("/", "");
            documento = documento.Replace(".", "");
            documento = documento.Replace("-", "");

            return documento.RemoveAccents().ToUpper();
        }

        public static Domain.Retorno.Arquivo Retorno(string file)
        {
            var Arquivo = new Domain.Retorno.Arquivo();
            var Header = new Domain.Retorno.Header();
            var Detalhes = new List<Domain.Retorno.Detalhe>();
            var Trailer = new Domain.Retorno.Trailler();

            using (var stream = new StreamReader(file))
            {
                string line;
                while ((line = stream.ReadLine()) != null)
                {
                    int nuTipoLinha = Convert.ToInt32(line.GetContent(0, 1));

                    if (nuTipoLinha == 0) // Header
                    {
                        Header.NuSequenciaRetorno = line.GetContent(100, 7);
                    }
                    else if (nuTipoLinha == 7) // Detalhe
                    {
                        Detalhes.Add(new Domain.Retorno.Detalhe
                        {
                            NuControleParticipante = line.GetContent(38, 25),
                            NuTipoCobranca = line.GetContent(80, 1),
                            NuDiasCalculo = line.GetContent(82, 4),
                            NuNaturezaRecebimento = line.GetContent(86, 2),
                            TxPrefixoTitulo = line.GetContent(88, 3),
                            NuContaCaucao = line.GetContent(94, 1),
                            NuComando = line.GetContent(108, 2),
                            DtVencimento = line.GetContent(146, 6),
                            NuCodBancoRecebedora = line.GetContent(165, 3),
                            NuPrefixoAgenciaRecebedora = line.GetContent(168, 4),
                            NuEspecieTitulo = line.GetContent(173, 2),
                            DtCredito = line.GetContent(175, 6),
                            VaTarifa = line.GetContent(181, 5),
                            NuIndicativoDebito = line.GetContent(318, 1),
                            NuIndicativoValor = line.GetContent(319, 1),
                            VaAjuste = line.GetContent(320, 10),
                            NuIndicativoAutoriazacaoLiquidacaoParcial = line.GetContent(390, 1),
                            NuCanalPagamentoTitulo = line.GetContent(392, 2),
                            NuSequencialRegistro = line.GetContent(364, 6),
                        });
                    }
                    else if (nuTipoLinha == 9) // Trailler
                    {
                        Trailer.NuCobrancaSimplesQtdeTitulos = line.GetContent(17, 8);
                        Trailer.VaCobrancaSimplesTotal = line.GetContent(25, 13);
                        Trailer.NuCobrancaSimplesNumeroAviso = line.GetContent(39, 8);
                    }
                }

                Arquivo.Header = Header;
                Arquivo.Detalhes = Detalhes;
                Arquivo.Trailler = Trailer;

                stream.Close();
            }

            return Arquivo;
        }

        public static string RemoveAccents(this string text)
        {
            StringBuilder sbReturn = new StringBuilder();
            var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();
            foreach (char letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    sbReturn.Append(letter);
            }
            return sbReturn.ToString();
        }

        private static string GetContent(this string content, int nuStart, int nuLength, string replaceIn = null, string replaceOut = null)
        {
            string str = string.Empty;

            if (content.Length >= (nuStart + nuLength))
                str = content.Substring(nuStart, nuLength);
            else
                str = content.Substring(nuStart, (content.Length - nuStart));

            if (replaceIn != null && replaceOut != null)
                str.Replace(replaceIn, replaceOut);

            return str;
        }
    }
}
