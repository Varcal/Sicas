using Domain.Administrativo.Entities;

namespace Application.Administrativo.ViewModels.ServicosSindicantes
{
    public class ServicoSindicanteVm
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public ServicoSindicanteVm()
        {
            
        }

        public ServicoSindicanteVm(ServicoSindicante servicoSindicante)
        {
            Id = servicoSindicante.Id;
            Nome = servicoSindicante.Nome;
        }
    }
}
