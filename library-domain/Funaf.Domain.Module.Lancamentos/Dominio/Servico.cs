using DBBroker.Mapping;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;

namespace Funaf.Domain.Module.Lancamentos.Dominio
{
    [DBMappedClass(Table = "dbo.tbServicos", PrimaryKey = "idServico")]
    public class Servico
    {
        public int Id { get; set; }

        [Required]
        public string txDiscriminacao { get; set; }

        public decimal vaServico { get; set; }

        public GrupoServico GrupoServicos { get; set; }

        [DBReadOnly(DBDefaultValue = "GETDATE()")]
        public DateTime Cadastro { get; set; }
    }
}