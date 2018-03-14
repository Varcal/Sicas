using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Api.Core.Controllers.Base;
using Application.Administrativo.Contracts;
using Application.Administrativo.ViewModels.Eventos;
using Application.Administrativo.ViewModels.Seguradoras;
using Newtonsoft.Json.Linq;

namespace Api.Administrativo.Controllers
{
    [Authorize]
    public class SeguradoraController : BaseController
    {
        private readonly ISeguradoraApplicationService _seguradoraApplicationService;

        public SeguradoraController(ISeguradoraApplicationService seguradoraApplicationService)
        {
            _seguradoraApplicationService = seguradoraApplicationService;
        }


        [HttpPost]
        [Route("api/seguradora/registrar")]
        public Task<HttpResponseMessage> Registrar([FromBody] dynamic body)
        {

            var eventoList = ((JArray)body.eventos).Select(x => new EventoVm()
            {
                FranquiaKm = (int)x["franquiaKm"],
                EventoTipoId = (int)x["eventoTipoId"],
                ServicoSeguradoraId = (int)x["servicoSeguradoraId"],
                Honorario = (decimal)x["honorario"]
            }).ToList();

            var seguradoraVm = new SeguradoraVm((string)body.nome, (string)body.cnpj, (string)body.telefone,(string)body.contato,(string)body.email,(decimal)body.valorPorKm, (string)body.observacoes, eventoList);

            _seguradoraApplicationService.Registrar(seguradoraVm, UsuarioLogado);

            return CreateResponse(HttpStatusCode.Created, null);
        }

        [HttpPost]
        [Route("api/seguradora/editar/{id}")]
        public Task<HttpResponseMessage> Editar(int id,[FromBody] dynamic body)
        {
            var eventoList = ((JArray)body.eventos).Select(x => new EventoVm()
            {
                FranquiaKm = x["franquiaKm"] == null ? 0 : (int)x["franquiaKm"],
                EventoTipoId = (int)x["eventoTipoId"],
                ServicoSeguradoraId = (int)x["servicoSeguradoraId"],
                Honorario = (decimal)x["honorario"]
            }).ToList();


            var seguradoraVm = new SeguradoraVm(id, (string)body.nome, (string)body.cnpj, (string)body.telefone, (string)body.contato, (string)body.email, (decimal)body.valorPorKm, (string)body.observacoes, eventoList);

            _seguradoraApplicationService.Editar(seguradoraVm, UsuarioLogado);

            return CreateResponse(HttpStatusCode.Created, null);
        }

        [HttpPost]
        [Route("api/seguradora/excluir")]
        public Task<HttpResponseMessage> Excluir([FromBody]int id)
        {
            _seguradoraApplicationService.Excluir(id, UsuarioLogado);

            return CreateResponse(HttpStatusCode.OK, null);
        }

        [HttpGet]
        [Route("api/seguradora")]
        public Task<HttpResponseMessage> SelecionarTodosAtivos()
        {
            var seguradoras = _seguradoraApplicationService.SelecionarTodosAtivos();

            return CreateResponse(HttpStatusCode.Created, seguradoras);
        }

        [HttpGet]
        [Route("api/seguradora/obterPorId/{id}")]
        public Task<HttpResponseMessage> ObterPorId(int id)
        {
            var seguradora = _seguradoraApplicationService.ObterPorId(id);

            return CreateResponse(HttpStatusCode.Created, seguradora);
        }
    }
}
