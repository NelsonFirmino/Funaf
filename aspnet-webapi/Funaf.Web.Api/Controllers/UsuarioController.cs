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
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Configuration;

namespace Funaf.Web.Api.Controllers.Autenticacao
{
#if DEBUG
    [EnableCors(origins: "*", headers: "*", methods: "*")]
#else
    [EnableCors(origins: "*", headers: "*", methods: "*")]
#endif
    public class UsuarioController : ApiController
    {
        [Authorize]
        [HttpGet]
        [Route("api/usuario")]
        public HttpResponseMessage GetAllUsuarios()
        {
            try
            {
                var usuarios = DBUsuario.GetAll("txNome");
                return Request.CreateResponse(HttpStatusCode.OK, usuarios);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Erro ao tentar Consultar Todos.", ex);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("api/usuario/{id}")]
        public HttpResponseMessage GetUsuario([FromUri] int id)
        {
            try
            {
                var usuarios = DBUsuario.GetById(id);
                return Request.CreateResponse(HttpStatusCode.OK, usuarios);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Erro ao tentar Consultar Todos.", ex);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("api/usuario/{nome}/{cpf}")]
        public HttpResponseMessage GetUsuarios([FromUri] string nome, string cpf)
        {
            try
            {
                if (nome == "null") nome = "";
                if (cpf == "null") cpf = "";
                var usuarios = DBUsuario.PesquisarUsuarios(nome, cpf);
                return Request.CreateResponse(HttpStatusCode.OK, usuarios);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Erro ao tentar Consultar Todos.", ex);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/usuario")]
        public HttpResponseMessage IncluirUsuario([FromBody] Usuario tb)
        {
            var ts = DBUsuario.GetTransaction();
            try
            {
                DBUsuario.Incluir(tb);
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

        [Authorize]
        [HttpPut]
        [Route("api/usuario")]
        public HttpResponseMessage AlterarUsuario([FromBody] Usuario tb)
        {
            var ts = DBUsuario.GetTransaction();
            try
            {
                DBUsuario.Alterar(tb);
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

        [Authorize]
        [HttpPatch]
        [Route("api/usuario/senha")]
        public HttpResponseMessage AlterarSenha([FromBody] Usuario tb)
        {
            var ts = DBUsuario.GetTransaction();
            try
            {
                DBUsuario.AlterarSenha(tb.Id, tb.txSenha);
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
        [Route("api/Usuario/autenticar")]
        public HttpResponseMessage Autenticar([FromBody] object token)
        {
            try
            {
                var vUsr = UsuarioHelper.DecodeUsuario(token);

                var autenticado = DBUsuario.Autenticar(vUsr.usuario, vUsr.senha);
                var FUNAF_URL = ConfigurationManager.AppSettings["FUNAF_URL"].ToString();

                if (autenticado != null)
                {
                    if (autenticado.Id > 0)
                    {
                        var key = "bCtSEErAZu8eq0Wk3L2ruJgShNepW0PLPfPH7I4gEZv1NSjKJrvrnvLO8ugXI3Ach2jIi2KT7mYhyfunW5v2blHsu31792yqlBXVd5YxmeFPdDwgmy8zmj0zLIw8a3F"; //Secret key which will be used later during validation
                        var issuer = FUNAF_URL;

                        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                        var permClaims = new List<Claim>
                        {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                        };

                        var tk = new JwtSecurityToken(issuer,
                            issuer,
                            permClaims,
                            expires: DateTime.Now.AddDays(1),
                            signingCredentials: credentials);

                        var tokenHandler = new JwtSecurityTokenHandler().WriteToken(tk);

                        autenticado.Cartorios = DBServentia.ListarPorServentia(autenticado.Id);
                        if (autenticado.Cartorios.Count > 0)
                        {
                            var lsCartoriosViewModel = new List<CartorioViewModel>();

                            foreach (var iCartorio in autenticado.Cartorios)
                            {
                                var novo = new CartorioViewModel();
                                novo.Id = iCartorio.Id;
                                novo.txCartorio = iCartorio.txCartorio;
                                novo.txComarca = DBComarca.RecuperarNomePorServentia(iCartorio.Id);
                                lsCartoriosViewModel.Add(novo);
                            }

                            var viewModel = new UsuarioViewModel()
                            {
                                Id = autenticado.Id,
                                usuario = autenticado.txCPF,
                                nome = autenticado.txNome,
                                txFuncao = autenticado.txFuncao,
                                cartorios = lsCartoriosViewModel,
                                tokenUser = $"Bearer {tokenHandler}"
                            };

                            return Request.CreateResponse<UsuarioViewModel>(HttpStatusCode.OK, viewModel);
                        }
                        else
                            return Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Não foi possivel recuperar os dados da serventia para este usuário, por favor tente novamente, se persistir entre em contato com o administrador do sistema.");
                    }
                    else
                    {
                        var res = Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Usuário e/ou senha inválido(s).");
                        return res;
                    }
                }
                else
                {
                    var res = Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Usuário não autorizado.");
                    return res;
                }
            }
            catch (Exception ex)
            {
                var res = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Ocorreu um erro enquanto o servidor realizava operação, por favor tente novamente, se persistir entre em contato com o administrador do sistema.", ex);
                return res;
            }
        }

        [Authorize]
        [HttpGet]
        [Route("api/maladiretafunaf")]
        public HttpResponseMessage MalaDireta()
        {
            try
            {
                var emails = new List<Email>();
                var teste = new Email()
                {
                    txCartorio = "PGE-RN",
                    txComarca = "Natal",
                    txEmail = "kellyson.victor@gmail.com",
                    txNome = "Kellyson",
                    txSenha = "123",
                    txUsuario = "05476161473"
                };

               
                emails = DBEmail.RecuperarDadosParaEnviarLoginESenhaTodos();
                foreach (var iEmail in emails)
                {
                    try
                    {
                        var msg = new PGEMailProxy.Entities.Email();
                        msg.ConfigurarServidor();
                        msg.AdicionarRemetente("Procuradoria Geral do Estado do Rio Grande do Norte - Módulo FUNAF", "suportepge@gmail.com");
                        msg.AdicionarDestinatario(iEmail.txNome, iEmail.txEmail);

                        var mensagem = "<table width='550' border='0' align='center' cellpadding='0' cellspacing='10'> <tbody> <tr> <td> <table width='530' border='0' cellspacing='0' cellpadding='0'> <tbody> <tr> <td colspan='2'> <table width='530' border='0' cellspacing='10' cellpadding='0'> <tbody> <tr> <td height='65' valign='top'> <table width='510' border='0' cellspacing='0' cellpadding='0'> <tbody> <tr> <td width='100'> <a href='http://nucleo.pge.rn.gov.br' target='_blank'> <img src='http://nucleo.pge.rn.gov.br/funafangular/assets/images/brasao.png' width='100' height='93' border='0'> </a> </td> <td width='410' align='center'> <span>Módulo de Declarações do FUNAF</span> <br> <span>Credenciais de Acesso</span> </td> </tr> </tbody> </table> </td> </tr> <tr> <td> <table width='510' border='0' cellspacing='0' cellpadding='0'> <tbody> <tr> <td height='4'> <strong>@Serventia</strong> </td> </tr> <tr> <td height='4'>Comarca: <strong>@Comarca</strong> </td> </tr> <tr> <td height='4'> <p align='justify'> <br>Prezado(a) Sr(a), <strong>@Responsavel</strong> <br> <br> <span>Através do presente informamos que o cadastramento para acesso à área restrita, na qual estará disponível ferramenta eletrônica para preenchimento da DPSE e geração da guia de recolhimento da taxa em favor do FUNAF (art. 4° da Instrução Normativa n° 001/2018 – GPGE) foi realizada com sucesso.</span> <br> <br> <span>O acesso deve ser realizado mediante a utilização do seguinte usuário e senha: </span> <br> <br> <span>Usuário: <strong>@CPF</strong> </span> <br> <br> <span>Senha: <strong>@Senha</strong> </span> <br> <br> <strong>Para acessar o sistema, Vossa Senhoria deve abrir a página da PGE ( <a href='http://www.pge.rn.gov.br'>www.pge.rn.gov.br</a>) e clicar no botão verde Serviços para o Contribuinte. Em seguinda, escolher a opção <a href='http://nucleo.pge.rn.gov.br/funafangular/login'>Área Restrita - Notários e Regitradores</a>. </strong> <br> <br> <span>Atenciosamente,</span> <br> <br> <span>Sergio Badiali</span> <br> <span>Chefe da Divisão de Informática da PGE/RN</span> </p> </td> </tr> <tr> <td> <br> <br> <strong>Este é um e-mail gerado automaticamente. Por favor, não responda.</strong> </td> </tr> </tbody> </table> </td> </tr> </tbody> </table> </td> </tr> </tbody> </table> </td> </tr> </tbody> </table>";

                        mensagem = mensagem.Replace("@Serventia", iEmail.txCartorio);
                        mensagem = mensagem.Replace("@Comarca", iEmail.txComarca.Replace("-", " ") + "/RN");
                        mensagem = mensagem.Replace("@Responsavel", iEmail.txNome);
                        mensagem = mensagem.Replace("@CPF", iEmail.txUsuario);
                        mensagem = mensagem.Replace("@Senha", iEmail.txSenha);

                        msg.AdicionarMensagem("Programa Declarações de Procedimentos e Serviços Efetuados - FUNAF ", mensagem, true);

                        MailProxy.Enviar(msg);

                        DBEmail.ConfirmarEnvio(iEmail.txEmail);
                    }
                    catch (Exception ex)
                    {
                        var log = new Domain.Module.Lancamentos.Dominio.AppLog()
                        {
                            txMensagem = string.Format("{0}/{1}", iEmail.txUsuario, ex.Message),
                            txStack = ex.StackTrace,
                            nuNotificarDBA = 0
                        };
                        DBAppLog.Save(log);
                    }
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Erro ao tentar Consultar Todos.", ex);
            }
        }



        [HttpPost]
        [AllowAnonymous]
        [Route("api/usuario/recuperar-senha")]
        public HttpResponseMessage RecuperarSenha([FromBody] dynamic model)
        {
            try
            {
                string cpfOuEmail = Convert.ToString(model.cpfOuEmail);
                var usuario = DBUsuario.PesquisarPorCpfOuEmail(cpfOuEmail);
                if (usuario == null)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Usuário não encontrado.");

                string senhaProvisoria = GerarSenhaProvisoria();
                DBUsuario.AlterarSenha(usuario.Id, senhaProvisoria);

               
                EnviarEmailRecuperacao(usuario.txEmail, usuario.txNome, senhaProvisoria);

                return Request.CreateResponse(HttpStatusCode.OK, "Senha provisória enviada por e-mail.");
            }
            catch (Exception ex)
            {
               
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        } 
        
        private void EnviarEmailRecuperacao(string email, string nome,
            string senha)
        {
            var msg = new PGEMailProxy.Entities.Email();
            msg.ConfigurarServidor();
            msg.AdicionarRemetente("Sistema FUNAF", "informatica@pge.rn.gov.br");
            msg.AdicionarDestinatario("Teste", "nelson1820@gmail.com");

            string corpo = $@"
        <p>Olá {nome},</p>
        <p>Sua senha provisória é: <strong>{senha}</strong></p>
        <p>Por favor, troque a senha após o login.</p>
        <p>Atenciosamente,<br/>Equipe FUNAF</p>";

            msg.AdicionarMensagem("Recuperação de Senha", corpo, true);

            try
            {
                MailProxy.Enviar(msg);
            }
            catch (Exception sendEx)
            {
                throw new ApplicationException("Falha ao enviar e-mail: " + sendEx.Message);
            }
        }

        private string GerarSenhaProvisoria()
        {
            return Guid.NewGuid()
                       .ToString("N")
                       .Substring(0, 8);
        }

        
        }
    } 
