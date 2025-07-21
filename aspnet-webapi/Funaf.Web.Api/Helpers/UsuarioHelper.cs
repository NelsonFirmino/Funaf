using System.Web.Script.Serialization;
using Funaf.Web.Api.ViewModels;

namespace Funaf.Web.Api.Helpers
{
    public abstract class UsuarioHelper
    {
        public static UsuarioViewModel DecodeUsuario(object token)
        {
            // Converte token Base64 para json numa variavel string
            var json = ConversorHelper.base64Decode(token.ToString());

            // Converter o conteudo do token para um objeto Serventia (nome, senha)
            var usr = new JavaScriptSerializer().Deserialize<UsuarioViewModel>(json);

            return usr;
        }
    }
}