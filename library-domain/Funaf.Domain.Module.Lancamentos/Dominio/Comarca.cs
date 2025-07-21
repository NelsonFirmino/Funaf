using DBBroker.Mapping;
using System;
using System.Collections.Generic;

namespace Funaf.Domain.Module.Lancamentos.Dominio
{
    [DBMappedClass(Table = "dbo.tbComarcas", PrimaryKey = "idComarca")]
    public class Comarca
    {
        public int Id { get; set; }

        public string txComarca { get; set; }

        [DBMappedList(RelationshipTable = "tbComarcas_Serventias", ParentColumnIds = "idComarca", ChildrenColumnIds = "idServentia")]
        public List<Serventia> Serventias { get; set; }

        [DBReadOnly(DBDefaultValue = "GETDATE()")]
        public DateTime dtCadastro { get; set; }

    }
}