namespace Domain.Dtos
{
    public class AlertaDto
    {
        public int TotalAbertoMais5Dias { get; private set; }
        public int TotalAberto3Dias { get; private set; }
        public int TotalAguradandoParecer { get; private set; }
        public int TotalFinalizadosNoMes { get; private set; }
      

        public AlertaDto(int totalAbertoMais5Dias, int totalAberto3Dias, int totalAguradandoParecer, int totalFinalizadosNoMes)
        {
            TotalAbertoMais5Dias = totalAbertoMais5Dias;
            TotalAberto3Dias = totalAberto3Dias;
            TotalAguradandoParecer = totalAguradandoParecer;
            TotalFinalizadosNoMes = totalFinalizadosNoMes;
        }

    }
}
