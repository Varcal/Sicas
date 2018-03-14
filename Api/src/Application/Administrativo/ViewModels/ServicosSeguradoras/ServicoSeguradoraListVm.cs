using Domain.Administrativo.Entities;

namespace Application.Administrativo.ViewModels.ServicosSeguradoras
{

    public class ServicoSeguradoraListVm
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public ServicoSeguradoraListVm()
        {

        }


        public ServicoSeguradoraListVm(ServicoSeguradora servicoSeguradora)
        {
            Id = servicoSeguradora.Id;
            Nome = servicoSeguradora.Nome;
        }
    }
}