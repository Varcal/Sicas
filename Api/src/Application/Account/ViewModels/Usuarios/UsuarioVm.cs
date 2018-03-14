using System.Collections.Generic;
using System.Linq;
using Application.Account.ViewModels.Perfis;
using Domain.Account.Entities;

namespace Application.Account.ViewModels.Usuarios
{
    public class UsuarioVm
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public ICollection<PerfilVm> Perfis { get; set; }

        public UsuarioVm(Usuario usuario)
        {
            Id = usuario.Id;
            Nome = usuario.Nome;
            Login = usuario.Login;
            Senha = usuario.Senha;
            Perfis = usuario.Perfis.Select(perfil => new PerfilVm(perfil)).ToList();
        }

        public UsuarioVm(string nome, string login, string password, ICollection<PerfilVm> perfis)
        {
            Nome = nome;
            Login = login;
            Senha = password;
            Perfis = perfis;
        }

        public UsuarioVm(int id, string nome, string login, string password, ICollection<PerfilVm> perfis)
            :this(nome,  login,  password,  perfis)
        {
            Id = id;
            Nome = nome;
            Login = login;
            Senha = password;
            Perfis = perfis;
        }

        public Usuario ToUsuario(UsuarioVm usuario, ICollection<Perfil> perfis)
        {
            return new Usuario(usuario.Nome, usuario.Login, perfis);
        }
    }
}
