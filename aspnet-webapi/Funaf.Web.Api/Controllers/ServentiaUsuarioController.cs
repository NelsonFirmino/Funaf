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
    public class ServentiaUsuarioController : ApiController
    {
        [HttpGet]
        [Route("api/serventiausuario")]
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
        [Route("api/serventiausuario/{serventia}/{usuario}")]
        public HttpResponseMessage GetServentias([FromUri]int serventia, int usuario)
        {
            try
            {
                var serventiausuarios = DBServentiaUsuario.PesquisarServentiaUsuarios(serventia, usuario);
                return Request.CreateResponse(HttpStatusCode.OK, serventiausuarios);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Erro ao tentar Consultar Todos.", ex);
            }
        }

        [HttpPost]
        [Route("api/serventiausuario")]
        public HttpResponseMessage IncluirServentiaUsuario([FromBody]ServentiaUsuario tb)
        {
            var ts = DBServentiaUsuario.GetTransaction();
            try
            {
                DBServentiaUsuario.Save(tb, ts);
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
        [Route("api/serventiausuario")]
        public HttpResponseMessage AlterarServentiaUsuario([FromBody]ServentiaUsuario tb)
        {
            var ts = DBServentiaUsuario.GetTransaction();
            try
            {
                DBServentiaUsuario.Save(tb, ts);
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
        [Route("api/serventiausuario/{id}")]
        public HttpResponseMessage DeleteServentiaUsuario([FromUri]int id)
        {
            var ts = DBServentiaUsuario.GetTransaction();
            try
            {
                DBServentiaUsuario.Delete(id, ts);
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
