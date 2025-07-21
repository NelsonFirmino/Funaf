using System;
using System.Net;
using Newtonsoft.Json;
using System.Text;
using System.IO;
using System.Collections.Specialized;
using System.Xml.Serialization;
using Funaf.Service.Module.Cobranca.Dominio.Helper;
using Funaf.Service.Module.Cobranca.Dominio;
using System.Xml;
using Funaf.Service.Module.Cobranca.Dominio.Envelope;
using Funaf.Service.Module.Cobranca.Persistencia;
using Funaf.Service.Module.Cobranca.Dominio.Exceptions;

namespace Funaf.Service.Module.Cobranca.Service
{
    public struct CodigoDeBarrasBoleto
    {
        public string CodigoBarraNumerico;
        public string LinhaDigitavel;
    }

    /// <summary>
    /// Implementação do serviço de registro de boletos do BB
    /// </summary>
    public static class RegistrarBoletoService
    {
        private static string URL_REGISTRAR_BOLETO = @"URL_REGISTRAR_BOLETO";
        private static string URL_GERAR_TOKEN = @"URL_GERAR_TOKEN";
        private static string CLIENT_ID = @"CLIENT_ID";
        private static string CLIENT_SECRET = @"CLIENT_SECRET";

        /// <summary>
        /// Inicializar as confirgurações
        /// </summary>
        static RegistrarBoletoService()
        {
            try
            {
                URL_GERAR_TOKEN = URL_GERAR_TOKEN.ReadConfig();
                URL_REGISTRAR_BOLETO = URL_REGISTRAR_BOLETO.ReadConfig();
                CLIENT_ID = CLIENT_ID.ReadConfig();
                CLIENT_SECRET = CLIENT_SECRET.ReadConfig();
            }
            catch (Exception ex)
            {
                throw new CobrancaModuleException("O sistema não conseguiu carregar as configurações iniciais, verifique seu arquivo config", ex);
            }
        }

        /// <summary>
        /// Realizar o procedimento de gerar token e registrar o boleto no serviço dp BB.
        /// </summary>
        /// <param name="Titulo">Dados do boleto</param>
        /// <param name="Pagador">Dados do devedor</param>
        /// <returns>Retorna a linha digitavel do boleto</returns>
        public static CodigoDeBarrasBoleto Invoke(TituloPGE Titulo, PagadorPGE Pagador)
        {
            CodigoDeBarrasBoleto codigoDeBarras;
            var registroBoletoWS = new RegistroBBWSBoleto();


            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback += (mender, certificate, chain, sslPolicyErrors) => true;




#if DEBUG
            if (!Directory.Exists(@"C:/Temp/"))
                throw new CobrancaModuleException("Antes de efetuar o debug, crie o diretorio c:/temp/");
#endif

            // _textoNumeroTituloCliente = "000" + _numeroConvenio.ToString().PadLeft(7, '0') + textoNumeroTituloCliente.ToString().PadLeft(10, '0');

            if (Titulo.TextoNumeroTituloCliente.Length == 20)
            {
                var idRegistroBBWSBoleto = int.Parse(Titulo.TextoNumeroTituloCliente.Substring(11));

                registroBoletoWS = DBRegistroBBWSBoleto.GetById(idRegistroBBWSBoleto);
            }

            if (string.IsNullOrEmpty(registroBoletoWS.txCodigoBarraNumerico) || string.IsNullOrEmpty(registroBoletoWS.txLinhaDigitavel))
            {

                var token = new Token();

                try
                {
                    token = GerarToken();
                }
                catch (Exception ex)
                {
                    throw new CobrancaModuleException("Erro ao gerar o token" + ex.Message, ex);
                }

                var xml = PrepararRequisicao(Titulo, Pagador);
                var envelope = PostResquest(token, xml);

                var idRegistroBoleto = 0;
                int.TryParse(Titulo.TextoNumeroTituloCliente.Substring(10), out idRegistroBoleto);
                var msgErro = envelope.Body.resposta.textoMensagemErro;

                try
                {
#if DEBUG
                    // Salvar o arquivo XML na pasta Temp com a requisição no diretorio de saída da aplicação
                    File.WriteAllText(@"C:/Temp/" + envelope.Body.requisicao.textoNumeroTituloCliente + ".xml", envelope.ToXML());
                    //idRegistroBoleto = (idRegistroBoleto / 10730);
#endif

                    if (msgErro.Contains("Titulo ja incluido anteriormente"))
                        throw new TituloJaRegistradoException("Titulo ja registrado");

                    if (msgErro.Contains("CPF do pagador nao encontrado na base"))
                        throw new CPFCNPJInvalido("Titulo ja registrado");

                    // Se a linha digitavel retornada for vazia, lançar exceção e envia mensagem para o cliente tratar
                    if (string.IsNullOrWhiteSpace(envelope.Body.resposta.linhaDigitavel))
                    {
                        var error = new RegistroBBWSErro()
                        {
                            RegistroBBWSBoleto = new RegistroBBWSBoleto() { Id = idRegistroBoleto },
                            txSiglaSistemaMsg = envelope.Body.resposta.siglaSistemaMensagem,
                            nuRetornoPrograma = envelope.Body.resposta.codigoRetornoPrograma,
                            txNomePrograma = envelope.Body.resposta.nomeProgramaErro,
                            txMensagem = msgErro,
                            nuPosicaoErro = envelope.Body.resposta.numeroPosicaoErroPrograma,
                            nuTipoRetorno = envelope.Body.resposta.codigoTipoRetornoPrograma,
                            xmlRequest = xml,
                            xmlResponse = JsonConvert.SerializeObject(envelope.Body.resposta)
                        };

                        File.WriteAllText(@"C:/Temp/" + envelope.Body.requisicao.textoNumeroTituloCliente + ".xml", envelope.ToXML());

                        DBRegistroBBWSErro.Save(error);

                        throw new CobrancaModuleException(msgErro, new Exception(JsonConvert.SerializeObject(envelope.Body.resposta)));
                    }

                    registroBoletoWS = DBRegistroBBWSBoleto.GetById(idRegistroBoleto);

                    registroBoletoWS.txLinhaDigitavel = envelope.Body.resposta.linhaDigitavel.Trim();
                    registroBoletoWS.txNossoNumero = envelope.Body.resposta.textoNumeroTituloCobranca.Trim();
                    registroBoletoWS.txCodigoBarraNumerico = envelope.Body.resposta.codigoBarraNumerico.Trim();

                    // Add o nosso numero formatado e a linha digital ao registro do banco de dados
                    if (idRegistroBoleto > 0)
                        DBRegistroBBWSBoleto.Save(registroBoletoWS);
                }
                catch (TituloJaRegistradoException)
                {
                    registroBoletoWS = DBRegistroBBWSBoleto.GetById(idRegistroBoleto);
                }
                catch (CPFCNPJInvalido)
                {
                    throw new CobrancaModuleException(msgErro, new Exception(JsonConvert.SerializeObject(envelope.Body.requisicao)));
                }
                catch (Exception ex)
                {
                    throw new CobrancaModuleException(ex.Message, new Exception(JsonConvert.SerializeObject(envelope)));
                }
            }


            codigoDeBarras.CodigoBarraNumerico = registroBoletoWS.txCodigoBarraNumerico;
            codigoDeBarras.LinhaDigitavel = registroBoletoWS.txLinhaDigitavel;

            return codigoDeBarras;
        }

        /// <summary>
        /// Implementação de atenticação OATH2.
        /// </summary>
        /// <returns>Retonar token do tipo Bearer</returns>
        private static Token GerarToken()
        {
            var token = new Token();

            try
            {
                var json = string.Empty;
                var credenciais = string.Format("{0}:{1}", CLIENT_ID, CLIENT_SECRET);
                credenciais = credenciais.ToBase64Encode();

                var webRequest = (HttpWebRequest)WebRequest.Create(URL_GERAR_TOKEN);
                webRequest.Headers.Add("Authorization", "Basic " + credenciais);
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.Method = "POST";

                var parameters = new NameValueCollection();
                parameters.Add("grant_type", "client_credentials");
                parameters.Add("scope", "cobranca.registro-boletos");

                var data = new StringBuilder();
                foreach (string key in parameters)
                    data.AppendFormat("{0}={1}&", key, parameters[key]);
                data.Length -= 1;

                using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
                    streamWriter.Write(data.ToString());

                var webResponse = webRequest.GetResponse() as HttpWebResponse;

                using (var responseStream = webResponse.GetResponseStream())
                {
                    var reader = new StreamReader(responseStream, Encoding.UTF8);
                    json = reader.ReadToEnd();
                }

                token = JsonConvert.DeserializeObject<Token>(json);
            }
            catch (Exception ex)
            {
                throw new CobrancaModuleException("O sistema não conseguiu gerar o token", ex);
            }

            return token;
        }

        /// <summary>
        /// Realizar chamada ao serviço que registrar o boleto
        /// </summary>
        /// <param name="token">Token tipo Bearer</param>
        /// <param name="xml">Dados da requisição</param>
        /// <returns>XMl com o resultado da requisição</returns>
        private static SoapEnvelope PostResquest(Token token, string xml)
        {
            var result = new SoapEnvelope();

            var xSerRequest = new XmlSerializer(typeof(SoapEnvelope));
            var seRequest = (SoapEnvelope)xSerRequest.Deserialize(new StringReader(xml));

            var authToken = string.Format("{0} {1}", token.token_type, token.access_token);
            var webRequest = (HttpWebRequest)WebRequest.Create(URL_REGISTRAR_BOLETO);

            try
            {
                webRequest.Method = "POST";
                webRequest.ContentType = "text/xml;charset=utf-8";
                webRequest.Headers.Add("SOAPAction", "registrarBoleto");
                webRequest.Headers.Add("Authorization", authToken);

                using (var stream = webRequest.GetRequestStream())
                {
                    using (var w = new StreamWriter(stream))
                    {
                        w.Write(xml);
                    }
                }

                using (var webResponse = (HttpWebResponse)webRequest.GetResponse())
                {
                    if (webResponse.StatusCode == HttpStatusCode.OK)
                    {
                        using (var s = webResponse.GetResponseStream())
                        {
                            var reader = new StreamReader(s, Encoding.UTF8);

                            var serializer = new XmlSerializer(typeof(SoapEnvelope));

                            using (var sr = new StringReader(reader.ReadToEnd()))
                            {
                                result = (SoapEnvelope)serializer.Deserialize(sr);
                            }
                        }
                    }
                    else
                    {
                        var responseString = string.Empty;

                        var res = webResponse.GetResponseStream();

                        using (var sr = new StreamReader(res, true))
                        {
                            responseString += sr.ReadToEnd();
                        }

                        throw new CobrancaModuleException("O serviço não conseguiu registrar o boleto. " + responseString);
                    }
                }

                result.Body.requisicao = seRequest.Body.requisicao;
            }
            catch (Exception ex)
            {
                throw new CobrancaModuleException("Erro 500. Parametros invalidos no serviço de boletos. " + ex.Message, ex);
            }

            return result;
        }

        /// <summary>
        /// Montagem do XML para envio na requisição de registrar boleto
        /// </summary>
        /// <param name="titulo"></param>
        /// <param name="pagador"></param>
        /// <returns></returns>
        public static string PrepararRequisicao(TituloPGE titulo, PagadorPGE pagador)
        {
            var xml = string.Empty;

            try
            {
                var soapEnvelope = new SoapEnvelope();
                soapEnvelope.Body.requisicao.numeroConvenio = titulo.NumeroConvenio;
                soapEnvelope.Body.requisicao.dataEmissaoTitulo = titulo.DataEmissaoTitulo.ToString("dd.MM.yyyy");
                soapEnvelope.Body.requisicao.dataVencimentoTitulo = titulo.DataVencimentoTitulo.ToString("dd.MM.yyyy");
                soapEnvelope.Body.requisicao.valorOriginalTitulo = titulo.ValorOriginalTitulo;
                soapEnvelope.Body.requisicao.codigoTipoTitulo = titulo.CodigoTipoTitulo;
                soapEnvelope.Body.requisicao.textoNumeroTituloBeneficiario = titulo.IdBoleto.ToString();
                soapEnvelope.Body.requisicao.textoDescricaoTipoTitulo = titulo.TexoDescricaoTipoTitulo;
                soapEnvelope.Body.requisicao.textoNumeroTituloCliente = titulo.TextoNumeroTituloCliente;
                soapEnvelope.Body.requisicao.textoMensagemBloquetoOcorrencia = titulo.TextoMensagemBloquetoOcorrencia;

                soapEnvelope.Body.requisicao.codigoTipoInscricaoPagador = pagador.CodigoTipoInscricaoPagador;
                soapEnvelope.Body.requisicao.numeroInscricaoPagador = pagador.NumeroInscricaoPagador;
                soapEnvelope.Body.requisicao.nomePagador = pagador.NomePagador;
                soapEnvelope.Body.requisicao.textoEnderecoPagador = pagador.TextoEnderecoPagador;
                soapEnvelope.Body.requisicao.numeroCepPagador = pagador.NumeroCepPagador;
                soapEnvelope.Body.requisicao.nomeMunicipioPagador = pagador.NomeMunicipioPagador;
                soapEnvelope.Body.requisicao.nomeBairroPagador = pagador.NomeBairroPagador;
                soapEnvelope.Body.requisicao.siglaUfPagador = pagador.SiglaUfPagador;

                var serializer = new XmlSerializer(typeof(SoapEnvelope));
                var ns = new XmlSerializerNamespaces();
                ns.Add("soapenv", "http://schemas.xmlsoap.org/soap/envelope/");
                ns.Add("sch", "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd");

                var settings = new XmlWriterSettings
                {
                    Indent = true,
                    Encoding = Encoding.GetEncoding("UTF-8"),
                    OmitXmlDeclaration = true
                };

                var sww = new StringWriterWithEncoding(Encoding.UTF8);
                var writer = XmlWriter.Create(sww, settings);
                serializer.Serialize(writer, soapEnvelope, ns);
                xml = sww.ToString();

                xml = xml.RemoveAccents();

            }
            catch (Exception ex)
            {
                throw new CobrancaModuleException(ex.Message, ex);
            }

            return xml;
        }
    }
}
