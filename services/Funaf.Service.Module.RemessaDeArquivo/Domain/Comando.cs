using DBBroker.Mapping;
using System;

namespace RemessaBB.FUNAF.Domain
{
    [Serializable]
    [DBMappedClass(PrimaryKey = "idComando", Table = "tbRemessaBB_Comandos")]
    public class Comando
    {
        public int Id { get; set; }
        public string txComando { get; set; }
    }
}