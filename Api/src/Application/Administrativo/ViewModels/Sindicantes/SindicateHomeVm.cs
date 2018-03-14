using Domain.Dtos;

namespace Application.Administrativo.ViewModels.Sindicantes
{
    public class SindicateHomeVm
    {
        public string Nome { get; set; }
        public int PrimeiraQuinzena { get; set; }
        public int SegundaQuinzena { get; set; }


        protected SindicateHomeVm()
        {
            
        }


        public SindicateHomeVm(SindicanteHomeDto sindicante)
        {
            Nome = sindicante.Nome;
            PrimeiraQuinzena = sindicante.PrimeiraQuinzena;
            SegundaQuinzena = sindicante.SegundaQuinzena;
        }
    }
}
