using Funaf.Domain.Module.Lancamentos.Persistencia;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Funaf.Web.Api.Controllers
{
    #if DEBUG
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    #else
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    #endif
    [Authorize]
    public class ServicosController : ApiController
    {
        [HttpGet]
        [Route("api/servicos")]
        public HttpResponseMessage GetAllTables()
        {
            try
            {
                var tb = DBServico.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, tb);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Erro ao tentar Consultar Todos.", ex);
            }
        }
    }
}
