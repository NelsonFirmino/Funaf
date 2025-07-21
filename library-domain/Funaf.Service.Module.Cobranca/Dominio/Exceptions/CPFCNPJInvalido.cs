using System;

namespace Funaf.Service.Module.Cobranca.Dominio.Exceptions
{
    public class CPFCNPJInvalido : Exception
    {
        public CPFCNPJInvalido(string mensagem) : base("[CobrancaModule] " + mensagem)
        { }

        public CPFCNPJInvalido(string mensagem, Exception ex) : base("[CobrancaModule] " + mensagem, ex)
        { }
    }
}
