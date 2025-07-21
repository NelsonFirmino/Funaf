using DBBroker.Mapping;
using Funaf.Domain.Module.Lancamentos.Dominio;
using System;

namespace RemessaBB.FUNAF.Domain
{
    [Serializable]
    [DBMappedClass(PrimaryKey = "idRemessaRetorno", Table = "tbRemessaBB_Retorno")]
    public class RemessaBB_Retorno
    {
        public int id { get; set; }
        public Boleto Boleto { get; set; }
        public Comando Comando { get; set; }
        public int nuCodRetorno { get; set; }
        public int nuCodCobranca { get; set; }
        public int nuDiasCalculo { get; set; }
        public int nuNaturezaRecebimento { get; set; }
        public int nuContaCaucao { get; set; }
        public int nuCodBancoRecebedor { get; set; }
        public int nuPrefixoAgenciaRecebedora { get; set; }
        public int nuEspecieTitulo { get; set; }
        public int nuIndicativoDebito { get; set; }
        public int nuIndicativoValor { get; set; }
        public int nuIndicativoAutorizacaoLiquidacao { get; set; }
        public int nuCanalPagamentoUtilizado { get; set; }
        public string txPrefixoTitulo { get; set; }
        public decimal vaTarifa { get; set; }
        public decimal vaAjuste { get; set; }
        public DateTime dtCredito { get; set; }
        public DateTime dtVencimento { get; set; }
        public DateTime? dtRetorno { get; set; }
    }
}