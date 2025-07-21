using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Funaf.Web.Api.Helpers;
using Funaf.Web.Api.ViewModels;
using Funaf.Domain.Module.Lancamentos.Persistencia;
using Funaf.Domain.Module.Lancamentos.Dominio;
using System.Collections.Generic;
using PGEMailProxy;
using System.Linq;

namespace Funaf.Web.Api.Controllers.Autenticacao
{
    #if DEBUG
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    #else
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    #endif
    [Authorize]
    public class ServentiaController : ApiController
    {
        [HttpGet]
        [Route("api/serventia")]
        public HttpResponseMessage GetAllServentias()
        {
            try
            {
                var serventias = DBServentia.GetTodos();
                return Request.CreateResponse(HttpStatusCode.OK, serventias);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Erro ao tentar Consultar Todos.", ex);
            }
        }
        
        [HttpGet]
        [Route("api/serventia/{cartorio}/{responsavel}")]
        public HttpResponseMessage GetServentias([FromUri]string cartorio, string responsavel)
        {
            try
            {
                if (cartorio == "null") cartorio = "";
                if (responsavel == "null") responsavel = "";
                var usuarios = DBServentia.PesquisarServentias(cartorio, responsavel);
                return Request.CreateResponse(HttpStatusCode.OK, usuarios);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Erro ao tentar Consultar Todos.", ex);
            }
        }
        
        [HttpPost]
        [Route("api/serventia")]
        public HttpResponseMessage IncluirServentia([FromBody]Serventia tb)
        {
            var ts = DBServentia.GetTransaction();
            try
            {
                DBServentia.Save(tb, ts);
                
                var tbComarcaServentia = new ComarcaServentia();
                tbComarcaServentia.idComarca = tb.Comarca.Id;
                tbComarcaServentia.idServentia = tb.Id;
                DBComarcaServentia.Save(tbComarcaServentia, ts);
                ts.Commit();

                return Request.CreateResponse(HttpStatusCode.OK, tb);
            }
            catch (Exception ex)
            {
                if (ts.Connection != null) { ts.Rollback(); }

                //DBAppLog.Save(new AppLog { txMensagem = ex.Message, txStack = ex.StackTrace });
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Erro: Registro não foi incluido.", ex);
            }
        }
        
        [HttpPut]
        [Route("api/serventia")]
        public HttpResponseMessage AlterarServentia([FromBody]Serventia tb)
        {
            var ts = DBServentia.GetTransaction();
            try
            {
                var tbComarcaServentia = DBComarcaServentia.GetByServentia(tb.Id);
                DBServentia.Save(tb, ts);
                tbComarcaServentia.idComarca = tb.Comarca.Id;
                tbComarcaServentia.idServentia = tb.Id;
                DBComarcaServentia.Save(tbComarcaServentia, ts);

                ts.Commit();

                return Request.CreateResponse(HttpStatusCode.OK, tb);
            }
            catch (Exception ex)
            {
                if (ts.Connection != null) { ts.Rollback(); }

                //DBAppLog.Save(new AppLog { txMensagem = ex.Message, txStack = ex.StackTrace });
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Erro: Registro não foi incluido.", ex);
            }
        }
        
        [HttpDelete]
        [Route("api/serventia")]
        public HttpResponseMessage DeleteServentia([FromUri]int id)
        {
            var ts = DBServentia.GetTransaction();
            try
            {
                DBServentia.Delete(id, ts);
                ts.Commit();

                return Request.CreateResponse(HttpStatusCode.OK, id);
            }
            catch (Exception ex)
            {
                if (ts.Connection != null) { ts.Rollback(); }

                //DBAppLog.Save(new AppLog { txMensagem = ex.Message, txStack = ex.StackTrace });
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Erro: Registro não foi incluido.", ex);
            }


        }
    }
}
