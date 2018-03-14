namespace Application.Administrativo.ViewModels.Processos
{
    public class AcionamentoVm
    {
        public string Nome { get; set; }
        public byte[] Bytes { get; set; }

        public AcionamentoVm()
        {
            
        }

        public AcionamentoVm(string nome, byte[] bytes)
        {
            Nome = nome;
            Bytes = bytes;
        }
    }
}
