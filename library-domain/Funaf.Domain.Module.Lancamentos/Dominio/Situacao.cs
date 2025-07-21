using DBBroker.Mapping;

namespace Funaf.Domain.Module.Lancamentos.Dominio
{
    [DBMappedClass(Table = "dbo.tbSituacoes", PrimaryKey = "idSituacao")]
    public class Situacao
    {
        public int Id { get; set; }

        public string txSituacao { get; set; }
    }
}
