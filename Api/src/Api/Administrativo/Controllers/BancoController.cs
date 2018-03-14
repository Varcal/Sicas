using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Api.Core.Controllers.Base;
using Application.Administrativo.Contracts;

namespace Api.Administrativo.Controllers
{
    [Authorize]
    public class BancoController: BaseController
    {
        private readonly IBancoApplicantionService _applicantionService;

        public BancoController(IBancoApplicantionService applicantionService)
        {
            _applicantionService = applicantionService;
        }

        [HttpGet]
        [Route("api/banco")]
        public Task<HttpResponseMessage> SelecionarTodosAtivos()
        {
            var bancos = _applicantionService.SelecionarTodosAtivos();

            return CreateResponse(HttpStatusCode.OK, bancos);
        }  
    }
}