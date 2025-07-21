using Funaf.Domain.Module.Lancamentos.Dominio;
using Funaf.Domain.Module.Lancamentos.Persistencia;
using Funaf.Web.Api.ViewModels;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Funaf.Web.Api.Controllers.Reports
{
#if DEBUG
    [EnableCors(origins: "*", headers: "*", methods: "*")]
#else
        [EnableCors(origins: "*", headers: "*", methods: "*" )]
#endif
    public class ReportsController : ApiController
    {
        [HttpGet]
        [Route("api/reports/boletoematraso/{id}")]
        public HttpResponseMessage GerarRelatorioBoletoEmAtraso([FromUri] int id)
        {
            var response = Request.CreateResponse(HttpStatusCode.NotFound, "");

            try
            {

                var lsDados = DBReportBoletoEmAtraso.RecuperarItens();
                if (lsDados.Count() > 0)
                {
                    var subTitulo = "EM " + DateTime.Now.ToString("dd/MM/yyyy");
                    var pdfString = ReportBoletoEmAtraso.CriarPDF(lsDados, subTitulo);

                    string NomeRel = "boletoEmAtraso_" + DateTime.Now.ToString("yyyyMMddHHmm") + ".pdf";
                    var pdfStream = Convert.FromBase64String(pdfString);

                    response = Request.CreateResponse(HttpStatusCode.OK, pdfString);
                    response.Content = new ByteArrayContent(pdfStream);
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("inline");
                    response.Content.Headers.ContentDisposition.FileName = NomeRel;
                }
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
                //  log.Info(ex.Message);
            }

            return response;
        }
        //[HttpGet]
        //[Route("api/reports/extratorecolhimento/{ano}/{id}")]
        //public HttpResponseMessage GerarRelatorioextratoRecolhimento([FromUri] int ano, int id)
        //{
        //    var response = Request.CreateResponse(HttpStatusCode.NotFound, "");

        //    try
        //    {

        //        var lsDados = DBReportExtratoRecolhimento.RecuperarItens(ano,id);
        //        if (lsDados.Count() > 0)
        //        {
        //            var subTitulo = lsDados[0].txComarca + " - " + lsDados[0].txCartorio;
        //            var pdfString = ReportExtratoRecolhimento.CriarPDF(lsDados, subTitulo);

        //            string NomeRel = "extrato_" + DateTime.Now.ToString("yyyyMMddHHmm") + ".pdf";
        //            var pdfStream = Convert.FromBase64String(pdfString);

        //            response = Request.CreateResponse(HttpStatusCode.OK, pdfString);
        //            response.Content = new ByteArrayContent(pdfStream);
        //            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
        //            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("inline");
        //            response.Content.Headers.ContentDisposition.FileName = NomeRel;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
        //        //  log.Info(ex.Message);
        //    }

        //    return response;
        //}
        [HttpGet]
        [Route("api/reports/extratototal/{ano}")]
        public HttpResponseMessage GerarRelatorioExtratoTotal([FromUri] int ano)
        {
            var response = Request.CreateResponse(HttpStatusCode.NotFound, "");

            try
            {

                var lsDados = DBReportExtratoRecolhimentoTotal.RecuperarItens(ano);
                if (lsDados.Count() > 0)
                {
                    var subTitulo = "EM " + DateTime.Now.ToString("dd/MM/yyyy");
                    var pdfString = ReportExtratoRecolhimentoTotal.CriarPDF(lsDados, subTitulo);

                    string NomeRel = "extratototal_" + DateTime.Now.ToString("yyyyMMddHHmm") + ".pdf";
                    var pdfStream = Convert.FromBase64String(pdfString);

                    response = Request.CreateResponse(HttpStatusCode.OK, pdfString);
                    response.Content = new ByteArrayContent(pdfStream);
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("inline");
                    response.Content.Headers.ContentDisposition.FileName = NomeRel;
                }
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
                //  log.Info(ex.Message);
            }

            return response;
        }
        [HttpGet]
        [Route("api/reports/inadimplente/{ano}")]
        public HttpResponseMessage GerarRelatorioInadimplentes([FromUri] int ano)
        {
            var response = Request.CreateResponse(HttpStatusCode.NotFound, "");

            try
            {

                var lsDados = DBReportInadimplente.RecuperarItens(ano);
                if (lsDados.Count() > 0)
                {
                    var subTitulo = "EM " + DateTime.Now.ToString("dd/MM/yyyy");
                    var pdfString = ReportInadimplente.CriarPDF(lsDados, subTitulo);

                    string NomeRel = "Inadimplente_" + DateTime.Now.ToString("yyyyMMddHHmm") + ".pdf";
                    var pdfStream = Convert.FromBase64String(pdfString);

                    response = Request.CreateResponse(HttpStatusCode.OK, pdfString);
                    response.Content = new ByteArrayContent(pdfStream);
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("inline");
                    response.Content.Headers.ContentDisposition.FileName = NomeRel;
                }
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
                //  log.Info(ex.Message);
            }

            return response;
        }
        [HttpGet]
        [Route("api/reports/declaracaorecolhimento/{ano}/{id}")]
        public HttpResponseMessage GerarRelatorioDeclaracaoRecolhimento([FromUri] int ano, int id)
        {
            var response = Request.CreateResponse(HttpStatusCode.NotFound, "");

            try
            {

                var lsDados = DBReportDeclaradoRecolhimento.RecuperarItens(ano, id);
                if (lsDados.Count() > 0)
                {
                    var subTitulo = lsDados[0].txComarca + " - " + lsDados[0].txCartorio;
                    var pdfString = ReportDeclaradoRecolhimento.CriarPDF(lsDados, subTitulo);

                    string NomeRel = "declaracao_" + DateTime.Now.ToString("yyyyMMddHHmm") + ".pdf";
                    var pdfStream = Convert.FromBase64String(pdfString);

                    response = Request.CreateResponse(HttpStatusCode.OK, pdfString);
                    response.Content = new ByteArrayContent(pdfStream);
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("inline");
                    response.Content.Headers.ContentDisposition.FileName = NomeRel;
                }
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
                //  log.Info(ex.Message);
            }

            return response;
        }
        [HttpGet]
        [Route("api/reports/declaracaototal/{ano}")]
        public HttpResponseMessage GerarRelatorioDeclaracaoTotal([FromUri] int ano)
        {
            var response = Request.CreateResponse(HttpStatusCode.NotFound, "");

            try
            {

                var lsDados = DBReportDeclaradoRecolhimentoTotal.RecuperarItens(ano);
                if (lsDados.Count() > 0)
                {
                    var subTitulo = "EM " + DateTime.Now.ToString("dd/MM/yyyy");
                    var pdfString = ReportDeclaradoRecolhimentoTotal.CriarPDF(lsDados, subTitulo);

                    string NomeRel = "declaracaototal_" + DateTime.Now.ToString("yyyyMMddHHmm") + ".pdf";
                    var pdfStream = Convert.FromBase64String(pdfString);

                    response = Request.CreateResponse(HttpStatusCode.OK, pdfString);
                    response.Content = new ByteArrayContent(pdfStream);
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("inline");
                    response.Content.Headers.ContentDisposition.FileName = NomeRel;
                }
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
                //  log.Info(ex.Message);
            }

            return response;
        }
     

        [HttpPost]
        [Route("api/reports/historico/")]
        public HttpResponseMessage GerarRelatorioHistoricos([FromBody]HistoricoViewModel data)
        {
          
            var response = Request.CreateResponse(HttpStatusCode.NotFound, "");

            try
            {
                var lsDados = DBReportHistorico.RecuperarItens(data.idServentia,data.dtInicio, data.dtFim);
                if (lsDados.Count() > 0)
                {
                    var subTitulo = lsDados[0].txComarca + " - " + lsDados[0].txCartorio;
                    var pdfString = ReportHistorico.CriarPDF(lsDados, subTitulo);

                    string NomeRel = "historico_" + DateTime.Now.ToString("yyyyMMddHHmm") + ".pdf";
                    var pdfStream = Convert.FromBase64String(pdfString);

                    response = Request.CreateResponse(HttpStatusCode.OK, pdfString);
                    response.Content = new ByteArrayContent(pdfStream);
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("inline");
                    response.Content.Headers.ContentDisposition.FileName = NomeRel;
                }
                else
                {
                     response = Request.CreateErrorResponse(HttpStatusCode.NotFound, "errossss");
                 //   response = Request.CreateResponse(HttpStatusCode.Forbidden, "
                }
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
                //  log.Info(ex.Message);
            }

            return response;
        }
    }
}

