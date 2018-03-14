using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Api.Core.Controllers.Base;
using Application.Administrativo.Contracts;

namespace Api.Core.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private readonly IProcessoApplicationService _processoApplicationService;

        public HomeController(IProcessoApplicationService processoApplicationService)
        {
            _processoApplicationService = processoApplicationService;
        }

        [HttpGet]
        [Route("api/home/alertas/{usuarioId}")]
        public Task<HttpResponseMessage> Alertas(int usuarioId)
        {
            var alerta = _processoApplicationService.ObterAlerta(usuarioId);

            return CreateResponse(HttpStatusCode.OK, alerta);
        }

        [HttpGet]
        [Route("api/home/sindicantesProcesosMes")]
        public Task<HttpResponseMessage> SindicantesProcesosMes()
        {
            var sidicantes = _processoApplicationService.SindicantesProcesosMes();

            return CreateResponse(HttpStatusCode.OK, sidicantes);
        }
    }
}
