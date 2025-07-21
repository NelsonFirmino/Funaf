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
    [DBMappedClass(Table = "ReportInandimplente", PrimaryKey = "idDados")]
    public class ReportInadimplente
    {
        public string txComarca { get; set; }
        public string txCartorio { get; set; }
        public string txMes { get; set; }
        public int nuAno { get; set; }
        public string txTelefone { get; set; }

        public static string CriarPDF(List<ReportInadimplente> dados, string subTitulo)
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
            writer.PageEvent = new HeaderAndFooter("RELAÇÃO DE INADIMPLENTES DO FUNAF EM "+ dados[0].nuAno, subTitulo, " ");
            doc.Open();

            doc.Add(Chunk.NEWLINE);

            PdfPTable tabela = new PdfPTable(4) { WidthPercentage = 100 };
            tabela.SpacingBefore = 20;
            tabela.SpacingAfter = 20;
            tabela.SetTotalWidth(new float[] { 30,50,20,20 });

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
            tabela.AddCell(new Phrase("TELEFONE", FontNegritoHeader));
            tabela.AddCell(new Phrase("PERÍODO", FontNegritoHeader));
      
 

            foreach (var iDado in dados)
            {
                tabela.DefaultCell.BackgroundColor = (contador % 2 == 0) ? cinzaClaro : BaseColor.WHITE;

                tabela.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                tabela.AddCell(new Phrase(iDado.txComarca, FontNormal));
                tabela.AddCell(new Phrase(iDado.txCartorio, FontNormal));
                tabela.AddCell(new Phrase(iDado.txTelefone, FontNormal));
                tabela.AddCell(new Phrase(iDado.txMes + "/" + iDado.nuAno, FontNormal));
                contador++;

            }
            

            doc.Add(tabela);

            doc.Close();

            var pdfBase64 = Convert.ToBase64String(ms.ToArray());

            return pdfBase64;
        }
   

    }
}
