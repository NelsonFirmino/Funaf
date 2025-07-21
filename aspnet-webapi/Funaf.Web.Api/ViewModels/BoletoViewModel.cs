using System;
using System.IO;
using System.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Font = iTextSharp.text.Font;
using Funaf.Domain.Module.Lancamentos.Persistencia;
using Funaf.Domain.Module.Lancamentos.Dominio.Exceptions;
using Funaf.Domain.Module.Lancamentos.Dominio;
using System.Drawing;
using System.Globalization;
using Funaf.Service.Module.Cobranca.Dominio.Builders;
using Funaf.Service.Module.Cobranca.Service;
using Funaf.Service.Module.Cobranca.Dominio.Exceptions;

namespace Funaf.Web.Api.ViewModels
{
    public class BoletoViewModel
    {
        private const string AGENCIA = "3795-8";
        private const string CONTA_BANCARIA = "5480-1";
        private const int NUMERO_CONVENIO = 2818310;

        public static MemoryStream CriarBoleto(int idDeclaracao)
        {
            var boleto = DBBoleto.GerarBoleto(idDeclaracao);

            var fs = new MemoryStream();
            var document = new Document(PageSize.A4);
            var writer = PdfWriter.GetInstance(document, fs);

            var reader = new PdfReader(AppDomain.CurrentDomain.BaseDirectory + "\\App_LocalResources\\Modelo_BB.pdf");
            var importedPage = writer.GetImportedPage(reader, 1);
            document.Open();

            document.NewPage();

            if (boleto.Id > 0)
            {
                boleto.Declaracoes = DBDeclaracao.ListarPorBoleto(boleto.Id);

                if (boleto.Declaracoes == null || boleto.Declaracoes.Count == 0)
                    throw new FunafAPIException("Boleto sem declarações vinculadas, entre em contado com o administrador do sistema.");

                var periodo = "";
                                
                foreach (var iDeclaracao in boleto.Declaracoes)
                {
                    periodo = string.Format("Ref. à DPSE no periodo de {0} até {1}\n", iDeclaracao.dtPeriodoInicial.ToShortDateString(), iDeclaracao.dtPeriodoFinal.ToShortDateString());

                    if (iDeclaracao.Boleto.vaMulta > 0)
                        periodo += string.Format("\nValor do Recolhimento: {0:C} + Valor da Multa: {1:C} = Valor do documento {2:C}", iDeclaracao.Boleto.vaPrincipal, iDeclaracao.Boleto.vaMulta, iDeclaracao.Boleto.vaDocumento);

                }
                
                var Titulo = new TituloBuilder()
                        .WithNumeroConvenio(NUMERO_CONVENIO)
                        .WithDataEmissaoTitulo(DateTime.Today)
                        .WithDataVencimentoTitulo(boleto.dtVencimento)
                        .WithValorOriginalTitulo(boleto.vaDocumento)
                        .WithCodigoTipoTitulo(4)
                        .WithTexoDescricaoTipoTitulo("Declaração do FUNAF")
                        .WithIdBoleto(boleto.Id)
                        .WithTextoNumeroTituloCliente(boleto.idRegistroBBWSBoleto)
                        .WithTextoMensagemBloquetoOcorrencia(periodo)
                        .Build();

                var serventia = DBServentia.GetById(boleto.Declaracoes[0].Serventia.Id);

                var comarca = DBComarca.RecuperarPorServentia(serventia.Id);

                var Pagador = new PagadorBuilder()
                            .WithNumeroInscricaoPagador(serventia.txCPF)
                            .WithNomePagador(serventia.txResponsavel)
                            .WithTextoEnderecoPagador(serventia.txEndereco)
                            .WithNumeroCepPagador(serventia.txCEP)
                            .WithNomeMunicipioPagador(comarca.txComarca)
                            .WithNomeBairroPagador(serventia.txBairro)
                            .WithSiglaUfPagador("RN")
                            .Build();

                CodigoDeBarrasBoleto codigoDeBarras;

                try
                {
                    codigoDeBarras = RegistrarBoletoService.Invoke(Titulo, Pagador);

                    DBBoleto.AtualizarNossoNumero(boleto.idRegistroBBWSBoleto, Titulo.TextoNumeroTituloCliente);

                    boleto.txNossoNumero = Titulo.TextoNumeroTituloCliente;
                }
                catch (CobrancaModuleException ex)
                {
                    throw new FunafAPIException("O sistema não conseguiu efetuar o registro do boleto, tente mais tarde. Caso persista entre em contado com o administrador do sistema." + ex.Message, ex);
                }

                AdicionarCodigoBarras(document, codigoDeBarras);

                writer.DirectContent.AddTemplate(importedPage, 0, 0);

                int linha = 0, margemEsq = 36, margemDir = 560;
                
                float fontPadrao = 8.5f;

                /*   R E C I B O   D O   S A C A D O   
                 = = = = = =   = =   = = = = = = */
                Escrever(_linhaDigitavel, writer, 11.5f, true, margemDir, 713, Element.ALIGN_RIGHT);

                var dadosContaBancaria = string.Format("{0} / {1}", AGENCIA, CONTA_BANCARIA);
                var nossonumero = string.Format("{0}", boleto.txNossoNumero.TrimStart('0'));
                var dadosDaServentia = string.Format("Responsável: {0} ({1})", serventia.txResponsavel, serventia.txCPF);

                // Linha 1
                linha = 690;
                Escrever("PROCURADORIA GERAL DO ESTADO - RN", writer, fontPadrao, margemEsq, linha);
                Escrever(dadosContaBancaria, writer, fontPadrao, 296, linha);
                Escrever("R$", writer, fontPadrao, 389, linha);
                Escrever("1", writer, fontPadrao, 431, linha);
                Escrever(nossonumero, writer, fontPadrao, margemDir, linha, Element.ALIGN_RIGHT);

                // Linha 2
                linha = 668;
                Escrever(boleto.Sequencial.ToString(CultureInfo.InvariantCulture), writer, fontPadrao, margemEsq, linha);
                Escrever("08.286.940/0001-09", writer, fontPadrao, 240, linha);
                Escrever(boleto.dtVencimento.ToShortDateString(), writer, fontPadrao, true, 332, linha, Element.ALIGN_LEFT);
                Escrever(boleto.vaDocumento.ToString("N"), writer, fontPadrao, true, margemDir, linha, Element.ALIGN_RIGHT);

                // Linha 3
                linha = 645;
                Escrever("", writer, fontPadrao, margemEsq + 264, linha);
                Escrever(boleto.vaDocumento.ToString("N"), writer, fontPadrao, margemDir, linha, Element.ALIGN_RIGHT);

                // Linha 4
                linha = 625;
                Escrever(serventia.txCartorio, writer, fontPadrao, margemEsq, linha);

                linha = 599;
                Escrever(dadosDaServentia, writer, fontPadrao, margemEsq, linha);

                // Linha 5 - Instruções

                /* *   F I C H A   D E   C O M P E N S A Ç Ã O    
                       = = = = =   = =   = = = = = = = = = = =   * */
                Escrever(_linhaDigitavel, writer, 11.5f, true, margemDir, 479, Element.ALIGN_RIGHT);

                // Linha 1
                linha = 455;
                Escrever("PAGÁVEL NA REDE BANCÁRIA ATÉ O VENCIMENTO", writer, fontPadrao, margemEsq, linha);
                Escrever(boleto.dtVencimento.ToShortDateString(), writer, fontPadrao, true, margemDir, linha, Element.ALIGN_RIGHT);

                // Linha 2
                linha = 432;
                Escrever("PROCURADORIA GERAL DO ESTADO - RN", writer, fontPadrao, margemEsq, linha);
                Escrever(dadosContaBancaria, writer, fontPadrao, margemDir, linha, Element.ALIGN_RIGHT);

                // Linha 3
                linha = 410;
                Escrever(DateTime.Now.ToShortDateString(), writer, fontPadrao, margemEsq, linha);
                Escrever(boleto.Sequencial.ToString(CultureInfo.InvariantCulture), writer, fontPadrao, 168, linha);
                Escrever("DAE", writer, fontPadrao, 290, linha);
                Escrever("N", writer, fontPadrao, 340, linha);
                Escrever(DateTime.Now.ToShortDateString(), writer, fontPadrao, 374, linha);
                Escrever(nossonumero, writer, fontPadrao, margemDir, linha, Element.ALIGN_RIGHT);

                // Linha 4
                linha = 387;
                Escrever("017", writer, fontPadrao, 168, linha);
                Escrever("R$", writer, fontPadrao, 234, linha);
                Escrever(boleto.vaDocumento.ToString("N"), writer, fontPadrao, true, margemDir, linha, Element.ALIGN_RIGHT);

                // Multa e Juros
                Escrever("", writer, fontPadrao, false, margemDir, 320, Element.ALIGN_RIGHT);

                // Valor Cobrado
                Escrever(boleto.vaDocumento.ToString("N"), writer, fontPadrao, true, margemDir, 274, Element.ALIGN_RIGHT);

                // Instruções e Sacado
                EscreverInstrucoes(document, boleto);

                //boletosParaRegistrar.Add(boleto);
            }

            document.Close();
            reader.Close();

            return fs;
        }

        #region Métodos estáticos de Construção dos boletos

        private static string _linhaDigitavel;

        private static void Escrever(string texto, PdfWriter writer, float fontSize, float x, float y, int alinhamento)
        {
            Escrever(texto, writer, fontSize, false, x, y, alinhamento);
        }

        private static void Escrever(string texto, PdfWriter writer, float fontSize, float x, float y)
        {
            Escrever(texto, writer, fontSize, false, x, y, Element.ALIGN_LEFT);
        }

        private static void Escrever(string texto, PdfWriter writer, float fontSize, bool negrito, float x, float y, int alinhamento)
        {
            BaseFont bf = BaseFont.CreateFont(negrito ? BaseFont.HELVETICA_BOLD : BaseFont.HELVETICA, BaseFont.WINANSI, true);
            Phrase phrase = new Phrase(texto, new Font(bf));
            phrase.Font.Size = fontSize;

            ColumnText.ShowTextAligned(writer.DirectContent, alinhamento, phrase, x, y, 0);
        }

        private static void EscreverInstrucoes(Document document, Boleto boleto)
        {
            // O boleto não pode ser emitido para serventias distintas
            var serventia = boleto.Declaracoes.FirstOrDefault().Serventia;
            var dadosDaServentia = string.Format("Responsável: {0} ({1})", serventia.txResponsavel, serventia.txCPF);

            PdfPTable tableInstrucoes = new PdfPTable(1);
            /* COMMENT TO DESIGN */
            tableInstrucoes.DefaultCell.BorderWidth = 0;
            tableInstrucoes.WidthPercentage = 101;
            tableInstrucoes.DefaultCell.MinimumHeight = 195;
            tableInstrucoes.AddCell("");
            tableInstrucoes.DefaultCell.MinimumHeight = 88;

            // Parágrafo com sacado e dados do processo e parcela
            Paragraph paragSacado = new Paragraph();
            paragSacado.Font = new Font(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.WINANSI, true));
            paragSacado.Font.Size = 8.5f;

            paragSacado.Add(Chunk.NEWLINE);
            paragSacado.Add(new Chunk(serventia.txCartorio, new Font(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.WINANSI, true))));
            paragSacado.Add(Chunk.NEWLINE);
            paragSacado.Add(new Chunk(dadosDaServentia, new Font(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.WINANSI, true))));

            paragSacado.Add(Chunk.NEWLINE);
            paragSacado.Font.Size = 7f;

            paragSacado.Add(Chunk.NEWLINE);
            paragSacado.Add(Chunk.NEWLINE);

            var periodo = "";

            foreach (var iDeclaracao in boleto.Declaracoes)
            {
                Paragraph paragInstrucao = new Paragraph();
                paragInstrucao.Font = new Font(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.WINANSI, true));
                paragInstrucao.Font.Size = 8.5f;
                paragInstrucao.Add(Chunk.NEWLINE);
                paragInstrucao.Add(Chunk.NEWLINE);
                periodo += string.Format("\nRef. à DPSE no periodo de {0} até {1}", iDeclaracao.dtPeriodoInicial.ToShortDateString(), iDeclaracao.dtPeriodoFinal.ToShortDateString());

                if (iDeclaracao.Boleto.vaMulta > 0)
                    periodo += string.Format("\nValor do Recolhimento: {0:C} + Valor da Multa: {1:C} = Valor do documento {2:C}", iDeclaracao.Boleto.vaPrincipal, iDeclaracao.Boleto.vaMulta, iDeclaracao.Boleto.vaDocumento);

                paragInstrucao.Add(new Chunk(periodo, new Font(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.WINANSI, true))));
                paragInstrucao.Add(Chunk.NEWLINE);
                tableInstrucoes.AddCell(paragInstrucao);
            }

            tableInstrucoes.DefaultCell.MinimumHeight = 152;
            tableInstrucoes.AddCell("");
            tableInstrucoes.DefaultCell.MinimumHeight = 100;

            var parag = new Paragraph
            {
                IndentationRight = 120,
                Leading = 10.5f,
                Font = new Font(BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.WINANSI, true))
                {
                    Size = 8.5f
                }
            };

            parag.Add(new Chunk("Não conceder qualquer desconto ou abatimento."));

            parag.Add(Chunk.NEWLINE);
            parag.Add(Chunk.NEWLINE);

            parag.Add(new Chunk("Não receber/pagar após o vencimento, pois o pagamento em atraso está sujeito"
                , new Font(BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.WINANSI, true))));
            parag.Add(Chunk.NEWLINE);
            parag.Add(new Chunk("a juros de mora e multa de atraso"
                , new Font(BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.WINANSI, true))));
            parag.Add(Chunk.NEWLINE);
            parag.Add(Chunk.NEWLINE);

            parag.Add(new Chunk("Após o vencimento, emitir novo boleto no site da PGE, através da área restrita do Notários", new Font(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.WINANSI, true))));

            parag.Add(Chunk.NEWLINE);

            parag.Add(new Chunk(periodo, new Font(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.WINANSI, true))));

            //parag.Add(paragSacado);            

            tableInstrucoes.AddCell(parag);

            tableInstrucoes.DefaultCell.MinimumHeight = 0;
            tableInstrucoes.AddCell(paragSacado);

            document.Add(tableInstrucoes);
        }

        private static void AdicionarCodigoBarras(Document document, CodigoDeBarrasBoleto codigoDeBarras)
        {
            // Comprimento total igual a 103mm e altura total igual a 13mm
            Barcode barcode = new BarcodeInter25();
            barcode.StartStopText = true;
            barcode.BarHeight = 40.6f;
            barcode.Code = codigoDeBarras.CodigoBarraNumerico.Trim();

            if (barcode.Code.Length != 44)
                throw new FunafAPIException("O código de barras está fora de conformidade. ");

            _linhaDigitavel = codigoDeBarras.LinhaDigitavel.Trim();

            _linhaDigitavel = string.Format("{0}.{1} {2}.{3} {4}.{5} {6} {7}",
                _linhaDigitavel.Substring(0, 5), _linhaDigitavel.Substring(5, 5),
                _linhaDigitavel.Substring(10, 5), _linhaDigitavel.Substring(15, 6),
                _linhaDigitavel.Substring(21, 5), _linhaDigitavel.Substring(26, 6),
                _linhaDigitavel.Substring(32, 1), 
                _linhaDigitavel.Substring(33));

            var img = barcode.CreateDrawingImage(Color.Black, Color.Transparent);
            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(img, BaseColor.WHITE);
            jpg.SetAbsolutePosition(35, 145);

            document.Add(jpg);
        }

        #endregion
    }
}