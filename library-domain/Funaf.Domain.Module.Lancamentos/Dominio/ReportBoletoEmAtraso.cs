using DBBroker.Mapping;
using Funaf.Domain.Module.Lancamentos.Helper;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MoreLinq;


namespace Funaf.Domain.Module.Lancamentos.Dominio
{
    [DBMappedClass(Table = "spRelBoletoEmAtraso", PrimaryKey = "idDados")]
    public class ReportBoletoEmAtraso
    {
        public string txComarca { get; set; }
        public string txCartorio { get; set; }
        public string txSituacao { get; set; }
        public decimal vaPrincipal { get; set; }
        public decimal vaJuros { get; set; }
        public decimal vaMulta { get; set; }
        public decimal vaDocumento { get; set; }
        public string dtPeriodoInicial { get; set; }
        public string dtPeriodoFinal { get; set; }
        public string txTelefone { get; set; }

        public static string CriarPDF(List<ReportBoletoEmAtraso> dados, string subTitulo)
        {
            var ms = new MemoryStream();
            var cinzaClaro = new BaseColor(230, 230, 230);

            var FonteTrebuchet = BaseFont.CreateFont(AppDomain.CurrentDomain.BaseDirectory + "\\RESOURCES\\Fonts\\Trebuchet\\trebuc.ttf", BaseFont.WINANSI, true);

            Font FontNegritoSecao = new Font(FonteTrebuchet, 10, Font.BOLD);
            Font FontNegritoHeader = new Font(FonteTrebuchet, 9, Font.BOLD);
            Font FontNormal = new Font(FonteTrebuchet, 8, Font.NORMAL);

            var doc = new Document(PageSize.A4.Rotate());
            doc.AddCreationDate();

            var writer = PdfWriter.GetInstance(doc, ms);
            writer.SetBoxSize("art", doc.PageSize);
            writer.PageEvent = new HeaderAndFooter("BOLETOS DO FUNAF EM ATRASO", subTitulo, " ");
            doc.Open();

            doc.Add(Chunk.NEWLINE);

            PdfPTable tabela = new PdfPTable(8) { WidthPercentage = 100 };
            tabela.SpacingBefore = 8;
            tabela.SpacingAfter = 8;
            tabela.SetTotalWidth(new float[] { 18,30,10,15, 10, 7, 7,10 });

            tabela.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabela.DefaultCell.BorderWidth = 0;
            tabela.DefaultCell.MinimumHeight = 20f;
            tabela.DefaultCell.PaddingLeft = 5;
            tabela.DefaultCell.PaddingRight = 5;

            var contador = 0;

            tabela.DefaultCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            //tabela.DefaultCell.BorderWidth = 1;
            tabela.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
            tabela.DefaultCell.Colspan = 0;
            tabela.DefaultCell.BorderWidthBottom = 0;
            tabela.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
            tabela.AddCell(new Phrase("COMARCA", FontNegritoHeader));
            tabela.AddCell(new Phrase("CARTÓRIO", FontNegritoHeader));
            tabela.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            tabela.AddCell(new Phrase("TELEFONE", FontNegritoHeader));
            tabela.AddCell(new Phrase("PERÍODO", FontNegritoHeader));
            tabela.AddCell(new Phrase("VR DECLARADO (R$)", FontNegritoHeader));
            tabela.AddCell(new Phrase("JUROS (R$)", FontNegritoHeader));
            tabela.AddCell(new Phrase("MULTA (R$)", FontNegritoHeader));
            tabela.AddCell(new Phrase("VR A PAGAR (R$)", FontNegritoHeader));
      
            var totPrincipal = 0m;
            var totJuros = 0m;
            var totMulta = 0m;
            var totDocumento = 0m;
            var subPrincipal = 0m;
            var subJuros = 0m;
            var subMulta = 0m;
            var subDocumento = 0m;
            var cartorios = dados.DistinctBy(e => e.txCartorio).ToList();

            //(e => e.txCartorio).ToList();

            foreach (var iCartorio in cartorios)
            {
                foreach (var iDado in dados.FindAll(e => e.txCartorio.Equals(iCartorio.txCartorio)))
                {
                    tabela.DefaultCell.BackgroundColor = (contador % 2 == 0) ? cinzaClaro : BaseColor.WHITE;

                    tabela.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                    tabela.AddCell(new Phrase(iDado.txComarca.ToUpper(), FontNormal));
                    tabela.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                    tabela.AddCell(new Phrase(iDado.txCartorio, FontNormal));
                    tabela.AddCell(new Phrase(iDado.txTelefone, FontNormal));
                    tabela.AddCell(new Phrase(iDado.dtPeriodoInicial + "-" + iDado.dtPeriodoFinal, FontNormal));

                    tabela.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tabela.AddCell(new Phrase(iDado.vaPrincipal.ToString("N2"), FontNormal));
                    tabela.AddCell(new Phrase(iDado.vaJuros.ToString("N2"), FontNormal));
                    tabela.AddCell(new Phrase(iDado.vaMulta.ToString("N2"), FontNormal));
                    tabela.AddCell(new Phrase(iDado.vaDocumento.ToString("N2"), FontNormal));
                    contador++;
                    totPrincipal += iDado.vaPrincipal;
                    totJuros += iDado.vaJuros;
                    totMulta += iDado.vaMulta;
                    totDocumento += iDado.vaDocumento;

                    subPrincipal += iDado.vaPrincipal;
                    subJuros += iDado.vaJuros;
                    subMulta += iDado.vaMulta;
                    subDocumento += iDado.vaDocumento;

                }
                tabela.DefaultCell.BackgroundColor = BaseColor.LIGHT_GRAY;

                tabela.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                tabela.AddCell(new Phrase("SUBTOTAL(R$)", FontNegritoHeader));
                tabela.AddCell(new Phrase("", FontNegritoHeader));
                tabela.AddCell(new Phrase("", FontNegritoHeader));
                tabela.AddCell(new Phrase("", FontNegritoHeader));
                tabela.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                tabela.AddCell(new Phrase(subPrincipal.ToString("N2"), FontNegritoHeader));
                tabela.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                tabela.AddCell(new Phrase(subJuros.ToString("N2"), FontNegritoHeader));
                tabela.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                tabela.AddCell(new Phrase(subMulta.ToString("N2"), FontNegritoHeader));
                tabela.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                tabela.AddCell(new Phrase(subDocumento.ToString("N2"), FontNegritoHeader));
                subPrincipal = 0m;
                subJuros = 0m;
                subMulta = 0m;
                subDocumento = 0m;
            }
            tabela.DefaultCell.BackgroundColor = BaseColor.LIGHT_GRAY;

            tabela.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
            tabela.AddCell(new Phrase("TOTAL(R$)", FontNegritoHeader));
            tabela.AddCell(new Phrase("", FontNegritoHeader));
            tabela.AddCell(new Phrase("", FontNegritoHeader));
            tabela.AddCell(new Phrase("", FontNegritoHeader));
            tabela.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            tabela.AddCell(new Phrase(totPrincipal.ToString("N2"), FontNegritoHeader));
            tabela.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            tabela.AddCell(new Phrase(totJuros.ToString("N2"), FontNegritoHeader));
            tabela.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            tabela.AddCell(new Phrase(totMulta.ToString("N2"), FontNegritoHeader));
            tabela.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            tabela.AddCell(new Phrase(totDocumento.ToString("N2"), FontNegritoHeader));
            doc.Add(tabela);

            doc.Close();

            var pdfBase64 = Convert.ToBase64String(ms.ToArray());

            return pdfBase64;
        }
   

    }
}
