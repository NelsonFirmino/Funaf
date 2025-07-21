using DBBroker.Mapping;
using System;

namespace Funaf.Domain.Module.Lancamentos.Dominio
{
    [DBMappedClass(PrimaryKey = "IdDeclaracao", Table = "tbRelatorio")]
    public class RelRecolhimento
    {
        public int idDeclaracao { get; set; }
        public string txCartorio { get; set; }
        public string txResponsavel { get; set; }
        public string txNossoNumero { get; set; }
        public DateTime dtLiquidacao { get; set; }
        public decimal vaPrincipal { get; set; }
        public decimal vaMulta { get; set; }
        public decimal vaJuros { get; set; }
        public decimal vaPago { get; set; }
        public DateTime dtCredito { get; set; }


    }
}
