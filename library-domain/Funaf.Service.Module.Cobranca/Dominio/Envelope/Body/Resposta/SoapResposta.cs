using System;
using System.Xml.Serialization;

namespace Funaf.Service.Module.Cobranca.Dominio.Envelope.Body.Resposta
{
    [Serializable]
    public class SoapResposta
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

        [XmlElement("textoNumeroTituloCobrancaBb", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public string textoNumeroTituloCobranca { get; set; }

        [XmlElement("numeroCarteiraCobranca", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public int numeroCarteiraCobranca { get; set; }

        [XmlElement("numeroVariacaoCarteiraCobranca", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public int numeroVariacaoCarteiraCobranca { get; set; }

        [XmlElement("codigoPrefixoDependenciaBeneficiario", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public int codigoPrefixoDependenciaBeneficiario { get; set; }

        [XmlElement("numeroContaCorrenteBeneficiario", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public int numeroContaCorrenteBeneficiario { get; set; } //5011 ou 5480

        [XmlElement("codigoCliente", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public int codigoCliente { get; set; }

        [XmlElement("linhaDigitavel", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public string linhaDigitavel { get; set; }

        [XmlElement("codigoBarraNumerico", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public string codigoBarraNumerico { get; set; }

        [XmlElement("codigoTipoEnderecoBeneficiario", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public int codigoTipoEnderecoBeneficiario { get; set; }

        [XmlElement("nomeLogradouroBeneficiario", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public string nomeLogradouroBeneficiario { get; set; }

        [XmlElement("nomeBairroBeneficiario", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public string nomeBairroBeneficiario { get; set; }

        [XmlElement("nomeMunicipioBeneficiario", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public string nomeMunicipioBeneficiario { get; set; }

        [XmlElement("codigoMunicipioBeneficiario", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public int codigoMunicipioBeneficiario { get; set; }

        [XmlElement("siglaUfBeneficiario", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public string siglaUfBeneficiario { get; set; }

        [XmlElement("codigoCepBeneficiario", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public int codigoCepBeneficiario { get; set; }

        [XmlElement("indicadorComprovacaoBeneficiario", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public string indicadorComprovacaoBeneficiario { get; set; }

        [XmlElement("numeroContratoCobranca", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public int numeroContratoCobranca { get; set; }
    }
}
