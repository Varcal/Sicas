namespace Domain.Administrativo.ValueObjects
{
    public class Diretorio
    {
        public string Administrativo { get; private set; }
        public string Financeiro { get; private set; }

        public Diretorio(string administrativo, string financeiro)
        {
            Administrativo = administrativo;
            Financeiro = financeiro;
        }
    }
}
