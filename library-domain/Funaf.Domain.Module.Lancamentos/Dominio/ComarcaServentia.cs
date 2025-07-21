using DBBroker.Mapping;
using System;

namespace Funaf.Domain.Module.Lancamentos.Dominio
{
    [DBMappedClass(Table = "dbo.tbComarcas_Serventias", PrimaryKey = "idComarcaServentia")]
    public class ComarcaServentia
    {
        public int Id { get; set; }

        public int idComarca { get; set; }
        [DBReadOnly]
        public string txComarca { get; set; }

        public int idServentia { get; set; }
        [DBReadOnly]
        public string txServentia { get; set; }

        [DBReadOnly(DBDefaultValue = "GETDATE()")]
        public DateTime dtCadastro { get; set; }

    }
}