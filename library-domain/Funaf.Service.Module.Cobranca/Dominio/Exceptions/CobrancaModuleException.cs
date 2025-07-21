using System;

namespace Funaf.Service.Module.Cobranca.Dominio.Exceptions
{
    [Serializable]
    public class CobrancaModuleException : Exception
    {
        public CobrancaModuleException(string mensagem) : base("[CobrancaModule] " + mensagem)
        { }

        public CobrancaModuleException(string mensagem, Exception ex) : base("[CobrancaModule] " + mensagem, ex)
        { }
    }
}
