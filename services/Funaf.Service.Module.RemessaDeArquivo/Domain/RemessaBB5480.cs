using PGEDBBroker.Mapping;
using System;

namespace RemessaBB.Funaf.Domain
{
    [Serializable]
    [Mapeamento(NomeDaChavePrimaria = "idRemessaBB", NomeDaTabela = "tbRemessaBB")]
    public class RemessaBB5480
    {
        public int id { get; set; }
        public string txArquivo { get; set; }
        public DateTime dtProcessamento { get; set; }
        public string usuario { get; set; }
        public int nuTipoRemessa { get; set; }
        public int nuSequencial { get; set; }
    }
}