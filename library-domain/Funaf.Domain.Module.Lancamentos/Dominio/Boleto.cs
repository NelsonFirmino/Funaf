using System;
using DBBroker.Mapping;
using System.Collections.Generic;

namespace Funaf.Domain.Module.Lancamentos.Dominio
{
    [DBMappedClass(Table = "tbBoletos", PrimaryKey = "idBoleto")]
    public class Boleto
    {
        public int Id { get; set; }
        
        [DBTransient]
        public List<Declaracao> Declaracoes { get; set; }

        [DBTransient]
        public int Sequencial
        {
            get
            {
                return Id;
            }
        }

        [DBReadOnly]
        public int idRegistroBBWSBoleto { get; set; }

        [DBTransient]
        public string txNossoNumero { get; set; }

        public Situacao Situacao { get; set; }

        public decimal vaPrincipal { get; set; }

        public decimal vaJuros { get; set; }

        public decimal vaMulta { get; set; }

        public decimal vaDocumento { get; set; }
        
        public DateTime dtVencimento { get; set; }        
    }
}
