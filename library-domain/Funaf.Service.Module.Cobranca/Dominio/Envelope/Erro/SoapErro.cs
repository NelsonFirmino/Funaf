using System;
using System.Xml.Serialization;

namespace Funaf.Service.Module.Cobranca.Dominio.Envelope.Body.Erro
{
    [Serializable]
    public class Erro
    {
        [XmlElement("siglaSistemaMensagem", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public string siglaSistemaMensagem { get; set; }

        [XmlElement("codigoRetornoPrograma", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public int codigoRetornoPrograma { get; set; }

        [XmlElement("nomeProgramaErro", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public string nomeProgramaErro { get; set; }

        [XmlElement("textoMensagemErro", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public string textoMensagemErro { get; set; }

        [XmlElement("numeroPosicaoErroPrograma", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public int numeroPosicaoErroPrograma { get; set; }

        [XmlElement("codigoTipoRetornoPrograma", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public int codigoTipoRetornoPrograma { get; set; }
    }
}
