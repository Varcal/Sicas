using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using Domain.Account.Contracts.Repositories;
using Domain.Account.Entities;
using Infra.Data.Contexts;
using Infra.Data.Core.Repositories.Base;
using SharedKernel.Security;

namespace Infra.Data.Account.Repositories
{
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(EfContext context) 
            : base(context)
        {
        }

        public Usuario Autenticar(string login, string senha)
        {
           return Context.Usuarios.Include(x=>x.Perfis).AsNoTracking().SingleOrDefault(x => x.Login == login && x.Senha == senha && x.Ativo);
        }

        public Usuario ObterPorId(int id)
        {
            return Context.Usuarios.Include(x => x.Perfis).SingleOrDefault(x => x.Id == id);
        }

        public bool VerificarSeLoginExiste(string login)
        {
            return Context.Usuarios.Select(x => x.Login).Contains(login);
        }

        public IEnumerable<Perfil> SelecionarPerfis(int usuarioId)
        {
            var perfis = Context.Usuarios.Where(x=>x.Id == usuarioId).Select(x => x.Perfis).SingleOrDefault();
            return perfis;
        }

        public IEnumerable<Usuario> SelecionarTodos()
        {
            return Context.Usuarios.Include(p=>p.Perfis).AsNoTracking().ToList();
        }

        public bool VerificarLoginSenha(string login, string senha)
        {
            return Context.Usuarios.Any(x=>x.Login == login && x.Senha == senha);
        }

        public bool VerificarSenhaPadrao(int id)
        {
            var senhaPadrao = StringHelper.Encrypt(ConfigurationManager.AppSettings["senhaPadrao"]);

            return Context.Usuarios.Any(x => x.Id == id && x.Senha == senhaPadrao);
        }
    }
}
