using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Funaf.Domain.Module.Lancamentos.Persistencia;
using Funaf.Domain.Module.Lancamentos.Dominio;
using Funaf.Web.Api.ViewModels;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Funaf.Web.Api.Controllers.Lancamentos
{
    #if DEBUG
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    #else
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    #endif
    [Authorize]
    public class DeclaracaoController : ApiController
    {
        [HttpPost]
        [Route("api/Declaracao")]
        public HttpResponseMessage IncluirDeclaracoes([FromBody]Declaracao tb)
        {
            var ts = DBDeclaracao.GetTransaction();
            try
            {
                DBDeclaracao.Save(tb, ts);
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

        [HttpPost]
        [Route("api/DeclaracaoServico")]
        public HttpResponseMessage IncluirDeclaracaoServico([FromBody]DeclaracaoServico tb)
        {
            var ts = DBDeclaracaoServico.GetTransaction();
            try
            {
                DBDeclaracaoServico.Save(tb, ts);
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

        [Route("api/declaracoes/cartorio/{idcartorio}")]
        [HttpGet]
        public HttpResponseMessage RecuperarDeclaracoesPorCartorio(int idcartorio)
        {
            try
            {
                var tb = DBDeclaracao.ListagemDeclaracoesSimplificada(idcartorio);

                var declaracoes = new List<DeclaracaoSimplificadaViewModel>();

                foreach (var iDeclaracao in tb)
                {
                    var novo = new DeclaracaoSimplificadaViewModel
                    {
                        Id = iDeclaracao.Id,
                        dtInicio = iDeclaracao.dtPeriodoInicial.ToShortDateString(),
                        dtFinal = iDeclaracao.dtPeriodoFinal.ToShortDateString(),
                        valor = DBDeclaracaoServico.RecuperarValorTotal(iDeclaracao.Id).ToString("N2"),
                        isQuite = (iDeclaracao.Boleto == null) ? false : (iDeclaracao.Boleto.Situacao.Id == 402)
                    };

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

        [HttpPost]
        [Route("api/NovaDeclaracao")]
        public HttpResponseMessage SalvarTabelas([FromBody]FormDataViewModel viewModel)
        {
            var ts = DBDeclaracao.GetTransaction();

            try
            {

                var novaDeclaracao = new Declaracao()
                {
                    dtPeriodoInicial = DateTime.Parse(viewModel.dtInicialPeriodo),
                    dtPeriodoFinal = DateTime.Parse(viewModel.dtFinalPeriodo),
                    Serventia = new Serventia() { Id = int.Parse(viewModel.cartorio) },
                    UsuarioAbertura = new Usuario() { Id = int.Parse(viewModel.usuario) }
                };

                if(DBDeclaracao.VerificarPeriodo(novaDeclaracao))
                {
                    DBDeclaracao.Save(novaDeclaracao, ts);

                    if (novaDeclaracao.Id > 0)
                    {
                        foreach (var iServico in viewModel.servicos)
                        {
                            var novo = new DeclaracaoServico()
                            {
                                Declaracao = novaDeclaracao,
                                Servico = new Servico() { Id = iServico.Id },
                                nuQuantidade = iServico.Quantidade,
                                vaServico = iServico.Valor
                            };

                            if (PeriodoInvalido(novaDeclaracao))
                            {
                                throw new Exception(JsonConvert.SerializeObject(novo));
                            }

                            DBDeclaracaoServico.Save(novo, ts);
                        }
                    }

                    ts.Commit();

                    return Request.CreateResponse(HttpStatusCode.OK, novaDeclaracao);
                }       
                else
                    return Request.CreateResponse(HttpStatusCode.Forbidden, "Já existe uma declaração registradas entre o periodo informado, selecione outro periodo antes de enviar esta declaração.");

                
            }
            catch (Exception ex)
            {
                if (ts.Connection != null) { ts.Rollback(); }

                DBAppLog.Save(new AppLog { txMensagem = ex.Message, txStack = ex.StackTrace });
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Erro: Registro não foi incluido.", ex);
            }
        }

        private bool PeriodoInvalido(Declaracao declaracao)
        {
            return (declaracao.dtPeriodoInicial.Year != declaracao.dtPeriodoFinal.Year) || (declaracao.dtPeriodoInicial.Month != declaracao.dtPeriodoFinal.Month);
        }
    }
}
