using Domain.Administrativo.Entities;

namespace Application.Administrativo.ViewModels.Processos
{
    public class SeguradoVm
    {
        public string Nome { get; set; }
        //public EnderecoVm EnderecoSegurado { get; set; }


        public SeguradoVm()
        {
            
        }

        public SeguradoVm(Segurado segurado)
        {
            Nome = segurado.Nome;
           //EnderecoSegurado = new EnderecoVm(segurado.EnderecoSegurado);
        }
    }
}