using DBBroker.Mapping;

namespace Funaf.Domain.Module.Lancamentos.Dominio
{
    [DBMappedClass(PrimaryKey = "idrelatorio", Table = "tbRelatorio")]
    public class RelFichaCadastral
    {
        public string txCartorio { get; set; }
        public string txEndereco { get; set; }
        public string txCEP { get; set; }
        public string txTelefone { get; set; }
        public string txEmail { get; set; }
        public string txResponsavel { get; set; }
    }
}
