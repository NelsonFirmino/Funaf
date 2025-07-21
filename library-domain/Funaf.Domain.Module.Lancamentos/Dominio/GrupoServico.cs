using DBBroker.Mapping;

namespace Funaf.Domain.Module.Lancamentos.Dominio
{
    [DBMappedClass(Table = "dbo.tbGrupo_Servicos", PrimaryKey = "idGrupo")]
    public class GrupoServico
    {
        public int Id { get; set; }

        public string txDiscriminacao { get; set; }
    }
}