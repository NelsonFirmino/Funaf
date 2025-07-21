using System;
using System.Xml;
using System.Xml.Serialization;

namespace Funaf.Service.Module.Cobranca.Dominio.Envelope.Body.Requisicao
{
    [Serializable]
    public class SoapRequisicao
    {
        [XmlElement("numeroConvenio", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public int numeroConvenio { get; set; }

        [XmlElement("numeroCarteira", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public short numeroCarteira { get; set; }

        [XmlElement("numeroVariacaoCarteira", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public short numeroVariacaoCarteira { get; set; }

        [XmlElement("codigoModalidadeTitulo", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public short codigoModalidadeTitulo { get; set; }

        [XmlElement("dataEmissaoTitulo", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public string dataEmissaoTitulo { get; set; }

        [XmlElement("dataVencimentoTitulo", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public string dataVencimentoTitulo { get; set; }

        [XmlElement("valorOriginalTitulo", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public decimal valorOriginalTitulo { get; set; }

        [XmlElement("codigoTipoDesconto", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public short codigoTipoDesconto { get; set; }

        [XmlElement("dataDescontoTitulo", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public string dataDescontoTitulo { get; set; }

        [XmlElement("percentualDescontoTitulo", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public decimal percentualDescontoTitulo { get; set; }

        [XmlElement("valorDescontoTitulo", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public decimal valorDescontoTitulo { get; set; }

        [XmlElement("valorAbatimentoTitulo", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public decimal valorAbatimentoTitulo { get; set; }

        [XmlElement("quantidadeDiaProtesto", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public short quantidadeDiaProtesto { get; set; }

        [XmlElement("codigoTipoJuroMora", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public short codigoTipoJuroMora { get; set; }

        [XmlElement("percentualJuroMoraTitulo", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public decimal percentualJuroMoraTitulo { get; set; }

        [XmlElement("valorJuroMoraTitulo", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public decimal valorJuroMoraTitulo { get; set; }

        [XmlElement("codigoTipoMulta", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public short codigoTipoMulta { get; set; }

        [XmlElement("dataMultaTitulo", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public string dataMultaTitulo { get; set; }

        [XmlElement("percentualMultaTitulo", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public decimal percentualMultaTitulo { get; set; }

        [XmlElement("valorMultaTitulo", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public decimal valorMultaTitulo { get; set; }

        [XmlElement("codigoAceiteTitulo", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public string codigoAceiteTitulo { get; set; }

        [XmlElement("codigoTipoTitulo", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public short codigoTipoTitulo { get; set; } // 24 PARA DIVIDA -  ? FUNAF

        [XmlElement("textoDescricaoTipoTitulo", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public string textoDescricaoTipoTitulo { get; set; } //PGE-DIVIDA ATIVA // FUNAF

        [XmlElement("indicadorPermissaoRecebimentoParcial", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public string indicadorPermissaoRecebimentoParcial { get; set; }

        [XmlElement("textoNumeroTituloBeneficiario", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public string textoNumeroTituloBeneficiario { get; set; }

        [XmlElement("textoCampoUtilizacaoBeneficiario", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public string textoCampoUtilizacaoBeneficiario { get; set; }

        [XmlElement("codigoTipoContaCaucao", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public short codigoTipoContaCaucao { get; set; }

        [XmlElement("textoNumeroTituloCliente", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public string textoNumeroTituloCliente { get; set; }

        [XmlElement("textoMensagemBloquetoOcorrencia", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public string textoMensagemBloquetoOcorrencia { get; set; }

        [XmlElement("codigoTipoInscricaoPagador", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public short codigoTipoInscricaoPagador { get; set; }

        [XmlElement("numeroInscricaoPagador", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public string numeroInscricaoPagador { get; set; }

        [XmlElement("nomePagador", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public string nomePagador { get; set; }

        [XmlElement("textoEnderecoPagador", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public string textoEnderecoPagador { get; set; }

        [XmlElement("numeroCepPagador", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public int numeroCepPagador { get; set; }

        [XmlElement("nomeMunicipioPagador", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public string nomeMunicipioPagador { get; set; }

        [XmlElement("nomeBairroPagador", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public string nomeBairroPagador { get; set; }

        [XmlElement("siglaUfPagador", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public string siglaUfPagador { get; set; }

        [XmlElement("textoNumeroTelefonePagador", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public string textoNumeroTelefonePagador { get; set; }

        [XmlElement("codigoTipoInscricaoAvalista", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public short codigoTipoInscricaoAvalista { get; set; }

        [XmlElement("numeroInscricaoAvalista", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public long numeroInscricaoAvalista { get; set; }

        [XmlElement("nomeAvalistaTitulo", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public string nomeAvalistaTitulo { get; set; }

        [XmlElement("codigoChaveUsuario", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public string codigoChaveUsuario { get; set; }


        [XmlElement("codigoTipoCanalSolicitacao", Namespace = "http://www.tibco.com/schemas/bws_registro_cbr/Recursos/XSD/Schema.xsd")]
        public short codigoTipoCanalSolicitacao { get; set; }

        public SoapRequisicao()
        {
            codigoChaveUsuario = "J1234567";
            codigoTipoCanalSolicitacao = 5;
            numeroCarteira = 17;
            numeroVariacaoCarteira = 19;
            nomeAvalistaTitulo = string.Empty;
            numeroInscricaoAvalista = 0;
            codigoTipoInscricaoAvalista = 0;
            textoNumeroTelefonePagador = string.Empty;
            textoMensagemBloquetoOcorrencia = "";
            codigoTipoContaCaucao = 0;
            textoCampoUtilizacaoBeneficiario = "";
            indicadorPermissaoRecebimentoParcial = "N";
            codigoAceiteTitulo = "A";
            valorMultaTitulo = 0m;
            percentualMultaTitulo = 0m;
            dataMultaTitulo = "";
            codigoModalidadeTitulo = 1;
            codigoTipoDesconto = 0;
            dataDescontoTitulo = string.Empty;
            percentualDescontoTitulo = 0m;
            valorDescontoTitulo = 0m;

            valorAbatimentoTitulo = 0;
            quantidadeDiaProtesto = 0;
            codigoTipoJuroMora = 0;
            percentualJuroMoraTitulo = 0;
            valorJuroMoraTitulo = 0;
            codigoTipoMulta = 0;
        }

        public static implicit operator SoapRequisicao(XmlDocument v)
        {
            throw new NotImplementedException();
        }
    }
}
