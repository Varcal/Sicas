using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Api.Core.Controllers.Base;
using Application.Core.Contracts;

namespace Api.Core.Controllers
{
    public class EstadoController : BaseController
    {
        private readonly IEstadoApplicationService _estadoApplicationService;

        public EstadoController(IEstadoApplicationService estadoApplicationService)
        {
            _estadoApplicationService = estadoApplicationService;
        }

        [HttpGet]
        [Route("api/estados")]
        public Task<HttpResponseMessage> SelecionarTodos()
        {
            var estadoList = _estadoApplicationService.SelecionarTodos();

            return CreateResponse(HttpStatusCode.Created, estadoList);
        }
    }
}
