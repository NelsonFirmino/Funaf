using DBBroker.Mapping;
using System;

namespace Funaf.Domain.Module.Lancamentos.Dominio
{
    [DBMappedClass(Table = "dbo.tbServentias_Usuarios", PrimaryKey = "idServentiaUsuario")]
    public class ServentiaUsuario
    {
        public int Id { get; set; }

        public Serventia Serventia { get; set; }

        public Usuario Usuario { get; set; }

        [DBReadOnly(DBDefaultValue = "GETDATE()")]
        public DateTime dtCadastro { get; set; }

    }
}