namespace Application.Administrativo.ViewModels.Sindicantes
{
    public class SindicanteDeleteVm
    {
        public int Id { get; private set; }
        public int SindicanteTipoId { get; private set; }

        public SindicanteDeleteVm(int id, int sindicanteTipoId)
        {
            Id = id;
            SindicanteTipoId = sindicanteTipoId;
        }
    }
}
