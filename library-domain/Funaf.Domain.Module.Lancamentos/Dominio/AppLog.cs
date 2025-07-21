using DBBroker.Mapping;
using System;

namespace Funaf.Domain.Module.Lancamentos.Dominio
{
    [DBMappedClass(Table = "tbAppLog", PrimaryKey = "idLog")]
    public class AppLog
    {
        public int Id { get; set; }

        public string txMensagem { get; set; }

        public string txStack { get; set; }

        public int nuVerificado { get; set; }

        [DBReadOnly(DBDefaultValue = "GETDATE()")]
        public DateTime dtCadastro { get; set; }

        public int nuNotificarDBA { get; set; }
    }
}
