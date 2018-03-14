using System.Collections.Generic;
using System.Linq;
using Domain.Financeiro.Entities.RecibosSindicantes;

namespace CrossCutting.Reports.RecibosSindicantes
{
    public class ReciboDto
    {
        public string Numero { get; set; }
        public string Sindicante { get; set; }
        public string Cpf { get; set; }
        public string DadosBancarios { get; set; } 
        public IEnumerable<LancamentoDto> Lancamentos { get; set; }
        public decimal Total { get; set; }


        public ReciboDto(ReciboSindicante reciboSindicante)
        {
            Numero = reciboSindicante.DataCadastro.Year + reciboSindicante.Id.ToString().PadLeft(4, '0');
            Sindicante = reciboSindicante.Sindicante;
            Cpf = reciboSindicante.Cpf;
            DadosBancarios = reciboSindicante.DadosBancarios;
            Lancamentos = reciboSindicante.ReciboSindicanteItems.Select(r => new LancamentoDto(r)).ToList();
            Total = reciboSindicante.ReciboSindicanteItems.Sum(x => x.Honorario) +
                    reciboSindicante.ReciboSindicanteItems.Sum(x => x.Despesa) +
                    reciboSindicante.ReciboSindicanteItems.Sum(x => x.OutrosCreditos) -
                    reciboSindicante.ReciboSindicanteItems.Sum(x=>x.Desconto);
        }     
    }
}
