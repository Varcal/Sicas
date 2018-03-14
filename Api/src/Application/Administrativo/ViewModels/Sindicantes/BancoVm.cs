using Domain.Administrativo.Entities;

namespace Application.Administrativo.ViewModels.Sindicantes
{
    public class BancoVm
    {
        public int Id { get; set; }
        public string Nome { get; set; }


        public BancoVm()
        {
            
        }

        public BancoVm(Banco banco)
        {
            Id = banco.Id;
            Nome = banco.Numero.Trim().PadLeft(3, '0') + " - " + banco.Nome;
        }
    }
}
