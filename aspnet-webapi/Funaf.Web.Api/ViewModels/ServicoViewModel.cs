using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Funaf.Web.Api.ViewModels
{
    public class ServicoViewModel
    {
        public int Id { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
    }
}