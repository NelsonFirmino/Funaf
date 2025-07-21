using System;
using System.Xml.Serialization;
using Funaf.Service.Module.Cobranca.Dominio.Envelope.Body;

namespace Funaf.Service.Module.Cobranca.Dominio.Envelope
{
    [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    [Serializable]
    public class SoapEnvelope
    {
        [XmlElement(ElementName = "Header", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public SoapHeader Header { get; set; }

        [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public SoapBody Body { get; set; }

        public SoapEnvelope()
        {
            Header = new SoapHeader();
            Body = new SoapBody();
        }

        public string ToXML()
        {
            using (var stringwriter = new System.IO.StringWriter())
            {
                var serializer = new XmlSerializer(GetType());
                serializer.Serialize(stringwriter, this);
                return stringwriter.ToString();
            };
        }
    }
}
