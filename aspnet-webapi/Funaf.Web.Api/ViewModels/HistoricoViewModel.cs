using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Funaf.Web.Api.ViewModels
{
    public class HistoricoViewModel
    {
        public DateTime dtInicio { get; set; }
        public DateTime dtFim { get; set; }
        public int idServentia { get; set; }
    }
}