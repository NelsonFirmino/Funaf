using DBBroker.Mapping;

namespace Funaf.Domain.Module.Lancamentos.Dominio
{
    [DBMappedClass(PrimaryKey = "idrelatorio", Table = "tbRelatorio")]
    public class RelDevolucaoSolicitacao
    {
        public string txNumero { get; set; }
        public string txMatriculaResponsavel { get; set; }
        public string txResponsavel { get; set; }
        public string txSolicitante { get; set; }
        public string txTipoBusca { get; set; }
        public string txTipoDevolucao { get; set; }
        public string dtSolicitacao { get; set; }
        public string dtDecisao { get; set; }
        public string dtCiencia { get; set; }
        public string txAssunto { get; set; }
    }
}
