namespace Funaf.Service.Module.Cobranca.Dominio
{
    public sealed class PagadorPGE
    {
        public PagadorPGE()
        { }
        
        public PagadorPGE(short codigoTipoInscricaoPagador, 
                            string numeroInscricaoPagador,
                            string nomePagador, 
                            string textoEnderecoPagador, 
                            int numeroCepPagador,
                            string nomeMunicipioPagador, 
                            string nomeBairroPagador, 
                            string siglaUfPagador)
        {
            CodigoTipoInscricaoPagador = codigoTipoInscricaoPagador;
            NumeroInscricaoPagador = numeroInscricaoPagador;
            NomePagador = nomePagador;
            TextoEnderecoPagador = textoEnderecoPagador;
            NumeroCepPagador = numeroCepPagador;
            NomeMunicipioPagador = nomeMunicipioPagador;
            NomeBairroPagador = nomeBairroPagador;
            SiglaUfPagador = siglaUfPagador;
        }

        public short CodigoTipoInscricaoPagador { get; set; }
        public string NumeroInscricaoPagador { get; set; }
        public string NomePagador { get; set; }
        public string TextoEnderecoPagador { get; set; }
        public int NumeroCepPagador { get; set; }
        public string NomeMunicipioPagador { get; set; }
        public string NomeBairroPagador { get; set; }
        public string SiglaUfPagador { get; set; }
    }
}
