using System;

namespace Funaf.Web.Api.ViewModels
{
    public class ArrecadacaoDividaSigefViewModel
    {
        public DateTime dtInicio { get; set; }
        public DateTime dtFim { get; set; }
        public string tipoRelatorio { get; set; }
        public string idTipoTributo { get; set; }
    }
}