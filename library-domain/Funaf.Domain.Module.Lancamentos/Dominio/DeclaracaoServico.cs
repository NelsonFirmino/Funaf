using DBBroker.Mapping;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;

namespace Funaf.Domain.Module.Lancamentos.Dominio
{
    [DBMappedClass(Table = "dbo.tbDeclaracoes_Servicos", PrimaryKey = "idDeclaracaoServico")]
    public class DeclaracaoServico
    {
        public int Id { get; set; }

        public Declaracao Declaracao { get; set; }

        public Servico Servico { get; set; }

        public int nuQuantidade { get; set; }

        public Decimal vaServico { get; set; }

        [DBReadOnly]
        public Decimal vaTotal { get; set; }
    }
}
