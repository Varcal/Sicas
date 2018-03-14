using System.Collections.Generic;
using System.Linq;
using Application.Account.Contracts;
using Application.Account.ViewModels.Usuarios;
using Application.Core.Services;
using Domain.Account.Contracts.Repositories;
using Domain.Account.Entities;
using Infra.Data.Core.UnitOfWork;
using SharedKernel.Security;

namespace Application.Account.Services
{
    public class UsuarioApplicationService : ApplicationService, IUsuarioApplicationService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPerfilRepository _perfilRepository;

        public UsuarioApplicationService(IUnitOfWork uow, IUsuarioRepository usuarioRepository, IPerfilRepository perfilRepository) : base(uow)
        {
            _usuarioRepository = usuarioRepository;
            _perfilRepository = perfilRepository;
        }


        public UsuarioVm Autenticar(string login, string senha)
        {
            var usuario = _usuarioRepository.Autenticar(login, StringHelper.Encrypt(senha));
           
            return new UsuarioVm(usuario);
        }

        public bool Registrar(UsuarioVm model, string user)
        {
            var perfis = RetornarPerfis(model);
            var usuario = model.ToUsuario(model, perfis);

            _usuarioRepository.Add(usuario);

            return Save(user);
        }

        public bool Alterar(UsuarioVm model, string user)
        {
            var perfis = RetornarPerfis(model);
            var usuario = _usuarioRepository.ObterPorId(model.Id);

            usuario.Alterar(model.Nome, model.Login, perfis);
            _usuarioRepository.Update(usuario);

            return Save(user);
        }

        public bool Excluir(int id, string user)
        {
            var usuario = _usuarioRepository.ObterPorId(id);
            _usuarioRepository.Delete(usuario);

            return Save(user);
        }

        public UsuarioVm ObterPorId(int id)
        {
            var usuario = _usuarioRepository.ObterPorId(id);

            return new UsuarioVm(usuario);
        }

        public UsuarioLogadoVm ObterUsuario(string login, string senha)
        {
            var usuario = _usuarioRepository.Autenticar(login, StringHelper.Encrypt(senha));
            return new UsuarioLogadoVm(usuario);
        }

        public IEnumerable<UsuarioVm> SelecionarTodosAtivos()
        {
            var usuarios = _usuarioRepository.GetAllActive();

            return usuarios.Select(usuario => new UsuarioVm(usuario)).ToList();
        }

        private List<Perfil> RetornarPerfis(UsuarioVm model)
        {
            var perfilIds = model.Perfis.Select(x => x.Id).ToList();
            var perfis = _perfilRepository.SelecionarPorIds(perfilIds).ToList();
            return perfis;
        }

        public bool VerificarSeLoginExiste(string login)
        {
            return _usuarioRepository.VerificarSeLoginExiste(login);
        }

        public bool VerificarLoginSenha(string login, string senha)
        {
            return _usuarioRepository.VerificarLoginSenha(login, StringHelper.Encrypt(senha));
        }

        public void AlterarSenha(UsuarioVm model, string usuarioLogado)
        {
            var usuario = _usuarioRepository.ObterPorId(model.Id);
            usuario.AlterarSenha(model.Senha);

            Save(usuarioLogado);
        }

        public bool VerificarSenhaPadrao(int id)
        {
            return _usuarioRepository.VerificarSenhaPadrao(id);
        }

        public void ResetarSenha(int usuarioId, string usuarioLogado)
        {
            var usuario = _usuarioRepository.ObterPorId(usuarioId);

            usuario.ResetarSenha();
            _usuarioRepository.Update(usuario);

            Save(usuarioLogado);
        }
    }
}
