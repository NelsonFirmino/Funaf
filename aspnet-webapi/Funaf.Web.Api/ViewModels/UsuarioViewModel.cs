using System.Collections.Generic;

namespace Funaf.Web.Api.ViewModels
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }
        public string usuario { get; set; }
        public string senha { get; set; }
        public int idSistema { get; set; }
        public string nome { get; set; }
        public string txFuncao { get; set; }
        public List<CartorioViewModel> cartorios { get; set; }
        public string tokenUser { get; set; }
    }
}