using System.Collections.Generic;
using System.Linq;
using Domain.Account.Entities;

namespace Application.Account.ViewModels.Usuarios
{
    public class UsuarioLogadoVm
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }   
        public IList<string> Perfis { get; set; }

        public UsuarioLogadoVm()
        {
            
        }

        public UsuarioLogadoVm(Usuario usuario)
        {
            Id = usuario.Id;
            Nome = usuario.Nome;
            Login = usuario.Login;
            Perfis = usuario.Perfis.Select(perfil => perfil.Nome).ToList();
        }
    }
}
