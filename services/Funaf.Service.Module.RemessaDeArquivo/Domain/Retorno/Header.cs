namespace RemessaBB.FUNAF.Domain.Retorno
{
    public class Header
    {
        public string NuIdentificacao { get; set; }
        public string NuTipoOperacao { get; set; }
        public string TxTipoOperacao { get; set; }
        public string TxTipoServico { get; set; }
        public string NuAgencia { get; set; }
        public string NuAgenciaDigito { get; set; }
        public string NuContaCorrente { get; set; }
        public string NuContaCorrenteDigito { get; set; }
        public string TxNomeCedente { get; set; }
        public string DtGravacao { get; set; }
        public string NuSequenciaRetorno { get; set; }
        public string NuConvenio { get; set; }
        public string NuSequencialRegistro { get; set; }
    }
}