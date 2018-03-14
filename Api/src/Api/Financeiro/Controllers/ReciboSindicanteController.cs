using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using Api.Core.Controllers.Base;
using Application.Financeiro.Contracts;

namespace Api.Financeiro.Controllers
{
    public class ReciboSindicanteController : BaseController
    {
        private readonly IReciboSindicanteAppService _reciboSindicanteAppService;

        public ReciboSindicanteController(IReciboSindicanteAppService reciboSindicanteAppService)
        {
            _reciboSindicanteAppService = reciboSindicanteAppService;
        }

        [HttpGet]
        [Route("api/reciboSindicante/selecionarReciboSindicantePorData/")]
        public Task<HttpResponseMessage> SelecionarPorSindicanteData(DateTime dataInicio, DateTime dataFim)
        {
            var recibos = _reciboSindicanteAppService.SelecionarPorSindicanteData(dataInicio, dataFim);

            return CreateResponse(HttpStatusCode.OK, recibos);
        }

        [HttpGet]
        [Route("api/reciboSindicante/emitirRecibo/{id}")]
        public HttpResponseMessage EmitirRecibo(int id)
        {

            if (User?.Identity != null && User.Identity.IsAuthenticated)
            {
                var recibo = _reciboSindicanteAppService.EmitirRecibo(id, UsuarioLogado);

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

        [HttpGet]
        [Route("api/reciboSindicante/emitirTodos")]
        public HttpResponseMessage EmitirTodos(DateTime dataInicio, DateTime dataFim)
        {

            if (User?.Identity != null && User.Identity.IsAuthenticated)
            {
                var arquivo = _reciboSindicanteAppService.EmitirTodos(dataInicio, dataFim, UsuarioLogado);

                var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(arquivo.Bytes)
                };

                response.Content.Headers.Add("x-filename", arquivo.Nome);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentDisposition.FileName = arquivo.Nome;
                
                return response;
            }

            return new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }


    }
}
