namespace Funaf.Service.Module.Cobranca.Dominio.Builders
{
    public sealed class PagadorBuilder
    {
        private short  _codigoTipoInscricaoPagador;
        private string   _numeroInscricaoPagador;
        private string _nomePagador;
        private string _textoEnderecoPagador;
        private int    _numeroCepPagador;
        private string _nomeMunicipioPagador;
        private string _nomeBairroPagador;
        private string _siglaUfPagador;

        /// <summary>
        /// (string) Número de inscrição do pagador(CPF/CNPJ) para o Título de Cobrança 
        /// </summary>
        /// <param name="numeroInscricaoPagador"></param>
        /// <returns></returns>
        public PagadorBuilder WithNumeroInscricaoPagador(string textoInscricaoPagador)
        {
            var docPagador = textoInscricaoPagador.Replace("-", "").Replace(".", "").Replace("/", "");

            _numeroInscricaoPagador = docPagador;

            if (textoInscricaoPagador.Length > 14)
            {
                _codigoTipoInscricaoPagador = 2;
            }
            else
            {
                _codigoTipoInscricaoPagador = 1;
            }
            return this;
        }

        /// <summary>
        /// (string) Nome que identifica a entidade, PF ou PJ, pagador original do título de cobrança
        /// </summary>
        /// <param name="nomePagador"></param>
        /// <returns></returns>
        public PagadorBuilder WithNomePagador(string nomePagador)
        {
            _nomePagador = nomePagador.Length > 60 ? nomePagador.Substring(0, 60): nomePagador;
            return this;
        }

        /// <summary>
        /// (string) Endereço da entidade, PF OU PJ, pagador original do título de cobrança 
        /// </summary>
        /// <param name="textoEnderecoPagador"></param>
        /// <returns></returns>
        public PagadorBuilder WithTextoEnderecoPagador(string textoEnderecoPagador)
        {
            _textoEnderecoPagador = textoEnderecoPagador.Length > 60 ? textoEnderecoPagador.Substring(0,60): textoEnderecoPagador;
            return this;
        }

        /// <summary>
        /// (string) CEP da entidade, PF OU PJ, pagador original do título de cobrança 
        /// </summary>
        /// <param name="numeroCepPagador"></param>
        /// <returns></returns>
        public PagadorBuilder WithNumeroCepPagador(string numeroCepPagador)
        {
            _numeroCepPagador = int.Parse(numeroCepPagador.Replace("-","").Replace(".", ""));
            return this;
        }

        /// <summary>
        /// (string) Nome do Município do pagador do título de cobrança 
        /// </summary>
        /// <param name="nomeMunicipioPagador"></param>
        /// <returns></returns>
        public PagadorBuilder WithNomeMunicipioPagador(string nomeMunicipioPagador)
        {
            _nomeMunicipioPagador = nomeMunicipioPagador.Length > 20 ? nomeMunicipioPagador.Substring(0,20): nomeMunicipioPagador;
            return this;
        }

        /// <summary>
        /// (string) Nome do Município do pagador do título de cobrança 
        /// </summary>
        /// <param name="nomeBairroPagador"></param>
        /// <returns></returns>
        public PagadorBuilder WithNomeBairroPagador(string nomeBairroPagador)
        {
            _nomeBairroPagador = nomeBairroPagador.Length >20 ? nomeBairroPagador.Substring(0, 20) : nomeBairroPagador;
            return this;
        }

        /// <summary>
        /// (string) Sigla da UF do pagador do título de cobrança 
        /// </summary>
        /// <param name="siglaUfPagador"></param>
        /// <returns></returns>
        public PagadorBuilder WithSiglaUfPagador(string siglaUfPagador)
        {
            _siglaUfPagador = siglaUfPagador.Length > 2 ? siglaUfPagador.Substring(0, 2): siglaUfPagador;
            return this;
        }

        public PagadorPGE Build()
        {
            PagadorPGE Pagador = null;
            Pagador = new PagadorPGE
                (
                _codigoTipoInscricaoPagador,
                _numeroInscricaoPagador,
                _nomePagador,
                _textoEnderecoPagador,
                _numeroCepPagador,
                _nomeMunicipioPagador,
                _nomeBairroPagador,
                _siglaUfPagador);

            return Pagador;
        }
    }
}
