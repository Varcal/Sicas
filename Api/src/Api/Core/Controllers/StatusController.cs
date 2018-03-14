using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Api.Core.Controllers.Base;
using Application.Core.Contracts;

namespace Api.Core.Controllers
{
    public class StatusController : BaseController
    {
        private readonly IStatusApplicationService _statusApplicationService;

        public StatusController(IStatusApplicationService statusApplicationService)
        {
            _statusApplicationService = statusApplicationService;
        }

        [HttpGet]
        [Route("api/status")]
        public Task<HttpResponseMessage> Selecionar()
        {
            var lista =  _statusApplicationService.SelecionarTodos();

            return CreateResponse(HttpStatusCode.OK, lista);
        }
    }
}
