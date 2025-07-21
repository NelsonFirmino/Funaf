using System;

namespace Funaf.Domain.Module.Lancamentos.Dominio.Exceptions
{
    public class FunafAPIException : Exception
    {
        public FunafAPIException(string message) : base("[FUNAFAPI] " + message)
        { }

        public FunafAPIException(string message, Exception ex) : base("[FUNAFAPI] " + message, ex)
        { }
    }
}
