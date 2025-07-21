using Funaf.Domain.Module.Lancamentos.Dominio;
using PGEDBBroker.Mapping;
using RemessaBB.Domain;
using System;

namespace RemessaBB.Funaf.Domain
{
    [Serializable]
    [Mapeamento(NomeDaChavePrimaria = "idRemessaBoletos", NomeDaTabela = "tbRemessaBB_Boletos")]
    public class RemessaBB_Boletos5480
    {
        public int id { get; set; }
        public RemessaBB5480 RemessaBB { get; set; }
        public Boleto Boleto { get; set; }
        public DateTime dtVencimento { get; set; }
        public Comando Comando { get; set; }
        public Serventia Serventia { get; set; }
        public int nuDiasProtesto { get; set; }
        public int? nuCodRetorno { get; set; }
        public DateTime? dtRetorno { get; set; }
    }
}