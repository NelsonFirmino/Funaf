using System;

namespace Funaf.Service.Module.Cobranca.Dominio.Exceptions
{
    public class TituloJaRegistradoException : Exception
    {
        public TituloJaRegistradoException(string mensagem) : base("[CobrancaModule] " + mensagem)
        { }

        public TituloJaRegistradoException(string mensagem, Exception ex) : base("[CobrancaModule] " + mensagem, ex)
        { }
    }
}
