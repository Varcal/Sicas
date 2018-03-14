using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Api.Core.Controllers.Base;
using Application.Core.Contracts;

namespace Api.Core.Controllers
{
    public class CidadeController : BaseController
    {
        private readonly ICidadeApplicationService _cidadeApplicationService;
        

        public CidadeController(ICidadeApplicationService cidadeApplicationService)
        {
            _cidadeApplicationService = cidadeApplicationService;
        }


        [HttpGet]
        [Route("api/cidades/{ufId}")]
        public Task<HttpResponseMessage> SelecionarPorEstado(int ufId)
        {
            var cidadeList = _cidadeApplicationService.SelecionarPorEstado(ufId);

            return CreateResponse(HttpStatusCode.Created, cidadeList);
        }
    }
}
