using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Funaf.Web.Api.ViewModels
{
    public class CartorioViewModel
    {
        public int Id { get; set; }

        public string txCartorio { get; set; }

        public string txComarca { get; set; }

        public string txResponsavel { get; set; }

        public string txEndereco { get; set; }

        public string txCEP { get; set; }

        public string txTelefone { get; set; }

        public string txEmail { get; set; }

        public string txServicos { get; set; }
    }
}