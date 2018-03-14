using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Api.Core.Controllers.Base;
using Application.Administrativo.Contracts;
using Application.Administrativo.ViewModels.EventosTipos;

namespace Api.Administrativo.Controllers
{
    [Authorize]
    public class EventoTipoController : BaseController
    {
        private readonly IEventoTipoApplicationService _eventoTipoApplicationService;

        public EventoTipoController(IEventoTipoApplicationService eventoTipoApplicationService)
        {
            _eventoTipoApplicationService = eventoTipoApplicationService;
        }

        [HttpPost]
        [Route("api/eventoTipo/registrar")]
        public Task<HttpResponseMessage> Registrar([FromBody]dynamic body)
        {
            var model = new EventoTipoVm
            {
                Nome = (string)body.nome
            };

            _eventoTipoApplicationService.Registrar(model, UsuarioLogado);

            return CreateResponse(HttpStatusCode.Created, null);
        }

        [HttpPost]
        [Route("api/eventoTipo/editar/{id}")]
        public Task<HttpResponseMessage> Editar(int id, [FromBody]dynamic body)
        {
            var model = new EventoTipoVm
            {
                Id = id,
                Nome = (string)body.nome
            };

            _eventoTipoApplicationService.Editar(model, UsuarioLogado);

            return CreateResponse(HttpStatusCode.Created, null);
        }

        [HttpPost]
        [Route("api/eventoTipo/excluir")]
        public Task<HttpResponseMessage> Excluir([FromBody]dynamic body)
        {
           
            _eventoTipoApplicationService.Excluir((int)body, UsuarioLogado);

            return CreateResponse(HttpStatusCode.Created, null);
        }

        [HttpGet]
        [Route("api/eventoTipo")]
        public Task<HttpResponseMessage> SelecionarTodos()
        {
            var eventoTipoList = _eventoTipoApplicationService.SelecionarTodosAtivos();

            return CreateResponse(HttpStatusCode.Created, eventoTipoList);
        }

        [HttpGet]
        [Route("api/eventoTipo/obterPorId/{id}")]
        public Task<HttpResponseMessage> ObterPorId(int id)
        {
            var eventoTipo = _eventoTipoApplicationService.ObterPorId(id);
            
            return CreateResponse(HttpStatusCode.Created, eventoTipo);
        }
    }
}
