using DBBroker.Mapping;
using System;

namespace Funaf.Domain.Module.Lancamentos.Dominio
{
    [DBMappedClass(Table = "dbo.tbDeclaracoes", PrimaryKey = "idDeclaracao")]
    public class Declaracao
    {
        public int Id { get; set; }

        [DBMappedTo(Column = "idServentia")]
        public Serventia Serventia { get; set; }

        [DBMappedTo(Column = "idBoleto")]
        public Boleto Boleto { get; set; }

        [DBMappedTo(Column = "idUsuarioAbertura")]
        public Usuario UsuarioAbertura { get; set; }

        [DBMappedTo(Column = "idUsuarioFechamento")]
        public Usuario UsuarioFechamento { get; set; }

        public DateTime dtPeriodoInicial { get; set; }

        public DateTime dtPeriodoFinal { get; set; }

        [DBReadOnly]
        public DateTime dtFechamento { get; set; }

        [DBReadOnly(DBDefaultValue = "GETDATE()")]
        public DateTime dtCadastro { get; set; }
    }
}
