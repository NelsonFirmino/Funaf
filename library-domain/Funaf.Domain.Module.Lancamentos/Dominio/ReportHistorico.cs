using DBBroker.Mapping;
using Funaf.Domain.Module.Lancamentos.Helper;
using iTextSharp.text;
using iTextSharp.text.pdf;
//using library_domain.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Funaf.Domain.Module.Lancamentos.Dominio
{
    [DBMappedClass(Table = "ReportHistorico", PrimaryKey = "idDados")]
    public class ReportHistorico
    {
        public int idDeclaracao { get; set; }
        public string txComarca { get; set; }
        public string txCartorio { get; set; }
        public string dtPeriodoInicial { get; set; }
        public string dtPeriodoFinal { get; set; }
        public string dtLiquidacao { get; set; }
        public decimal vaPrincipal { get; set; }
        public decimal vaMulta { get; set; }
        public decimal vaJuros { get; set; }
        public decimal vaPago { get; set; }

        public static string CriarPDF(List<ReportHistorico> dados, string subTitulo)
        {
            var ms = new MemoryStream();
            var cinzaClaro = new BaseColor(230, 230, 230);

            var FonteTrebuchet = BaseFont.CreateFont(AppDomain.CurrentDomain.BaseDirectory + "\\RESOURCES\\Fonts\\Trebuchet\\trebuc.ttf", BaseFont.WINANSI, true);

            Font FontNegritoSecao = new Font(FonteTrebuchet, 10, Font.BOLD);
            Font FontNegritoHeader = new Font(FonteTrebuchet, 9, Font.BOLD);
            Font FontNormal = new Font(FonteTrebuchet, 8, Font.NORMAL);

            var doc = new Document(PageSize.A4);
            doc.AddCreationDate();

            var writer = PdfWriter.GetInstance(doc, ms);
            writer.SetBoxSize("art", doc.PageSize);
            writer.PageEvent = new HeaderAndFooter("RELATÓRIO HISTÓRICO DE DECLARAÇÕES  ", subTitulo, " ");
            doc.Open();

            doc.Add(Chunk.NEWLINE);

            PdfPTable tabela = new PdfPTable(7) { WidthPercentage = 100 };
            tabela.SpacingBefore = 20;
            tabela.SpacingAfter = 20;
            tabela.SetTotalWidth(new float[] { 25,20,15,15,20,20,15 });

            tabela.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabela.DefaultCell.BorderWidth = 0;
            tabela.DefaultCell.MinimumHeight = 20f;
            tabela.DefaultCell.PaddingLeft = 4;
            tabela.DefaultCell.PaddingRight = 4;

            var contador = 0;
            var vaPrincipal = 0m;
            var vaMulta = 0m;
            var vaJuros = 0m;
            var vaPago = 0m;
            var vaDevido = 0m;
            var devido = 0m;

            tabela.DefaultCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            //tabela.DefaultCell.BorderWidth = 1;
            tabela.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
            tabela.DefaultCell.Colspan = 0;
            tabela.DefaultCell.BorderWidthBottom = 0;
            tabela.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
            tabela.AddCell(new Phrase("PERÍODO", FontNegritoHeader));
            tabela.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            tabela.AddCell(new Phrase("VR DECLARADO", FontNegritoHeader));
            tabela.AddCell(new Phrase("vR MULTA", FontNegritoHeader));
            tabela.AddCell(new Phrase("VR JUROS", FontNegritoHeader));
            tabela.AddCell(new Phrase("VR PAGO", FontNegritoHeader));
            tabela.AddCell(new Phrase("LIQUIDAÇÃO", FontNegritoHeader));
            tabela.AddCell(new Phrase("VR DEVIDO", FontNegritoHeader));

            foreach (var iDado in dados)
            {
                devido = iDado.vaPrincipal + iDado.vaMulta + iDado.vaJuros - iDado.vaPago;
                tabela.DefaultCell.BackgroundColor = (contador % 2 == 0) ? cinzaClaro : BaseColor.WHITE;
                tabela.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                tabela.AddCell(new Phrase(iDado.dtPeriodoInicial + " - " + iDado.dtPeriodoFinal, FontNormal));
                tabela.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                tabela.AddCell(new Phrase(iDado.vaPrincipal.ToString("N2"), FontNormal));
                tabela.AddCell(new Phrase(iDado.vaMulta.ToString("N2"), FontNormal));
                tabela.AddCell(new Phrase(iDado.vaJuros.ToString("N2"), FontNormal));
                tabela.AddCell(new Phrase(iDado.vaPago.ToString("N2"), FontNormal));
                tabela.AddCell(new Phrase(iDado.dtLiquidacao, FontNormal));
                tabela.AddCell(new Phrase(devido.ToString("N2"), FontNormal));
                contador++;
                vaPrincipal += iDado.vaPrincipal;
                vaMulta += iDado.vaMulta;
                vaJuros += iDado.vaJuros;
                vaPago += iDado.vaPago;
                vaDevido += devido;
            }
            tabela.DefaultCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            tabela.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
            tabela.AddCell(new Phrase("Total", FontNegritoHeader));
            tabela.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            tabela.DefaultCell.Colspan = 0;
            tabela.AddCell(new Phrase(vaPrincipal.ToString("N2"), FontNormal));
            tabela.AddCell(new Phrase(vaMulta.ToString("N2"), FontNormal));
            tabela.AddCell(new Phrase(vaJuros.ToString("N2"), FontNormal));
            tabela.AddCell(new Phrase(vaPago.ToString("N2"), FontNormal));
            tabela.AddCell(new Phrase("-", FontNegritoHeader));
            tabela.AddCell(new Phrase(vaDevido.ToString("N2"), FontNormal));
            doc.Add(tabela);

            doc.Close();

            var pdfBase64 = Convert.ToBase64String(ms.ToArray());

            return pdfBase64;
        }
   

    }
}
