using Domain.Core.Entities.Enderecos;

namespace Application.Core.ViewModels
{
    public class EstadoVm
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Uf { get; set; }

        public EstadoVm()
        {
            
        }

        public EstadoVm(Estado estado)
        {
            Id = estado.Id;
            Nome = estado.Nome;
            Uf = estado.Uf;
        }
    }
}
