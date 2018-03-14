using Domain.Core.Entities.Enderecos;

namespace Application.Core.ViewModels
{
    public class CidadeVm
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int EstadoId { get; set; }

        public CidadeVm()
        {
            
        }

        public CidadeVm(Cidade cidade)
        {
            Id = cidade.Id;
            Nome = cidade.Nome;
            EstadoId = cidade.EstadoId;
        }
    }
}
