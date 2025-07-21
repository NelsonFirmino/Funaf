using System;

namespace Funaf.Service.Module.Cobranca.Dominio
{
    public sealed class TituloPGE
    {
        public TituloPGE()
        { }
        
        public TituloPGE(int     numeroConvenio,
                        DateTime dataEmissaoTitulo,
                        DateTime dataVencimentoTitulo,
                        decimal  valorOriginalTitulo, 
                        short    codigoTipoTitulo,
                        int      idBoleto,
                        string   texoDescricaoTipoTitulo, 
                        string   textoNumeroTituloCliente,
                        string   textoMensagemBloquetoOcorrencia)
        {
            NumeroConvenio = numeroConvenio;
            DataEmissaoTitulo = dataEmissaoTitulo;
            DataVencimentoTitulo = dataVencimentoTitulo;
            ValorOriginalTitulo = valorOriginalTitulo;
            CodigoTipoTitulo = codigoTipoTitulo;
            IdBoleto = idBoleto;
            TexoDescricaoTipoTitulo = texoDescricaoTipoTitulo;
            TextoNumeroTituloCliente = textoNumeroTituloCliente;
            TextoMensagemBloquetoOcorrencia = textoMensagemBloquetoOcorrencia;
        }

        public int      NumeroConvenio { get; set; }
        public DateTime DataEmissaoTitulo { get; set; }
        public DateTime DataVencimentoTitulo { get; set; }
        public decimal  ValorOriginalTitulo { get; set; }
        public short    CodigoTipoTitulo { get; set; }
        public int      IdBoleto { get; set; }
        public string   TexoDescricaoTipoTitulo { get; set; }
        public string   TextoNumeroTituloCliente { get; set; }
        public string   TextoMensagemBloquetoOcorrencia { get; set; }
    }
}
