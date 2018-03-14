using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using Api.Core.Controllers.Base;
using Application.Administrativo.Contracts;
using Application.Administrativo.ViewModels.Processos;
using System.Web;

namespace Api.Administrativo.Controllers
{
    [Authorize]
    public class ProcessoController : BaseController
    {

        private readonly IProcessoApplicationService _processoApplicationService;

        public ProcessoController(IProcessoApplicationService processoApplicationService)
        {
            _processoApplicationService = processoApplicationService;
        }

        [HttpPost]
        [Route("api/processo/registrar")]
        public Task<HttpResponseMessage> Registrar([FromBody] dynamic body)
        {
            var processo = new ProcessoCreateVm()
            {
                DataCadastro = (DateTime)body.dataCadastro,
                EventoTipoId = (int)body.eventoTipo.id,
                ServicoSeguradoraId = (int)body.servicoSeguradora.id,
                NumeroReferencia = (string)body.numeroReferencia,
                SeguradoraId = (int)body.seguradora.id,
                ServicoSindicanteId = (int)body.servicoSindicante.id,
                SindicanteId = (int)body.sindicante.id,
                NumeroSinistro = (string)body.numeroSinistro,
                Segurado = new SeguradoVm
                {
                    Nome = (string)body.segurado.nome
                    //EnderecoSegurado = new EnderecoVm((string)body.segurado.enderecoSegurado.logradouro,
                    //(string)body.segurado.enderecoSegurado.numero, (string)body.segurado.enderecoSegurado.cep,
                    //(string)body.segurado.enderecoSegurado.bairro, (int)body.segurado.enderecoSegurado.cidade.id,
                    //(string)body.segurado.enderecoSegurado.complemento)
                },
                //LocalFatos = new EnderecoVm((string)body.localFatos.logradouro, (string)body.localFatos.numero, (string)body.localFatos.cep, (string)body.localFatos.bairro, (int)body.localFatos.cidade.id, (string)body.localFatos.complemento),
                Analista = (string)body.analista,
                Placa = (string)body.placa,
                CidadeId = (int)body.cidade.id,
                Arquivo = (string)body.arquivo != null ? Convert.FromBase64String(((string)body.arquivo).Replace("data:;base64,", "")) : null
        };

           _processoApplicationService.Registrar(processo, User.Identity.Name);

            return CreateResponse(HttpStatusCode.Created, null);
        }

        [HttpPost]
        [Route("api/processo/editar/{id}")]
        public Task<HttpResponseMessage> Editar(int id, [FromBody] dynamic body)
        {
            var processo = new ProcessoEditVm()
            {
                Id = id,
                EventoTipoId = (int)body.eventoTipo.id,
                ServicoSeguradoraId = (int)body.servicoSeguradora.id,
                NumeroReferencia = (string)body.numeroReferencia,
                SeguradoraId = (int)body.seguradora.id,
                ServicoSindicanteId = (int)body.servicoSindicante.id,
                SindicanteId = (int)body.sindicante.id,
                NumeroSinistro = (string)body.numeroSinistro,
                Segurado = new SeguradoVm
                {
                    Nome = (string)body.segurado.nome
                    //EnderecoSegurado = new EnderecoVm((string)body.segurado.enderecoSegurado.logradouro,
                    //(string)body.segurado.enderecoSegurado.numero, (string)body.segurado.enderecoSegurado.cep,
                    //(string)body.segurado.enderecoSegurado.bairro, (int)body.segurado.enderecoSegurado.cidade.id,
                    //(string)body.segurado.enderecoSegurado.complemento)
                },
                //LocalFatos = new EnderecoVm((string)body.localFatos.logradouro, (string)body.localFatos.numero, (string)body.localFatos.cep, (string)body.localFatos.bairro, (int)body.localFatos.cidade.id, (string)body.localFatos.complemento),
                Analista = (string)body.analista,
                Placa = (string)body.placa,
                CidadeId = (int)body.cidade.id,
                Arquivo = (string)body.arquivo != null ? Convert.FromBase64String(((string)body.arquivo).Replace("data:application/pdf;base64,", "")) : null
            };

            _processoApplicationService.Editar(processo, User.Identity.Name);

            return CreateResponse(HttpStatusCode.Created, null);
        }

        [HttpPost]
        [Route("api/processo/cancelarProcesso")]
        public Task<HttpResponseMessage> Cancelar([FromBody] dynamic body)
        {
            _processoApplicationService.CancelarProcesso((int)body, UsuarioLogado);

            return CreateResponse(HttpStatusCode.Created, null);
        }

        [HttpGet]
        [Route("api/processo/selecionarPorSeguradora")]
        public Task<HttpResponseMessage> SelecionarPorSeguradora(int seguradoraId, int statusId, int alerta)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var sid = Convert.ToInt32(claimsIdentity.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault());

            var processos = _processoApplicationService.SelecionarPorSeguradora(seguradoraId, statusId, alerta, sid);

            return CreateResponse(HttpStatusCode.OK, processos);
        }

        [HttpGet]
        [Route("api/processo/selecionarPorSeguradoraCombo/{seguradoraId}")]
        public Task<HttpResponseMessage> SelecionarPorSeguradoraCombo(int seguradoraId)
        {
            var processos = _processoApplicationService.SelecionarPorSeguradorasAtivas(seguradoraId);

            return CreateResponse(HttpStatusCode.OK, processos);
        }

        [HttpGet]
        [Route("api/processo/selecionarPorSeguradoraParaDespesas/{seguradoraId}")]
        public Task<HttpResponseMessage> SelecionarPorSeguradoraParaDespesas(int? seguradoraId)
        {
            var processos = _processoApplicationService.SelecionarPorSeguradoraParaDespesas(seguradoraId);

            return CreateResponse(HttpStatusCode.OK, processos);
        }

        [HttpGet]
        [Route("api/processo/selecionarPorSeguradoraData")]
        public Task<HttpResponseMessage> SelecionarPorSeguradora(int seguradoraId, DateTime dataInicio, DateTime dataFim)
        {
            var processos = _processoApplicationService.SelecionarPorSeguradoraData(seguradoraId, dataInicio, dataFim);

            return CreateResponse(HttpStatusCode.OK, processos);
        }

        [HttpGet]
        [Route("api/processo/obterPorId/{id}")]
        public Task<HttpResponseMessage> ObterPorId(int id)
        {
            var processo = _processoApplicationService.ObterPorId(id);

            return CreateResponse(HttpStatusCode.OK, processo);
        }

        [HttpGet]
        [Route("api/processo/obterParaParecer/{id}")]
        public Task<HttpResponseMessage> ObterParaParecer(int id)
        {
            var processo = _processoApplicationService.ObterParaParecer(id);

            return CreateResponse(HttpStatusCode.OK, processo);
        }

        [HttpGet]
        [Route("api/processo/selecionarComHistorico")]
        public Task<HttpResponseMessage> SelecionarComHistorico(int seguradoraId, string numeroSinistro)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var sid = Convert.ToInt32(claimsIdentity.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault());

            var processos = _processoApplicationService.SelecionarHistoricos(seguradoraId, numeroSinistro, sid);

            return CreateResponse(HttpStatusCode.OK, processos);
        }

        [HttpPost]
        [Route("api/processo/finalizarParecer")]
        public async Task<HttpResponseMessage> FinalizarParecer()
        {
            var context = HttpContext.Current;
            var id = Convert.ToInt32(context.Request.Params["id"]);
            var situacaoId = Convert.ToInt32(context.Request.Params["situacaoId"]);
            var comentario = context.Request.Params["comentario"];
            var file = context.Request.Files[0];

            var model = new ProcessoParecerVm
            {
                Id = id,
                Situacao = situacaoId,
                Comentario = comentario,
                Arquivos = file
            };

            _processoApplicationService.FinalizarParecer(model, UsuarioLogado);

            return await CreateResponse(HttpStatusCode.Created, null);
        }

        [HttpPost]
        [Route("api/processo/finalizarAnalise")]
        public Task<HttpResponseMessage> FinalizarAnalise([FromBody] dynamic body)
        {
            _processoApplicationService.FinalizarAnalise((int)body, UsuarioLogado);

            return CreateResponse(HttpStatusCode.Created, null);
        }

        [HttpPost]
        [Route("api/processo/finalizarDespesas")]
        public Task<HttpResponseMessage> FinalizarDespesas([FromBody] dynamic body)
        {
            _processoApplicationService.FinalizarDespesas((int)body, UsuarioLogado);

            return CreateResponse(HttpStatusCode.Created, null);
        }

        [HttpPost]
        [Route("api/processo/enviarSindicanteExterno")]
        public Task<HttpResponseMessage> EnviarSindicanteExterno([FromBody] dynamic body)
        {
            _processoApplicationService.EnviarSindicanteExterno((int) body.id, (int)body.sindicanteExternoId, UsuarioLogado);

            return CreateResponse(HttpStatusCode.OK, null);
        }

        [HttpPost]
        [Route("api/processo/finalizar")]
        public Task<HttpResponseMessage> Finalizar([FromBody] dynamic body)
        {
            var arquivo = ((string) body.arquivo).Split(',');

            var processoFinalizar = new ProcessoFinalizarVm
            {
                Id = (int)body.id,
                NumeroNF = (int)body.numeroNF,
                DataEmissaoNF = (DateTime)body.dataEmissaoNF,
                Arquivo = Convert.FromBase64String(arquivo[1]) 
            };

            _processoApplicationService.Finalizar(processoFinalizar,  UsuarioLogado);

            return CreateResponse(HttpStatusCode.OK, null);
        }


        [HttpGet]
        [Route("api/processo/downloadArquivo/{id}")]
        public HttpResponseMessage DownloadArquivos(int id)
        {
            if (User?.Identity != null && User.Identity.IsAuthenticated)
            {

                var arquivo = _processoApplicationService.BaixarAquivo(id);

                var response = new HttpResponseMessage
                {
                    Content = new StreamContent(arquivo.Bytes)
                };

                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentDisposition.FileName = arquivo.Nome;
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response.Content.Headers.ContentLength = arquivo.Length;

                return response;
            }

            return new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }


        [HttpGet]
        [AllowAnonymous]
        [Route("api/processo/downloadAcionamento/{id}")]
        public HttpResponseMessage DownloadAcionamento(int id)
        {
            if (User?.Identity != null && User.Identity.IsAuthenticated)
            {
                var acionamento = _processoApplicationService.BaixarAcionamento(id);

                var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(acionamento.Bytes)
                };

                response.Content = new ByteArrayContent(acionamento.Bytes);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("inline")
                {
                    FileName = String.Format(acionamento.Nome + DateTime.Now.Year + DateTime.Now.Month + ".rar")

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
