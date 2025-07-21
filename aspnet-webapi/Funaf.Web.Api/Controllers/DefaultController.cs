using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Funaf.Web.Api.Controllers
{
    public class DefaultController : ApiController
    {
        [HttpGet]
        [Route("")]
        public HttpResponseMessage IsReady()
        {
            var res = new HttpResponseMessage(HttpStatusCode.OK);
            res.Content = new StringContent("Service is ready!", System.Text.Encoding.UTF8, "text/plain");

            return res;
        }

        [HttpGet]
        [Route("api/auth")]
        [Authorize]
        public HttpResponseMessage IsReadyAuth()
        {
            var res = new HttpResponseMessage(HttpStatusCode.OK);
            res.Content = new StringContent("Service is ready auth!", System.Text.Encoding.UTF8, "text/plain");

            return res;
        }
    }
}
