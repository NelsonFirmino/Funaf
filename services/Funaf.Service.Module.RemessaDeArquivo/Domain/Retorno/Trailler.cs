namespace RemessaBB.FUNAF.Domain.Retorno
{
    public class Trailler
    {
        public string NuIdentificacao { get; set; }

        public string NuCobrancaSimplesQtdeTitulos { get; set; }
        public string VaCobrancaSimplesTotal { get; set; }
        public string NuCobrancaSimplesNumeroAviso { get; set; }

        public string NuCobrancaVinculadaQtdeTitulos { get; set; }
        public string VaCobrancaVinculadaTotal { get; set; }
        public string NuCobrancaVinculadaNumeroAviso { get; set; }

        public string NuCobrancaCaucionadaQtdeTitulos { get; set; }
        public string VaCobrancaCaucionadaTotal { get; set; }
        public string NuCobrancaCaucionadaNumeroAviso { get; set; }

        public string NuCobrancaDescontadaQtdeTitulos { get; set; }
        public string VaCobrancaDescontadaTotal { get; set; }
        public string NuCobrancaDescontadaNumeroAviso { get; set; }

        public string NuCobrancaVendorQtdeTitulos { get; set; }
        public string VaCobrancaVendorTotal { get; set; }
        public string NuCobrancaVendorNumeroAviso { get; set; }

        public string NuSequencial { get; set; }
    }
}