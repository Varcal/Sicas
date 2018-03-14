using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using Api.Core.Controllers.Base;
using Application.Core.ViewModels;
using Application.Financeiro.Contracts;
using Application.Financeiro.ViewModels.Despesas;
using Newtonsoft.Json.Linq;

namespace Api.Financeiro.Controllers
{
    [Authorize(Roles="Administrador, Financeiro")]
    public class DespesaController : BaseController
    {
        private readonly IDespesaAppService _despesaAppService;

        public DespesaController(IDespesaAppService despesaAppService)
        {
            _despesaAppService = despesaAppService;
        }


        [HttpPost]
        [Route("api/despesa/registrar")]
        public Task<HttpResponseMessage> Registrar([FromBody] dynamic body)
        {
            var despesas = ((JArray)body.despesas).Select(x => new DespesaVm(
                (int)x["id"],
                (int)body.id,
                (string)x["descricao"],
                (DateTime)x["data"],
                new EnderecoVm((string)x["origem"]["logradouro"], (string)x["origem"]["numero"], (string)x["origem"]["cep"], (string)x["origem"]["bairro"], (int)x["origem"]["cidade"]["id"], ""),
                new EnderecoVm((string)x["destino"]["logradouro"], (string)x["destino"]["numero"], (string)x["destino"]["cep"], (string)x["destino"]["bairro"], (int)x["destino"]["cidade"]["id"], ""),
                (decimal)x["kms"],
                (decimal)x["valorKm"],
                ((JArray)x["despesasAdicionais"]).Select(y => new DespesaAdicionalVm(
                    (int)y["id"],
                    (int)y["despesaTipoId"],
                    (int)y["despesaId"],
                    (decimal)y["valor"]
                    )).ToList()
                )
           ).ToList();

            var processo = new ProcessoDespesaRegistrarVm((int)body.id, despesas);

            _despesaAppService.Registrar(processo, UsuarioLogado);

            return CreateResponse(HttpStatusCode.OK, null);
        }


        [HttpGet]
        [Route("api/despesa/obterPorId/{id}")]
        public Task<HttpResponseMessage> ObterPorId(int id)
        {
            var retorno = _despesaAppService.ObterPorId(id);


            return CreateResponse(HttpStatusCode.OK, retorno);
        }


        [HttpPost]
        [Route("api/despesa/alterarDespesa/{id}")]
        public Task<HttpResponseMessage> AlterarDespesa(int id, [FromBody] dynamic body)
        {

            var despesa = new DespesaVm(
                (int)body.id,
                (int)body.processoId,
                (string)body.descricao,
                (DateTime)body.data,
                new EnderecoVm((string)body.origem["logradouro"], (string)body.origem["numero"], (string)body.origem["cep"], (string)body.origem["bairro"], (int)body.origem["cidade"]["id"], ""),
                new EnderecoVm((string)body.destino["logradouro"], (string)body.destino["numero"], (string)body.destino["cep"], (string)body.destino["bairro"], (int)body.destino["cidade"]["id"], ""),
                (decimal)body.kms,
                (decimal)body.valorKm,
                ((JArray)body.despesasAdicionais).Select(y => new DespesaAdicionalVm(
                    (int)y["id"],
                    (int)y["despesaTipoId"],
                    (int)body.id,
                    (decimal)y["valor"]
                    )).ToList()
                );

            _despesaAppService.AlterarDespesa(despesa, UsuarioLogado);

            return CreateResponse(HttpStatusCode.OK, null);
        }

        [HttpPost]
        public Task<HttpResponseMessage> Excluir([FromBody] dynamic body)
        {
            _despesaAppService.Excluir((int) body, UsuarioLogado);

            return CreateResponse(HttpStatusCode.OK, null);
        }

        [HttpGet]
        [Route("api/despesa/emitirRecibo/{id}")]
        public HttpResponseMessage EmitirRecibo(int id)
        {
         
            if (User?.Identity != null && User.Identity.IsAuthenticated)
            {
                var recibo = _despesaAppService.EmitirRecibo(id, UsuarioLogado);

                var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(recibo.Bytes)
                };
                response.Content = new ByteArrayContent(recibo.Bytes);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("inline")
                {
                    FileName = String.Format(recibo.Nome + DateTime.Now.ToString("MMMddyyyy_HHmmss"))

                };

                response.Headers.CacheControl = new CacheControlHeaderValue()
                {
                    MaxAge = new TimeSpan(0, 0, 60) // Cache for 30s so IE8 can open the PDF
                };

                return response;
            }

            return new HttpResponseMessage(HttpStatusCode.Unauthorized);

        }
    }
}