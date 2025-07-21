using DBBroker.Mapping;
using System;

namespace Funaf.Domain.Module.Lancamentos.Dominio
{ 
    [DBMappedClass(PrimaryKey = "idrelatorio", Table = "tbRelatorio")]
    public class RelResumo
    {
        public string txGrupo { get; set; }
        public int nuOrdem { get; set; }
        public string txServico { get; set; }
        public string txComarca { get; set; }
        public string txCartorio { get; set; }
        public string txResponsavel { get; set; }
        public string txCpf { get; set; }
        public DateTime dtCadastro { get; set; }
        public DateTime dtPeriodoInicial { get; set; }
        public DateTime dtPeriodoFinal { get; set; }
        public decimal vaServico { get; set; }
        public decimal nuQuantidade { get; set; }
        public decimal vaTotal { get; set; }
    }
}