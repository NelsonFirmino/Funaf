using DBBroker.Mapping;
using System;

namespace Funaf.Service.Module.Cobranca.Dominio
{
    [DBMappedClass(PrimaryKey = "idRegistroErro", Table = "dbo.tbRegistroBBWS_Erros")]
    public class RegistroBBWSErro
    {
        public int Id { get; set; }

        public RegistroBBWSBoleto RegistroBBWSBoleto { get; set; }

        public string txSiglaSistemaMsg { get; set; }

        public int nuRetornoPrograma { get; set; }

        public string txNomePrograma { get; set; }

        public string txMensagem { get; set; }

        public int nuPosicaoErro { get; set; }

        public int nuTipoRetorno { get; set; }

        public string xmlRequest { get; set; }

        public string xmlResponse { get; set; }

        [DBReadOnly]
        public DateTime dtCadastro { get; set; }

        [DBReadOnly]
        public TimeSpan hrCadastro { get; set; }
    }
}
