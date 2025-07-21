using DBBroker.Mapping;

namespace Funaf.Domain.Module.Lancamentos.Dominio.Sitad
{
    [DBMappedClass(PrimaryKey = "idrelatorio", Table = "tbRelatorio")]
    public class RelSigef
    {
        public string txTributo { get; set; }
        public string txContribuinte { get; set; }
        public string txIdentificador { get; set; }
        public string txNumeroOrigem { get; set; }
        public string txNossoNumero { get; set; }
        public string dtPagamento { get; set; }
        public decimal vaPrincipal { get; set; }
        public decimal vaMulta { get; set; }
        public decimal vaJuros { get; set; }
        public decimal vaHonorarios { get; set; }
        public decimal vaPago { get; set; }        
    }
}
