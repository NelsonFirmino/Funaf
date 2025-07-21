using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Linq;
using Funaf.Domain.Module.Lancamentos.Persistencia;

namespace Funaf.Web.Api.Controllers.Lancamentos
{
    #if DEBUG
    [EnableCors(origins: "*", headers: "*", methods: "*")]
#else
    [EnableCors(origins: "*", headers: "*", methods: "*")]
#endif
    [Authorize]
    public class SimuladorController : ApiController
    {
        [HttpGet]
        [Route("api/getTable/{idTabela}")]
        [Authorize]
        public HttpResponseMessage GetTable(int idTabela)
        {
            try
            {
                var tb = DBServico.GetAll().Where(t => t.GrupoServicos.Id == idTabela);
                return Request.CreateResponse(HttpStatusCode.OK, tb);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Erro ao tentar Consultar Todos.", ex);
            }
        }

        [HttpGet]
        [Route("api/getAllTables")]
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
