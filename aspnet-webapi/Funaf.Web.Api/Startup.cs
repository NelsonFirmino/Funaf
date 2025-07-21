using System.Configuration;
using System.Text;
using System.Web.Http;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;
using Owin;

namespace Funaf.Web.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var FUNAF_URL = ConfigurationManager.AppSettings["FUNAF_URL"].ToString();

            var config = new HttpConfiguration();

            WebApiConfig.Register(config);

            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = FUNAF_URL, // Documentar essa alteração *produção e desenvolvimento
                    ValidAudience = FUNAF_URL,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("bCtSEErAZu8eq0Wk3L2ruJgShNepW0PLPfPH7I4gEZv1NSjKJrvrnvLO8ugXI3Ach2jIi2KT7mYhyfunW5v2blHsu31792yqlBXVd5YxmeFPdDwgmy8zmj0zLIw8a3F")) // Chave secreta para usar durate a validação
                }
            });

            app.UseWebApi(config);
        }
    }
}