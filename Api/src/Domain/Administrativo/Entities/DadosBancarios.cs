using Domain.Core.Entities;

namespace Domain.Administrativo.Entities
{
    public class DadosBancarios : EntityId
    {
        public int BancoId { get; private set; }
        public int ContaTipo { get; private set; }
        public string Agencia { get; private set; }
        public string Conta { get; private set; }
        public string Digito { get; private set; }

        public Banco Banco { get; private set; }

        protected DadosBancarios()
        {
            
        }


        public DadosBancarios(int bancoId, int contaTipo, string agencia, string conta, string digito)
        {
            BancoId = bancoId;
            ContaTipo = contaTipo;
            Agencia = agencia;
            Conta = conta;
            Digito = digito;
        }

        public void Alterar(DadosBancarios dadosBancarios)
        {
            BancoId = dadosBancarios.BancoId;
            ContaTipo = dadosBancarios.ContaTipo;
            Agencia = dadosBancarios.Agencia;
            Conta = dadosBancarios.Conta;
            Digito = dadosBancarios.Digito;
        }

    }
}
