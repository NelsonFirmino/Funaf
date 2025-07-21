using System;
using DBBroker.Mapping;

namespace Funaf.Service.Module.Cobranca.Dominio
{
    [DBMappedClass(PrimaryKey = "idRegistroBBWSBoleto", Table = "dbo.tbRegistroBBWS_Boletos")]
    public class RegistroBBWSBoleto
    {
        public int Id { get; set; }

        public int idBoleto { get; set; }

        public decimal vaDocumento { get; set; }
        
        public DateTime dtVencimento { get; set; }
        [DBReadOnly]
        public DateTime dtEmissao { get; set; }
        [DBReadOnly]
        public TimeSpan hrEmissao { get; set; }

        public string txNossoNumero { get; set; }

        public string txLinhaDigitavel { get; set; }

        public string txCodigoBarraNumerico { get; set; }

    }
}
