using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Api.Core.Controllers.Base;
using Application.Administrativo.Contracts;

namespace Api.Administrativo.Controllers
{
    [Authorize]
    public class ServicoSindicanteController : BaseController
    {

        private readonly IServicoSindicanteApplicationService _servicoSindicanteApplicationService;

        public ServicoSindicanteController(IServicoSindicanteApplicationService servicoSindicanteApplicationService)
        {
           
           _servicoSindicanteApplicationService = servicoSindicanteApplicationService;
        }

        [HttpGet]
        [Route("api/servicoSindicante")]
        public Task<HttpResponseMessage> SelecionarTodosAtivos()
        {
            var servicoSindicanteList = _servicoSindicanteApplicationService.SelecionarTodosAtivos();

            return CreateResponse(HttpStatusCode.OK, servicoSindicanteList);
        }

        [HttpGet]
        [Route("api/servicoSindicante/selecionarPorSindicante/{id}")]
        public Task<HttpResponseMessage> SelecionarPorSindicante(int id)
        {
            var servicoSindicanteList = _servicoSindicanteApplicationService.SelecionarPorSindicante(id);

            return CreateResponse(HttpStatusCode.OK, servicoSindicanteList);
        }
    }
}
