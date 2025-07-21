using DBBroker.Mapping;
using Funaf.Domain.Module.Lancamentos.Dominio;
using System;

namespace RemessaBB.FUNAF.Domain
{
    [Serializable]
    [DBMappedClass(PrimaryKey = "idRemessaBoletos", Table = "tbRemessaBB_Boletos")]
    public class RemessaBB_Boletos
    {
        public int Id { get; set; }
        public RemessaBB RemessaBB { get; set; }
        public Boleto Boleto { get; set; }
        public DateTime dtVencimento { get; set; }
        public Comando Comando { get; set; }
        public Serventia Serventia { get; set; }
        public int nuDiasProtesto { get; set; }
        public int? nuCodRetorno { get; set; }
        public DateTime? dtRetorno { get; set; }
    }
}