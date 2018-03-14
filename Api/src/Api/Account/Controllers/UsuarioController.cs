using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Api.Core.Controllers.Base;
using Application.Account.Contracts;
using Application.Account.ViewModels.Perfis;
using Application.Account.ViewModels.Usuarios;
using Newtonsoft.Json.Linq;

namespace Api.Account.Controllers
{
    
    public class UsuarioController : BaseController
    {
        private readonly IUsuarioApplicationService _service;

        public UsuarioController(IUsuarioApplicationService service)
        {
            _service = service;
        }


        [HttpPost]
        [Authorize(Roles = "Administrador")]
        [Route("api/usuario/registrar")]
        public Task<HttpResponseMessage> Registrar([FromBody] dynamic body)
        {
            var perfis = ((JArray) body.perfis).Select(x => new PerfilVm()
            {
                Nome = (string) x["nome"],
                Id = (int) x["id"]
            }).ToList();

            var usuarioVm = new UsuarioVm((string) body.nome, (string) body.login, (string) body.senha, perfis);
            
            _service.Registrar(usuarioVm, UsuarioLogado);

            return CreateResponse(HttpStatusCode.Created, null);
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        [Route("api/usuario/editar/{id}")]
        public Task<HttpResponseMessage> Alterar(int id, [FromBody] dynamic body)
        {
            var perfis = ((JArray)body.perfis).Select(x => new PerfilVm()
            {
                Nome = (string)x["nome"],
                Id = (int)x["id"]
            }).ToList();

            var usuarioVm = new UsuarioVm( id, (string)body.nome, (string)body.login, (string)body.senha, perfis);

            _service.Alterar(usuarioVm, UsuarioLogado);

            return CreateResponse(HttpStatusCode.Created, null);
        }

        [HttpPost]
        [Authorize]
        [Route("api/usuario/alterarSenha")]
        public Task<HttpResponseMessage> AlterarSenha([FromBody] dynamic body)
        {
            var model = new UsuarioVm((int)body.id,(string)body.nome, (string)body.login, (string)body.novaSenha, null);

            _service.AlterarSenha(model, UsuarioLogado);

            return CreateResponse(HttpStatusCode.Created, null);
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        [Route("api/usuario/excluir")]
        public Task<HttpResponseMessage> Excluir([FromBody] dynamic body)
        {
            var usuario = _service.Excluir((int)body, UsuarioLogado);

            return CreateResponse(HttpStatusCode.Created, usuario);
        }

        [HttpGet]
        [Authorize]
        [Route("api/usuario/obterPorId/{id}")]
        public Task<HttpResponseMessage> ObterPorId(int id)
        {
            var usuario = _service.ObterPorId(id);

            return CreateResponse(HttpStatusCode.Created, usuario);
        }

        [HttpGet]
        [Authorize(Roles = "Administrador")]
        [Route("api/usuario")]
        public Task<HttpResponseMessage> SelecionarTodosAtivos()
        {
            var usuarios = _service.SelecionarTodosAtivos();

            return CreateResponse(HttpStatusCode.Created, usuarios);
        }

        
        [HttpPost]
        [AllowAnonymous]
        [Route("api/usuario/obterUsuarioLogado")]
        public Task<HttpResponseMessage> ObterUsuarioLogado([FromBody] dynamic body)
        {
            var usuarioLogado = _service.ObterUsuario((string)body.login, (string)body.password);

            return CreateResponse(HttpStatusCode.Created, usuarioLogado);
        }

        [HttpGet]
        [Authorize]
        [Route("api/usuario/verificarLogin")]
        public Task<HttpResponseMessage> VerificarSeLoginExiste(string login)
        {
            var existe = _service.VerificarSeLoginExiste(login);

            return CreateResponse(HttpStatusCode.OK, existe);
        }

        [HttpGet]
        [Authorize]
        [Route("api/usuario/VerificarLoginSenha")]
        public async Task<HttpResponseMessage> VerificarLoginSenha(string login, string senha)
        {
            var usuarioValido = _service.VerificarLoginSenha(login, senha);

            return await CreateResponse(HttpStatusCode.OK, usuarioValido);
        }

        [HttpGet]
        [Authorize]
        [Route("api/usuario/verificarSenhaPadrao/{id}")]
        public async Task<HttpResponseMessage> VerificarSenhaPadrao(int id)
        {
            var senhaPadrao = _service.VerificarSenhaPadrao(id);

            return await CreateResponse(HttpStatusCode.OK, senhaPadrao);
        }


        [HttpPost]
        [Authorize(Roles = "Administrador")]
        [Route("api/usuario/resetarSenha")]
        public Task<HttpResponseMessage> ResetarSenha([FromBody] dynamic body)
        {
             var usuarioId = (int)body;
            _service.ResetarSenha(usuarioId, UsuarioLogado);

            return CreateResponse(HttpStatusCode.OK, null);
        } 

    }
}