using System;
using System.Linq;
using Domain.Financeiro.Entities.RecibosSindicantes;

namespace Application.Financeiro.ViewModels.Lancamentos
{
    public class ReciboSindicanteRelatorioVm
    {
        public int Id { get; set; }
        public string Numero { get; private set; }
        public string Sindicante { get; private set; }
        public string Cpf { get; private set; }
        public string DadosBancario { get; private set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public decimal Total { get; private set; }
        public bool Emitido { get; set; } 

        public ReciboSindicanteRelatorioVm(ReciboSindicante reciboSindicante)
        {
            Id = reciboSindicante.Id;
            Numero = reciboSindicante.DataCadastro.Year + reciboSindicante.Id.ToString().PadLeft(4,'0');
            Sindicante = reciboSindicante.Sindicante;
            Cpf = reciboSindicante.Cpf;
            DadosBancario = reciboSindicante.DadosBancarios;
            DataInicio = reciboSindicante.DataInicio;
            DataFim = reciboSindicante.DataFim;
            Total = reciboSindicante.ReciboSindicanteItems.Sum(x => x.Honorario + x.OutrosCreditos + x.Despesa - x.Desconto);
            Emitido = reciboSindicante.Emitido;
        }
    }
}
