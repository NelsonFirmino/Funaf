using Funaf.Domain.Module.Lancamentos.Dominio;
using Funaf.Domain.Module.Lancamentos.Persistencia;
using Funaf.Web.Api.ViewModels;
using System;
using System.Collections.Generic;
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
    public class ComarcaController : ApiController
    {
        [HttpGet]
        [Route("api/comarcas")]
        public HttpResponseMessage GetAllComarcas()
        {
            try
            {
                var comarcas = new List<ComarcaVieModel>();

                var tb = DBComarca.GetAll();

                foreach (var iComarca in tb)
                {
                    var novo = new ComarcaVieModel
                    {
                        Id = iComarca.Id,
                        txComarca = iComarca.txComarca
                    };

                    comarcas.Add(novo);
                }

                return Request.CreateResponse(HttpStatusCode.OK, comarcas);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Erro ao tentar Consultar Todos.", ex);
            }
        }
    }
}
