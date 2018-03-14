using Domain.Account.Entities;

namespace Application.Account.ViewModels.Perfis
{
    public class PerfilVm
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public PerfilVm()
        {
            
        }

        public PerfilVm(Perfil perfil)
        {
            Id = perfil.Id;
            Nome = perfil.Nome;
        }
    }
}
