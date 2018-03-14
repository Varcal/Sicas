using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Api.Core.Controllers.Base;
using Application.Administrativo.Contracts;
using Application.Administrativo.ViewModels.EventosTipos;
using Application.Administrativo.ViewModels.ServicosSeguradoras;
using Newtonsoft.Json.Linq;

namespace Api.Administrativo.Controllers
{
    [Authorize]
    public class ServicoSeguradoraController : BaseController
    {
        private readonly IServicoSeguradoraApplicationService _servicoSeguradoraApplicationService;

        public ServicoSeguradoraController(IServicoSeguradoraApplicationService servicoSeguradoraApplicationService)
        {
            _servicoSeguradoraApplicationService = servicoSeguradoraApplicationService;
        }


        [HttpPost]
        [Route("api/servicoSeguradora/Registrar")]
        public Task<HttpResponseMessage> Registrar([FromBody] dynamic body)
        {
            var eventoTipoList = ((JArray)body.eventosTipos).Select(x => new EventoTipoVm()
            {
                Id = (int)x["id"],
                Nome = (string)x["nome"]
            }).ToList();

            var servicoSeguradora = new ServicoSeguradoraVm((string)body.nome, eventoTipoList);

            _servicoSeguradoraApplicationService.Registrar(servicoSeguradora, UsuarioLogado);
            
            return CreateResponse(HttpStatusCode.Created, null);
        }

        [HttpPost]
        [Route("api/servicoSeguradora/Alterar/{id}")]
        public Task<HttpResponseMessage> Alterar(int id, [FromBody] dynamic body)
        {
            var eventoTipoList = ((JArray)body.eventosTipos).Select(x => new EventoTipoVm()
            {
                Id = (int)x["id"],
                Nome = (string)x["nome"]
            }).ToList();

            var servicoSeguradora = new ServicoSeguradoraVm(id, (string)body.nome, eventoTipoList);

            _servicoSeguradoraApplicationService.Alterar(servicoSeguradora, UsuarioLogado);

            return CreateResponse(HttpStatusCode.Created, null);
        }

        [HttpPost]
        [Route("api/servicoSeguradora/Excluir")]
        public Task<HttpResponseMessage> Excluir([FromBody]dynamic id)
        {
            
            _servicoSeguradoraApplicationService.Excluir((int)id, UsuarioLogado);

            return CreateResponse(HttpStatusCode.Created, null);
        }

        [HttpGet]
        [Route("api/servicoSeguradora")]
        public Task<HttpResponseMessage> SelecionarTodosAtivos()
        {
            var servicoSeguradoraList = _servicoSeguradoraApplicationService.SelecionarTodosAtivos();

            return CreateResponse(HttpStatusCode.Created, servicoSeguradoraList);
        }

        [HttpGet]
        [Route("api/servicoSeguradoraComEventos")]
        public Task<HttpResponseMessage> SelecionarTodosComEventos()
        {
            var servicoSeguradoraList = _servicoSeguradoraApplicationService.SelecionarTodosComEventos();

            return CreateResponse(HttpStatusCode.Created, servicoSeguradoraList);
        }

        [HttpGet]
        [Route("api/servicoSeguradora/obterPorId/{id}")]
        public Task<HttpResponseMessage> ObterPorId(int id)
        {
            var servicoSeguradora = _servicoSeguradoraApplicationService.ObterPorId(id);

            return CreateResponse(HttpStatusCode.Created, servicoSeguradora);
        }
    }
}
