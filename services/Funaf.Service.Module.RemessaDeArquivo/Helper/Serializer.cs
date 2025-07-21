using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace RemessaBB.FUNAF.Helper
{
    public static class Serializer
    {
        //DESERIALIZANDO XML - CONVERTENDO EM OBJETO
        public static T Deserialize<T>(this XElement xElement)
        {
            using (var memoryStream = new MemoryStream(Encoding.ASCII.GetBytes(xElement.ToString())))
            {
                var xmlSerializer = new XmlSerializer(typeof(T));
                return (T)xmlSerializer.Deserialize(memoryStream);
            }
        }

        //SERIALIZANDO OBJETOS - CONVERTENDO EM XML
        public static XElement Serialize<T>(this object o)
        {
            Encoding encoding = Encoding.GetEncoding("ISO-8859-1");
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            using (var memoryStream = new MemoryStream())
            {
                using (TextWriter streamWriter = new StreamWriter(memoryStream))
                {
                    var xmlSerializer = new XmlSerializer(typeof(T));
                    xmlSerializer.Serialize(streamWriter, o, ns);
                    return XElement.Parse(Encoding.ASCII.GetString(memoryStream.ToArray()));
                }
            }
        }
    }
}