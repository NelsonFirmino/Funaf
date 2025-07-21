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
    public class CartoriosController : ApiController
    {
        [HttpGet]
        [Route("api/cartorios")]
        public HttpResponseMessage GetAllServentias()
        {
            try
            {
                var cartorios = new List<CartorioViewModel>();

                var tb = DBServentia.GetAll();

                foreach (var iServentia in tb)
                {
                    var novo = new CartorioViewModel
                    {
                        Id = iServentia.Id,
                        txCartorio = iServentia.txCartorio,
                        txResponsavel = iServentia.txResponsavel,
                        txEndereco = iServentia.txEndereco,
                        txCEP = iServentia.txCEP,
                        txEmail = iServentia.txEmail,
                        txTelefone = iServentia.txTelefone,
                        txServicos = iServentia.txServicos,
                        txComarca = DBComarca.RecuperarNomePorServentia(iServentia.Id)
                    };

                    cartorios.Add(novo);
                }

                return Request.CreateResponse(HttpStatusCode.OK, cartorios);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Erro ao tentar Consultar Todos.", ex);
            }
        }

        [Route("api/cartorios/{idcartorio}/declaracoes")]
        [HttpGet]
        public HttpResponseMessage GetDeclaracoesSimplificada(int idcartorio)
        {
            try
            {
                // retorna as 10 ultimas declarações
                var tb = DBDeclaracao.ListagemDeclaracoesSimplificada(idcartorio);

                var declaracoes = new List<DeclaracaoSimplificadaViewModel>();

                foreach (var iDeclaracao in tb)
                {
                    var novo = new DeclaracaoSimplificadaViewModel();
                    novo.dtInicio = iDeclaracao.dtPeriodoInicial.ToShortDateString();
                    novo.dtFinal = iDeclaracao.dtPeriodoFinal.ToShortDateString();
                    novo.valor = DBDeclaracaoServico.RecuperarValorTotal(iDeclaracao.Id).ToString("N2");

                    declaracoes.Add(novo);
                }

                return Request.CreateResponse(HttpStatusCode.OK, declaracoes);
            }
            catch (Exception ex)
            {
                DBAppLog.Save(new AppLog { txMensagem = ex.Message, txStack = ex.StackTrace });
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Erro: Registro não foi incluido.", ex);
            }
        }

        [Route("api/cartorios/{idcomarca}/comarca")]
        [HttpGet]
        public HttpResponseMessage ListarPorComarca(int idcomarca)
        {
            try
            {
                var tb = DBServentia.ListarPorComarca(idcomarca);

                var cartorios = new List<CartorioViewModel>();

                foreach (var iCartorio in tb)
                {
                    var novo = new CartorioViewModel();
                    novo.Id = iCartorio.Id;
                    novo.txCartorio = iCartorio.txCartorio;

                    cartorios.Add(novo);
                }

                return Request.CreateResponse(HttpStatusCode.OK, cartorios);
            }
            catch (Exception ex)
            {
                DBAppLog.Save(new AppLog { txMensagem = ex.Message, txStack = ex.StackTrace });
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Erro: não foi possível recuperar os cartórios para esta comarca.", ex);
            }
        }
    }
}
