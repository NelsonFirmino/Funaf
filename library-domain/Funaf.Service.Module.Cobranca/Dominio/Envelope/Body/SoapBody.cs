using System;
using System.Xml.Serialization;
using Funaf.Service.Module.Cobranca.Dominio.Envelope.Body.Resposta;
using Funaf.Service.Module.Cobranca.Dominio.Envelope.Body.Requisicao;

namespace Funaf.Service.Module.Cobranca.Dominio.Envelope.Body
{
    [Serializable]
    public class SoapBody
    {
        [XmlElement(ElementName = "requisicao", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public SoapRequisicao requisicao { get; set; }

        [XmlElement(ElementName = "resposta", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public SoapResposta resposta { get; set; }

        public SoapBody()
        {
            requisicao = new SoapRequisicao();
            resposta = new SoapResposta();
        }
    }
}