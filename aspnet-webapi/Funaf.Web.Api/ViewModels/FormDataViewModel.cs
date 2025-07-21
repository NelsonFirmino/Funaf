using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Funaf.Web.Api.ViewModels
{
    public class FormDataViewModel
    {
        public string dtInicialPeriodo { get; set; }

        public string dtFinalPeriodo { get; set; }

        public string cartorio { get; set; }

        public string usuario { get; set; }

        public ServicoViewModel[] servicos { get; set; }
    }
}