namespace Domain.Dtos
{
    public class SindicanteHomeDto
    {
        public string Nome { get; set; }
        public int PrimeiraQuinzena { get; set; }
        public int SegundaQuinzena { get; set; }

        public SindicanteHomeDto(string nome, int primeiraQuinzena, int segundaQuinzena)
        {
            Nome = nome;
            PrimeiraQuinzena = primeiraQuinzena;
            SegundaQuinzena = segundaQuinzena;
        }
    }
}
