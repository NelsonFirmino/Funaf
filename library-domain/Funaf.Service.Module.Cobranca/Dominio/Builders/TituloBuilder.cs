using System;

namespace Funaf.Service.Module.Cobranca.Dominio.Builders
{
    public sealed class TituloBuilder
    {
        private int      _numeroConvenio;
        private DateTime _dataEmissaoTitulo;
        private DateTime _dataVencimentoTitulo;
        private decimal  _valorOriginalTitulo;
        private short    _codigoTipoTitulo;
        private int      _idBoleto;
        private string   _texoDescricaoTipoTitulo;
        private string   _textoNumeroTituloCliente;
        private string   _textoMensagemBloquetoOcorrencia;

        /// <summary>
        /// (int) Identificador deteminado pelo sistama Cobrança para controlar a
        /// emissão do boleto, liquidação, crédito de valores ao beneficiário e 
        /// intercambio de dados com o cliente. 
        /// Convênios da carteira 17
        /// </summary>
        /// <param name="codigoTipoTitulo"></param>
        /// <returns></returns>
        public TituloBuilder WithNumeroConvenio(int numeroConvenio)
        {
            _numeroConvenio = numeroConvenio;
            return this;
        }
        /// <summary>
        /// (DateTime) Data da emissão do documento(NF,Fatura,duplicata,contrato,etc)
        /// que originou o título de cobrança. Não pode ser anterior a 365 dias,
        ///  nem maior que data atul, nem mair o vencimento 
        /// </summary>
        /// <param name="dataEmissaoTitulo"></param>
        /// <returns></returns>
        public TituloBuilder WithDataEmissaoTitulo(DateTime dataEmissaoTitulo)
        {
            _dataEmissaoTitulo = dataEmissaoTitulo;
            return this;
        }
        /// <summary>
        /// (DateTime) Data de Vencimento que deve ser entre a data mínima e máxima 
        /// permitida pela modalidade de cobrança informada.
        /// </summary>
        /// <param name="dataVancimentoTitulo"></param>
        /// <returns></returns>
        public TituloBuilder WithDataVencimentoTitulo(DateTime dataVencimentoTitulo)
        {
            _dataVencimentoTitulo = dataVencimentoTitulo;
            return this;
        }

        /// <summary>
        /// (Double) Valor da Fatura/Duplicata/Contrato.
        /// </summary>
        /// <param name="valorOriginalTitulo"></param>
        /// <returns></returns>
        public TituloBuilder WithValorOriginalTitulo(decimal valorOriginalTitulo)
        {
            _valorOriginalTitulo = Math.Round(valorOriginalTitulo, 2);
            return this;
        }

        /// <summary>
        /// (short) Códigos adotado pala FEBRABAN para identificar o tipo para o título
        /// de cobrança. Sendo 4 = duplicatas e serviço e 24 = Divida ativa-Estado
        /// </summary>
        /// <param name="codigoTipoTitulo"></param>
        /// <returns></returns>
        public TituloBuilder WithCodigoTipoTitulo(short codigoTipoTitulo)
        {
            _codigoTipoTitulo = codigoTipoTitulo;
            return this;
        }

        /// <summary>
        /// (string) Descrição do tipo de título para a cobrança
        /// </summary>
        /// <param name="texoDescricaoTipoTitulo"></param>
        /// <returns></returns>
        public TituloBuilder WithTexoDescricaoTipoTitulo(string texoDescricaoTipoTitulo)
        {
            _texoDescricaoTipoTitulo = texoDescricaoTipoTitulo.Length > 30 ? texoDescricaoTipoTitulo.Substring(0,30): texoDescricaoTipoTitulo;
            return this;
        }

        /// <summary>
        /// (int) Identificação do título de cobrança, gerado pelo beneficiário(idBoleto)
        /// </summary>
        /// <param name="idBoleto"></param>
        /// <returns></returns>
        public TituloBuilder WithIdBoleto(int idBoleto)
        {
            _idBoleto = idBoleto;
            return this;
        }
        /// <summary>
        /// (int) Número do Identificador da cobrança 
        /// </summary>
        /// <param name="textoNumeroTituloCliente"></param>
        /// <returns></returns>
        public TituloBuilder WithTextoNumeroTituloCliente(int textoNumeroTituloCliente)
        {
            _textoNumeroTituloCliente = "000" + _numeroConvenio.ToString().PadLeft(7, '0') + textoNumeroTituloCliente.ToString().PadLeft(10, '0');
            return this;
        }


        /// <summary>
        /// (DateTime,DateTime) Data de inicio e fim referente ao pagamento do boleto
        /// 
        /// </summary>
        /// <param name="TextoMensagemBloquetoOcorrencia"></param>
        /// <returns></returns>
        public TituloBuilder WithTextoMensagemBloquetoOcorrencia(string mensagem)
        {
            _textoMensagemBloquetoOcorrencia = mensagem;
            return this;
        }

        
        public TituloPGE Build()
        {
            TituloPGE titulo = null;
            titulo = new TituloPGE(_numeroConvenio, 
                                _dataEmissaoTitulo,
                                _dataVencimentoTitulo,
                                _valorOriginalTitulo,
                                _codigoTipoTitulo,
                                _idBoleto, 
                                _texoDescricaoTipoTitulo, 
                                _textoNumeroTituloCliente,
                                _textoMensagemBloquetoOcorrencia);
            return titulo;
        }
    }

}
