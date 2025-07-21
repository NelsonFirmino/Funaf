using DBBroker.Mapping;
using System;

namespace Funaf.Domain.Module.Lancamentos.Dominio
{
    [DBMappedClass(Table = "dbo.tbServentias", PrimaryKey = "idServentia")]
    public class Serventia
    {
        public int Id { get; set; }

        public string txCartorio { get; set; }

        public string txResponsavel { get; set; }

        public string txCPF { get; set; }

        public string txEndereco { get; set; }

        public string txBairro { get; set; }

        public string txCEP { get; set; }

        public string txTelefone { get; set; }

        public string txEmail { get; set; }

        public string txServicos { get; set; }

        [DBReadOnly]
        public string txComarca { get; set; }

        [DBTransient]
        public Comarca Comarca { get; set; }

        [DBReadOnly(DBDefaultValue = "GETDATE()")]
        public DateTime dtCadastro { get; set; }

    }
}