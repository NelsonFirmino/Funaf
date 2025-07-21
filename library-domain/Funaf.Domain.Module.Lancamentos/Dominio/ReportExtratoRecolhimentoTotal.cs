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
    [DBMappedClass(Table = "ReportExtratoArrecadacaoTotal", PrimaryKey = "idDados")]
    public class ReportExtratoRecolhimentoTotal
    {
        public string txMes { get; set; }
        public int nuAno { get; set; }
        //public decimal vaDocumento { get; set; }
        public decimal vaPago { get; set; }

        public static string CriarPDF(List<ReportExtratoRecolhimentoTotal> dados, string subTitulo)
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
            writer.PageEvent = new HeaderAndFooter("EXTRATO RECOLHIMENTO DO FUNAF EM "+ dados[0].nuAno, subTitulo, " ");
            doc.Open();

            doc.Add(Chunk.NEWLINE);

            PdfPTable tabela = new PdfPTable(2) { WidthPercentage = 70 };
            tabela.SpacingBefore = 20;
            tabela.SpacingAfter = 20;
            tabela.SetTotalWidth(new float[] { 20,20 });

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
            tabela.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            tabela.AddCell(new Phrase("MÊS/ANO", FontNegritoHeader));
           // tabela.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
           // tabela.AddCell(new Phrase("VR DECLARADO(R$)", FontNegritoHeader));
            tabela.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            tabela.AddCell(new Phrase("VR ARRECADADO(R$)", FontNegritoHeader));
      
            var totPago = 0m;
           // var totDocumento = 0m;

            foreach (var iDado in dados)
            {
                tabela.DefaultCell.BackgroundColor = (contador % 2 == 0) ? cinzaClaro : BaseColor.WHITE;

                tabela.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                tabela.AddCell(new Phrase(iDado.txMes + "/" + iDado.nuAno, FontNormal));
                //tabela.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                //tabela.AddCell(new Phrase(iDado.vaDocumento.ToString("N2"), FontNormal));
                tabela.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                tabela.AddCell(new Phrase(iDado.vaPago.ToString("N2"), FontNormal));
                contador++;
                totPago += iDado.vaPago;
              //  totDocumento += iDado.vaDocumento;
            }
            tabela.DefaultCell.BackgroundColor = BaseColor.LIGHT_GRAY;
           
            tabela.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            tabela.AddCell(new Phrase("TOTAL(R$)", FontNegritoHeader));
           // tabela.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
           // tabela.AddCell(new Phrase(totDocumento.ToString("N2"), FontNegritoHeader));
            tabela.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            tabela.AddCell(new Phrase(totPago.ToString("N2"), FontNegritoHeader));

            doc.Add(tabela);

            doc.Close();

            var pdfBase64 = Convert.ToBase64String(ms.ToArray());

            return pdfBase64;
        }
   

    }
}
