using Funaf.Domain.Module.Lancamentos.Dominio;
using Funaf.Domain.Module.Lancamentos.Persistencia;
using Funaf.Web.Api.ViewModels;
using Microsoft.Reporting.WebForms;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Funaf.Web.Api.Controllers
{
    #if DEBUG
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    #else
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    #endif
    public class ReportController : ApiController
    {
        [Authorize]
        [HttpPost]
        [Route("api/Report/Sigef/")]
        public HttpResponseMessage RelSigef([FromBody]ArrecadacaoDividaSigefViewModel data)
        {
            var retorno = Request.CreateResponse(HttpStatusCode.OK);

            var objRel = DBRelSigef.RecuperarItens(data.dtInicio, data.dtFim, data.idTipoTributo);

            ReportViewer rv = new ReportViewer();
            rv.ProcessingMode = ProcessingMode.Local;
            rv.LocalReport.ReportPath = @"Reports\\RelSigef.rdlc";
            rv.LocalReport.DataSources.Add(new ReportDataSource("DSRelatorio", objRel));

            ReportParameter dtIni = new ReportParameter("dtInicio", data.dtInicio.ToString("dd/MM/yyyy"));
            ReportParameter dtFim = new ReportParameter("dtFim", data.dtFim.ToString("dd/MM/yyyy"));

            rv.LocalReport.SetParameters(new ReportParameter[] { dtIni, dtFim });

            Warning[] warning = null;
            string[] streamids = null;
            string mimeType = null;
            string encoding = null;
            string fileNameExtension = null;

            if (data.tipoRelatorio == "excel")
            {
                byte[] dataReport = rv.LocalReport.Render("Excel", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warning);
                string RelTitulo = "RelArrecadacaoDividaSigef";

                DateTime dataHoraGeracao = DateTime.Now;
                string NomeRel = RelTitulo + dataHoraGeracao.ToString("yyyyMMddHHmm") + ".xls";

                retorno.Content = new ByteArrayContent(dataReport);
                retorno.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.ms-excel");
                retorno.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                retorno.Content.Headers.ContentDisposition.FileName = NomeRel;
            }
            else
            {
                byte[] dataReport = rv.LocalReport.Render("pdf", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warning);
                string RelTitulo = "RelArrecadacaoDividaSigef";

                DateTime dataHoraGeracao = DateTime.Now;
                string NomeRel = RelTitulo + dataHoraGeracao.ToString("yyyyMMddHHmm") + ".pdf";

                retorno.Content = new ByteArrayContent(dataReport);
                retorno.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                retorno.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("inline");
                retorno.Content.Headers.ContentDisposition.FileName = NomeRel;
            }

            return retorno;
        }

        [Authorize]
        [HttpGet]
        [Route("api/Report/DevolucaoSolicitacao/{idSolicitacao}")]
        public HttpResponseMessage RelDevolucaoSolicitacao([FromUri] int idSolicitacao)
        {
            var retorno = Request.CreateResponse(HttpStatusCode.OK);

            var objRel = DBRelDevolucaoSolicitacao.RecuperarItens(idSolicitacao);

            ReportViewer rv = new ReportViewer();
            rv.ProcessingMode = ProcessingMode.Local;
            rv.LocalReport.ReportPath = @"Reports\\RelDevolucaoSolicitacao.rdlc";
            rv.LocalReport.DataSources.Add(new ReportDataSource("DSRelatorio", objRel));

            //rv.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter("Empresa", "Microsoft"));

            Warning[] warning = null;
            string[] streamids = null;
            string mimeType = null;
            string encoding = null;
            string fileNameExtension = null;

            byte[] pdfData = rv.LocalReport.Render("pdf", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warning);
            string RelTitulo = "RelDevolucaoSolicitacao";

            DateTime dataHoraGeracao = DateTime.Now;
            string NomeRel = RelTitulo + dataHoraGeracao.ToString("yyyyMMddHHmm") + ".pdf";

            retorno.Content = new ByteArrayContent(pdfData);
            retorno.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            retorno.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("inline");
            retorno.Content.Headers.ContentDisposition.FileName = NomeRel;

            return retorno;
        }

        [Authorize]
        [HttpGet]
        [Route("api/Report/Resumo/{idDeclaracao}")]
        public HttpResponseMessage RelResumo([FromUri] int idDeclaracao)
        {
            var retorno = Request.CreateResponse(HttpStatusCode.OK);

            var objRel = DBRelResumo.RecuperarItens(idDeclaracao);

            ReportViewer rv = new ReportViewer();
            rv.ProcessingMode = ProcessingMode.Local;
            rv.LocalReport.ReportPath = @"Reports\\RelResumo.rdlc";
            rv.LocalReport.DataSources.Add(new ReportDataSource("dsRelatorio", objRel));

            //rv.LocalReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter("Empresa", "Microsoft"));

            Warning[] warning = null;
            string[] streamids = null;
            string mimeType = null;
            string encoding = null;
            string fileNameExtension = null;

            byte[] pdfData = rv.LocalReport.Render("pdf", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warning);
            string RelTitulo = "RelResumo";

            DateTime dataHoraGeracao = DateTime.Now;
            string NomeRel = RelTitulo + dataHoraGeracao.ToString("yyyyMMddHHmm") + ".pdf";

            retorno.Content = new ByteArrayContent(pdfData);
            retorno.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            retorno.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("inline");
            retorno.Content.Headers.ContentDisposition.FileName = NomeRel;

            return retorno;
        }

        [Authorize]
        [HttpGet]
        [Route("api/Report/EmitirGuia/{idDeclaracao}")]
        public HttpResponseMessage EmitirGuia([FromUri] int idDeclaracao)
        {
            var retorno = Request.CreateResponse(HttpStatusCode.OK);
            var RelTitulo = "Guia_Recolhimento_FUNAF_";

            try
            {
                var pdfData = BoletoViewModel.CriarBoleto(idDeclaracao);

                DateTime dataHoraGeracao = DateTime.Now;
                string NomeRel = RelTitulo + dataHoraGeracao.ToString("yyyyMMddHHmm") + ".pdf";

                retorno.Content = new ByteArrayContent(pdfData.ToArray());
                retorno.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                retorno.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                retorno.Content.Headers.ContentDisposition.FileName = NomeRel;
            }
            catch (Exception ex)
            {
                retorno = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                retorno.Content = new StringContent(ex.Message, System.Text.Encoding.UTF8, "text/plain");
            }

            return retorno;
        }

        [Authorize]
        [HttpGet]
        [Route("api/Report/FichaCadastral/{idDeclaracao}")]
        public HttpResponseMessage RelFichaCadastral([FromUri] int idDeclaracao)
        {
            var retorno = Request.CreateResponse(HttpStatusCode.OK);

            var objRel = DBRelFichaCadastral.RecuperarItens(idDeclaracao);

            ReportViewer rv = new ReportViewer();
            rv.ProcessingMode = ProcessingMode.Local;
            rv.LocalReport.ReportPath = @"Reports\\RelFichaCadastral.rdlc";
            rv.LocalReport.DataSources.Add(new ReportDataSource("DsRelatorio", objRel));

            Warning[] warning = null;
            string[] streamids = null;
            string mimeType = null;
            string encoding = null;
            string fileNameExtension = null;

            byte[] pdfData = rv.LocalReport.Render("pdf", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warning);
            string RelTitulo = "RelFichaCadastral";

            DateTime dataHoraGeracao = DateTime.Now;
            string NomeRel = RelTitulo + dataHoraGeracao.ToString("yyyyMMddHHmm") + ".pdf";

            retorno.Content = new ByteArrayContent(pdfData);
            retorno.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            retorno.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            retorno.Content.Headers.ContentDisposition.FileName = NomeRel;

            return retorno;
        }

        [Authorize]
        [HttpPost]
        [Route("api/Report/PrintCalculoFunaf")]
        public HttpResponseMessage PrintCalculoFunaf([FromBody]ServicoViewModel[] tb)
        {
            var retorno = Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                int inc = 0;
                RelResumo[] rels = new RelResumo[tb.Count()];
                foreach (var item in tb)
                {
                    var rel = new RelResumo();
                    var dbServ = DBServico.GetById(item.Id);
                    rel.nuQuantidade = item.Quantidade;
                    rel.nuOrdem = inc++;
                    rel.txGrupo = dbServ.GrupoServicos.txDiscriminacao;
                    rel.txServico = dbServ.txDiscriminacao;
                    rel.vaServico = dbServ.vaServico;
                    rel.vaTotal = rel.vaServico * rel.nuQuantidade;

                    rels[rel.nuOrdem] = rel;
                }

                var objRel = rels;

                ReportViewer rv = new ReportViewer
                {
                    ProcessingMode = ProcessingMode.Local
                };
                rv.LocalReport.ReportPath = @"Reports\\RelCalculoFunaf.rdlc";
                rv.LocalReport.DataSources.Add(new ReportDataSource("dsRelatorio", objRel));

                Warning[] warning = null;
                string[] streamids = null;
                string mimeType = null;
                string encoding = null;
                string fileNameExtension = null;

                byte[] pdfData = rv.LocalReport.Render("pdf", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warning);
                string RelTitulo = "RelCalculoFunaf";

                DateTime dataHoraGeracao = DateTime.Now;
                string NomeRel = RelTitulo + dataHoraGeracao.ToString("yyyyMMddHHmm") + ".pdf";

                retorno.Content = new ByteArrayContent(pdfData);
                retorno.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                retorno.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                retorno.Content.Headers.ContentDisposition.FileName = NomeRel;

                return retorno;

            }
            catch (Exception ex)
            {
                //if (ts.Connection != null) { ts.Rollback(); }
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Erro: Registro não foi incluido.", ex);
            }
        }

        [HttpPost]
        [Route("api/Report/RelRecolhimento")]
        public HttpResponseMessage RelRecolhimento([FromBody]RecolhimentoViewModel data)
        {
            var retorno = Request.CreateResponse(HttpStatusCode.OK);

            var objRel = DBRelRecolhimento.RecuperarItens(data.dtInicio, data.dtFim);

            ReportViewer rv = new ReportViewer();
            rv.ProcessingMode = ProcessingMode.Local;
            rv.LocalReport.ReportPath = @"Reports\\RelRecolhimentoFunaf.rdlc";
            rv.LocalReport.DataSources.Add(new ReportDataSource("dsRelatorio", objRel));

            ReportParameter dtIni = new ReportParameter("dtInicio", data.dtInicio.ToString("dd/MM/yyyy"));
            ReportParameter dtFim = new ReportParameter("dtFim", data.dtFim.ToString("dd/MM/yyyy"));

            rv.LocalReport.SetParameters(new ReportParameter[] { dtIni, dtFim });

            Warning[] warning = null;
            string[] streamids = null;
            string mimeType = null;
            string encoding = null;
            string fileNameExtension = null;

            byte[] pdfData = rv.LocalReport.Render("pdf", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warning);
            string RelTitulo = "RelRecolhimentoFunaf";

            DateTime dataHoraGeracao = DateTime.Now;
            string NomeRel = RelTitulo + dataHoraGeracao.ToString("yyyyMMddHHmm") + ".pdf";

            retorno.Content = new ByteArrayContent(pdfData);
            retorno.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            retorno.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("inline");
            retorno.Content.Headers.ContentDisposition.FileName = NomeRel;

            return retorno;
        }

        [Authorize]
        [HttpPost]
        [Route("api/Report/RelArrecadacaoServentia")]
        public HttpResponseMessage RelArrecadacaoServentia([FromBody]RecolhimentoViewModel data)
        {
            var retorno = Request.CreateResponse(HttpStatusCode.OK);

            var objRel = DBRelArrecadacaoServentia.RecuperarItens(data.dtInicio, data.dtFim);

            ReportViewer rv = new ReportViewer();
            rv.ProcessingMode = ProcessingMode.Local;
            rv.LocalReport.ReportPath = @"Reports\\RelArrecadacaoServentia.rdlc";
            rv.LocalReport.DataSources.Add(new ReportDataSource("DSServentia", objRel));

            ReportParameter dtIni = new ReportParameter("dtInicio", data.dtInicio.ToString("dd/MM/yyyy"));
            ReportParameter dtFim = new ReportParameter("dtFim", data.dtFim.ToString("dd/MM/yyyy"));

            rv.LocalReport.SetParameters(new ReportParameter[] { dtIni, dtFim });

            Warning[] warning = null;
            string[] streamids = null;
            string mimeType = null;
            string encoding = null;
            string fileNameExtension = null;

            byte[] pdfData = rv.LocalReport.Render("pdf", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warning);
            string RelTitulo = "RelArrecadacaoServentia";

            DateTime dataHoraGeracao = DateTime.Now;
            string NomeRel = RelTitulo + dataHoraGeracao.ToString("yyyyMMddHHmm") + ".pdf";

            retorno.Content = new ByteArrayContent(pdfData);
            retorno.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            retorno.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("inline");
            retorno.Content.Headers.ContentDisposition.FileName = NomeRel;

            return retorno;
        }
    }
}