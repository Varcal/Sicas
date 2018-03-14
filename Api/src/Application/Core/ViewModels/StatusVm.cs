namespace Application.Core.ViewModels
{
    public class StatusVm
    {
        public int Id { get; set; }
        public string Descricao { get; set; }

        public StatusVm(int id, string descricao)
        {
            Id = id;
            Descricao = descricao;
        }
    }
}
