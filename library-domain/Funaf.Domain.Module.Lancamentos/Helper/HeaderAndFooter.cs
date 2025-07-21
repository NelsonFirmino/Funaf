using iTextSharp.text;
using iTextSharp.text.pdf;
using System;

namespace Funaf.Domain.Module.Lancamentos.Helper
{
   public class HeaderAndFooter : PdfPageEventHelper
    {
        private string RESOURCES = AppDomain.CurrentDomain.BaseDirectory + "\\RESOURCES";

        public Image Image { get; private set; }
        public string Titulo { get; private set; }
        public string SubTitulo { get; private set; }
        public string Sistema { get; private set; }
        public string Local { get; private set; }

        private static Font FonteRodapeNormal = new Font(BaseFont.CreateFont(AppDomain.CurrentDomain.BaseDirectory + "\\RESOURCES\\Fonts\\Trebuchet\\trebuc.ttf", BaseFont.WINANSI, true), 8, Font.NORMAL);

        public HeaderAndFooter(Image image, string title = "", string subTitle = "", string sistema = "", string local = "")
        : base()
        {
            Image = image;
            Titulo = title;
            SubTitulo = subTitle;
            Sistema = sistema;
            Local = local;
        }

        public HeaderAndFooter(string title = "", string subTitle = "", string sistema = "", string local = "")
        : base()
        {
            Titulo = title;
            SubTitulo = subTitle;
            Sistema = sistema;
            Local = local;
        }

        public override void OnStartPage(PdfWriter writer, Document document)
        {
            var FonteTrebuchet = BaseFont.CreateFont(RESOURCES + "\\Fonts\\Trebuchet\\trebuc.ttf", BaseFont.WINANSI, true);
            var tblBrasao = new PdfPTable(1);
            tblBrasao.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            tblBrasao.DefaultCell.BorderWidth = 0;
            tblBrasao.SpacingBefore = 10;
            tblBrasao.SpacingAfter = 10;
            tblBrasao.WidthPercentage = 30;
            tblBrasao.AddCell(Image.GetInstance(RESOURCES + "\\Images\\logo_vertical_alterada.png"));

            var tblTitulos = new PdfPTable(1) { WidthPercentage = 100 };

            var pTitulo = new Paragraph(Titulo, new Font(FonteTrebuchet, 13, Font.BOLD));
            pTitulo.Alignment = Element.ALIGN_CENTER;

            var pSubTitulo = new Paragraph(SubTitulo, new Font(FonteTrebuchet, 9, Font.BOLDITALIC));
            pSubTitulo.Alignment = Element.ALIGN_CENTER;

            var cell1 = new PdfPCell(pTitulo);
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;
            cell1.BorderColor = BaseColor.WHITE;
            tblTitulos.AddCell(cell1);

            var cell2 = new PdfPCell(pSubTitulo);
            cell2.HorizontalAlignment = Element.ALIGN_CENTER;
            cell2.BorderColor = BaseColor.WHITE;
            cell2.PaddingBottom = 20;
            tblTitulos.AddCell(cell2);

           if (writer.PageNumber == 1)
            document.Add(tblBrasao);

            document.Add(tblTitulos);
        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            base.OnEndPage(writer, document);
            PdfPTable tabFot = new PdfPTable(new float[] { 1F });
            PdfPCell cell;
            tabFot.TotalWidth = 100F;
            cell = new PdfPCell(new Phrase("Data emissão: " + DateTime.Today.ToString("dd/MM/yyy"), FonteRodapeNormal));
            cell.Border = 0;
            tabFot.AddCell(cell);
            tabFot.WriteSelectedRows(0, -1, document.Left, document.Bottom, writer.DirectContent);

            PdfPTable tabFotSistema = new PdfPTable(new float[] { 1F });
            PdfPCell cellSistema;
            tabFotSistema.TotalWidth = 100F;
            cellSistema = new PdfPCell(new Phrase(Sistema, FonteRodapeNormal));
            cellSistema.Border = 0;
            tabFotSistema.AddCell(cellSistema);
            tabFotSistema.DefaultCell.Border = 0;
            tabFotSistema.WriteSelectedRows(0, -1, document.PageSize.Width / 2, document.Bottom, writer.DirectContent);

            PdfPTable tabFotPaginacao = new PdfPTable(new float[] { 1F });
            PdfPCell cellPaginacao;
            tabFotPaginacao.TotalWidth = 100F;
            cellPaginacao = new PdfPCell(new Phrase("Página: " + writer.PageNumber.ToString(), FonteRodapeNormal));
            cellPaginacao.Border = 0;
            tabFotPaginacao.AddCell(cellPaginacao);
            tabFotPaginacao.DefaultCell.Border = 0;
            tabFotPaginacao.WriteSelectedRows(0, -1, document.Right - 50, document.Bottom, writer.DirectContent);
        }
    }
}