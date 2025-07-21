using DBBroker.Mapping;
using System;

namespace RemessaBB.FUNAF.Domain
{
    [Serializable]
    [DBMappedClass(PrimaryKey = "idRemessaBB", Table = "tbRemessaBB")]
    public class RemessaBB
    {
        public int Id { get; set; }
        public string txArquivo { get; set; }
        public DateTime dtProcessamento { get; set; }
        public string usuario { get; set; }
        public int nuTipoRemessa { get; set; }
        public int nuSequencial { get; set; }
    }
}