using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Api.Core.Controllers.Base;
using Application.Core.Contracts;
using Application.Core.ViewModels;

namespace Api.Core.Controllers
{
    public class EnderecoController : BaseController
    {
        private readonly IEnderecoApplicationService _enderecoAppService;

        public EnderecoController(IEnderecoApplicationService enderecoAppService)
        {
            _enderecoAppService = enderecoAppService;
        }


        [HttpPost]
        [Route("api/endereco/cadastrar")]
        public Task<HttpResponseMessage> Cadastrar([FromBody] dynamic body)
        {
             var endereco = new EnderecoVm((string)body.logradouro,(string)body.endereco,(string)body.cep,(string)body.bairro,(int)body.cidade.id);

             _enderecoAppService.Cadastrar(endereco, UsuarioLogado);

            return CreateResponse(HttpStatusCode.OK, null);
        }
    }
}
