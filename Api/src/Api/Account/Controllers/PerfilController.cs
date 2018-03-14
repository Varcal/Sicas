using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Api.Core.Controllers.Base;
using Application.Account.Contracts;

namespace Api.Account.Controllers
{
    [Authorize(Roles="Administrador")]
    public class PerfilController: BaseController
    {
        private readonly IPerfilApplicationService _perfilApplicationService;

        public PerfilController(IPerfilApplicationService perfilApplicationService)
        {
            _perfilApplicationService = perfilApplicationService;
        }

        [HttpGet]
        [Route("api/perfil")]
        public Task<HttpResponseMessage> SelecionarTodosAtivos()
        {
            var perfis = _perfilApplicationService.SelecionarTodosAtivos();

            return CreateResponse(HttpStatusCode.Created, perfis);
        }
    }
}