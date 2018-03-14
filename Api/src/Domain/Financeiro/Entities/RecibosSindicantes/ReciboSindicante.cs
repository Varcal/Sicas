using System;
using Domain.Administrativo.Entities;
using Domain.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharedKernel.Enums;

namespace Domain.Financeiro.Entities.RecibosSindicantes
{
    public class ReciboSindicante: EntityId
    {
        public int SindicanteId { get; set; }
        public string Sindicante { get; private set; }
        public string Cpf { get; private set; }
        public string DadosBancarios { get; private set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public ICollection<ReciboSindicanteItem> ReciboSindicanteItems { get; private set; }
        public Sindicante SindicanteP { get; private set; }
        public bool Emitido { get; private set; }


        protected ReciboSindicante()
        {
            
        }

        public ReciboSindicante(Sindicante sindicante, DateTime dataInicio, DateTime dataFim, IList<Recibo> recibos)
        {
            SindicanteP = sindicante;
            Sindicante = sindicante.DadosPessoaFisica.Nome;
            Cpf = sindicante.DadosPessoaFisica.Cpf;
            DataInicio = dataInicio;
            DataFim = dataFim;
            DadosBancarios = MontarDadosBancarios(sindicante.DadosBancarios);
            ReciboSindicanteItems = recibos.Select(r => new ReciboSindicanteItem(r)).ToList();
        }

        public void Emitir()
        {
            Emitido = true;
        }

        private static string MontarDadosBancarios(DadosBancarios dadosBancarios)
        {
            var banco = dadosBancarios.Banco.Nome;
            var agencia = dadosBancarios.Agencia;
            var conta = dadosBancarios.ContaTipo == (int)ContaTipoEnum.Corrente ? "C/C" : "C/P";
         

            var sb = new StringBuilder();
            sb.Append(banco.Trim());
            sb.Append(" Ag. ");
            sb.Append(agencia.Trim());
            sb.Append(" ");
            sb.Append(conta.Trim());
            sb.Append(" ");
            sb.Append(dadosBancarios.Conta.Trim());
            if (dadosBancarios.Digito != null)
            {
                sb.Append("-");
                sb.Append(dadosBancarios.Digito.Trim());
            }

            return sb.ToString();
        }
    }
}
