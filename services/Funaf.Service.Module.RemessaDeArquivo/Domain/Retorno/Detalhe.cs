namespace RemessaBB.FUNAF.Domain.Retorno
{
    public class Detalhe
    {
        public string NuIdentificacao { get; set; }
        public string NuAgencia { get; set; }
        public string NuAgenciaDigito { get; set; }
        public string NuContaCorrente { get; set; }
        public string NuContaCorrenteDigito { get; set; }
        public string NuConvenioCedente { get; set; }
        public string NuControleParticipante { get; set; }
        public string NuNossoNumero { get; set; }
        public string NuTipoCobranca { get; set; }
        public string NuTipoCobrancaEspecifico { get; set; }
        public string NuDiasCalculo { get; set; }
        public string NuNaturezaRecebimento { get; set; }
        public string TxPrefixoTitulo { get; set; }
        public string NuCarteira { get; set; }
        public string NuContaCaucao { get; set; }
        public string NuTaxaDesconto { get; set; }
        public string NuTaxaIof { get; set; }
        public string NuCarteira2 { get; set; }
        public string NuComando { get; set; }
        public string DtLiquidacao { get; set; }
        public string NuTituloCedente { get; set; }
        public string DtVencimento { get; set; }
        public string VaTitulo { get; set; }
        public string NuCodBancoRecebedora { get; set; }
        public string NuPrefixoAgenciaRecebedora { get; set; }
        public string NuDigitoRecebedora { get; set; }
        public string NuEspecieTitulo { get; set; }
        public string DtCredito { get; set; }
        public string VaTarifa { get; set; }
        public string VaOutrasDespesas { get; set; }
        public string VaJurosDesconto { get; set; }
        public string VaIofDesconto { get; set; }
        public string VaAbatimento { get; set; }
        public string VaDescontoCedido { get; set; }
        public string VaRecebido { get; set; }
        public string VaJurosMora { get; set; }
        public string VaOutrosRecebimento { get; set; }
        public string VaAbatimentoNaoAproveitado { get; set; }
        public string VaLancamento { get; set; }
        public string NuIndicativoDebito { get; set; }
        public string NuIndicativoValor { get; set; }
        public string VaAjuste { get; set; }
        public string NuCodConvenioCompartilhado { get; set; }
        public string VaOriginalPago { get; set; }
        public string NuPrimeiroConvenio { get; set; }
        public string VaPrimeiroConvenio { get; set; }
        public string NuSegundoConvenio { get; set; }
        public string VaSegundoConvenio { get; set; }
        public string NuTerceiroConvenio { get; set; }
        public string VaTerceiroConvenio { get; set; }
        public string NuIndicativoAutoriazacaoLiquidacaoParcial { get; set; }
        public string NuCanalPagamentoTitulo { get; set; }
        public string NuSequencialRegistro { get; set; }

        public Auxiliar Auxiliar { get; set; }
        public Opcional Opcional { get; set; }
    }
}