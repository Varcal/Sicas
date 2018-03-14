using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Api.Core.Controllers.Base;
using Application.Financeiro.Contracts;
using Application.Financeiro.ViewModels.Lancamentos;
using Newtonsoft.Json.Linq;

namespace Api.Financeiro.Controllers
{
    public class LancamentoController : BaseController
    {
        private readonly ILancamentoAppService _lancamentoAppService;

        public LancamentoController(ILancamentoAppService lancamentoAppService)
        {
            _lancamentoAppService = lancamentoAppService;
        }


        [HttpPost]
        [Route("api/lancamento/registrar")]
        public Task<HttpResponseMessage> Registrar([FromBody] dynamic body)
        {
            var lancamento = new LancamentoRegistrarVm(
                (int)body.reciboId, 
                (int)body.lancamentoTipo,
                (string)body.descricao,
                (decimal)body.valor,
                (string)body.observacao,
                (int)body.tipoFinanceiro);

            _lancamentoAppService.Registrar(lancamento, UsuarioLogado);

            return CreateResponse(HttpStatusCode.OK, null);
        }

        [HttpPost]
        [Route("api/lancamento/excluir")]
        public Task<HttpResponseMessage> Excluir([FromBody] dynamic body)
        {
            _lancamentoAppService.Excluir((int) body, UsuarioLogado);

            return CreateResponse(HttpStatusCode.OK, null);
        }

        [HttpPost]
        [Route("api/lancamento/fecharLancamento")]
        public Task<HttpResponseMessage> FecharLancamento([FromBody] dynamic body)
        {
            _lancamentoAppService.FecharLancamento((int)body, UsuarioLogado);

            return CreateResponse(HttpStatusCode.OK, null);
        }

        [HttpPost]
        [Route("api/lancamento/reabrirLancamento")]
        public Task<HttpResponseMessage> ReabrirLancamento([FromBody] dynamic body)
        {
            _lancamentoAppService.ReabrirLancamento((int)body, UsuarioLogado);

            return CreateResponse(HttpStatusCode.OK, null);
        }

        [HttpGet]
        [Route("api/lancamento/selecionarLancamentos")]
        public Task<HttpResponseMessage> SelecionarLancamentos(int sindicanteId, int processoId)
        {
            var recibo = _lancamentoAppService.ObterRecibo(sindicanteId, processoId);

            return CreateResponse(HttpStatusCode.OK, recibo);
        }

        [HttpPost]
        [Route("api/lancamento/fecharPagamento")]
        public Task<HttpResponseMessage> FecharPagamento([FromBody] dynamic body)
        {

            var dataInicio = (DateTime) body.dataInicio;
            var dataFim = (DateTime)body.dataFim;

            var recibosIds = ((JArray)body.recibos).Select(x => (int)x["id"]).ToList();

            _lancamentoAppService.FecharPagamento(recibosIds, dataInicio, dataFim, UsuarioLogado);
            
            return CreateResponse(HttpStatusCode.OK, null);
        }

        [HttpGet]
        [Route("api/lancamento/selecionarPorSindicanteData")]
        public Task<HttpResponseMessage> SelecionarPorSindicanteData(int sindicanteId, DateTime dataInicio, DateTime dataFim)
        {
            var recibos = _lancamentoAppService.SelecionarPorSindicanteData(sindicanteId, dataInicio, dataFim);

            return CreateResponse(HttpStatusCode.OK, recibos);
        }
    }
}
