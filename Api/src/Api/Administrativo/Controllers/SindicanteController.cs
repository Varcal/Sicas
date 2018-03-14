using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Api.Core.Controllers.Base;
using Application.Administrativo.Contracts;
using Application.Administrativo.ViewModels.Sindicantes;
using Application.Core.ViewModels;
using Newtonsoft.Json.Linq;

namespace Api.Administrativo.Controllers
{
    [Authorize]
    public class SindicanteController: BaseController
    {
        private readonly ISindicanteApplicationService _sindicanteApplicationService;

        public SindicanteController(ISindicanteApplicationService sindicanteApplicationService)
        {
            _sindicanteApplicationService = sindicanteApplicationService;
        }


        [HttpGet]
        [Route("api/sindicantes")]
        public Task<HttpResponseMessage> SelecionarTodosAtivos()
        {
            var sindicantes = _sindicanteApplicationService.SelecionarTodosAtivos();

            return CreateResponse(HttpStatusCode.OK, sindicantes);
        }

        [HttpGet]
        [Route("api/sindicantes/TodosExternosAtivos")]
        public Task<HttpResponseMessage> SelecionarTodosExternosAtivos()
        {
            var sindicantes = _sindicanteApplicationService.SelecionarTodosExternosAtivos();

            return CreateResponse(HttpStatusCode.OK, sindicantes);
        }
      
        [HttpGet]
        [Route("api/sindicante/obterPorId/{id}")]
        public Task<HttpResponseMessage> ObterPorId(int id)
        {
            var sindicantes = _sindicanteApplicationService.ObterPorId(id);

            return CreateResponse(HttpStatusCode.OK, sindicantes);
        }

        [HttpPost]
        [Route("api/sindicante/registrar")]
        public Task<HttpResponseMessage> Registrar([FromBody] dynamic body)
        {         

            var sindicanteVm = new SindicanteVm(
                    (int) body.sindicanteTipoId,
                    (string) body.nome, (string) body.cpf, (string)body.rg,
                    (string) body.telefone,
                    (string) body.celular,
                    (string) body.email,
                    new EnderecoVm((string)body.endereco.logradouro, (string)body.endereco.numero,(string)body.endereco.cep,(string)body.endereco.bairro,(int)body.endereco.cidade.id,(string)body.complemento),
                    ((JArray)body.honorarios).Select(x => new HonorarioVm((int)x["servicoSeguradoraId"], (int)x["eventoTipoId"], (int)x["servicoSindicanteId"], (decimal)x["valor"])).ToList(),
                    (int)body.bancoId,
                    (int)body.contaTipo,
                    (string)body.agencia,
                    (string)body.conta,
                    (string)body.digito,
                    body.valorPorKm == null ? 0 : (decimal)body.valorPorKm
                );

            _sindicanteApplicationService.Registrar(sindicanteVm, UsuarioLogado);

            return CreateResponse(HttpStatusCode.Created, null);
        }

        [HttpPost]
        [Route("api/sindicante/editar/{id}")]
        public Task<HttpResponseMessage> Editar(int id,[FromBody] dynamic body)
        {
            var honorarioList = ((JArray)body.honorarios).Select(x => new HonorarioVm(
                (int)x["servicoSeguradoraId"],
                (int)x["eventoTipoId"],
                (int)x["servicoSindicanteId"],
                (decimal)x["valor"])
           ).ToList();

            var valorPorKm = body.valorPorKm == null ? 0 : (decimal)body.valorPorKm;


            var sindicanteVm = new SindicanteVm(
                id,
                (int)body.sindicanteTipoId,
                (string)body.nome, (string)body.cpf, (string)body.rg,
                (string)body.telefone,
                (string)body.celular,
                (string)body.email,
                new EnderecoVm((string)body.endereco.logradouro, (string)body.endereco.numero, (string)body.endereco.cep, (string)body.endereco.bairro, (int)body.endereco.cidade.id, (string)body.complemento),
                honorarioList,
                (int)body.bancoId,
                (int)body.contaTipo,
                (string)body.agencia,
                (string)body.conta,
                (string)body.digito,
                valorPorKm);

            _sindicanteApplicationService.Editar(sindicanteVm, UsuarioLogado);

            return CreateResponse(HttpStatusCode.Created, null);
        }

        [HttpPost]
        [Route("api/sindicante/excluir")]
        public Task<HttpResponseMessage> Excluir([FromBody] dynamic body)
        {

            var model = new SindicanteDeleteVm((int)body.id, (int)body.sindicanteTipoId);       

            _sindicanteApplicationService.Excluir(model, UsuarioLogado);

            return CreateResponse(HttpStatusCode.OK, null);
        }

        [HttpGet]
        [Route("api/sindicante/obterProcessos/{sindicanteId:int}")]
        public Task<HttpResponseMessage> ObterProcessos(int sindicanteId)
        {
            var processos = _sindicanteApplicationService.ObterProcessos(sindicanteId);

            return CreateResponse(HttpStatusCode.OK, processos);
        }

        [HttpGet]
        [Route("api/sindicante/selecionarPorProcesso/{processoId:int}")]
        public Task<HttpResponseMessage> SelecionarPorProcesso(int processoId)
        {
            var sindicantes = _sindicanteApplicationService.SelecionarPorProcesso(processoId);

            return CreateResponse(HttpStatusCode.OK, sindicantes);
        }
    }
}