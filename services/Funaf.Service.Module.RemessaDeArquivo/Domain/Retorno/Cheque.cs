namespace RemessaBB.FUNAF.Domain.Retorno
{
    public class Cheque
    {
        public string NuIdentificacao { get; set; }
        public string NuTipoServico { get; set; }
        public string NuNossoNumero { get; set; }
        public string DtPagamento { get; set; }
        public string VaCheque { get; set; }
        public string NuPrazoBloqueio { get; set; }
        public string NuMotivoDevolucao { get; set; }
        public string TxTrilhaCheque { get; set; }
        public string NuTipoCaptura { get; set; }
        public string NuSequencial { get; set; }
    }
}