using Domain.Administrativo.Entities;

namespace Application.Administrativo.ViewModels.Sindicantes
{
    public class DadosBancariosVm
    {
        public int BancoId { get; set; }
        public int ContaTipo { get; set; }
        public string Agencia { get; set; }
        public string Conta { get; set; }
        public string Digito { get; set; }

        public DadosBancariosVm()
        {
            
        }

        public DadosBancariosVm(DadosBancarios dadosBancarios)
        {
            BancoId = dadosBancarios.BancoId;
            ContaTipo = dadosBancarios.ContaTipo;
            Agencia = dadosBancarios.Agencia;
            Conta = dadosBancarios.Conta;
            Digito = dadosBancarios.Digito;
        }
    }
}
