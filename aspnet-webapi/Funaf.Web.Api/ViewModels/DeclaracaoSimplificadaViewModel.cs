namespace Funaf.Web.Api.ViewModels
{
    public class DeclaracaoSimplificadaViewModel
    {
        public int Id { get; set; }
        
        public string dtInicio { get; set; }

        public string dtFinal { get; set; }

        public string valor { get; set; }

        public bool isQuite { get; set; }
    }
}