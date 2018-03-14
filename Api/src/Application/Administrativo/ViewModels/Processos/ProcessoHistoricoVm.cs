using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Domain.Administrativo.Entities;
using SharedKernel.Enums;
using SharedKernel.Helpers;

namespace Application.Administrativo.ViewModels.Processos
{
    public class ProcessoHistoricoVm
    {
        public int Id { get; set; }
        public string Numero { get; set; }
        public string NumeroSinistro { get; set; }
        public string Seguradora { get; set; }
        public string Sindicante { get; set; }
        public string IniciadoEm { get; set; }
        public string FinalizadoEm { get; set; }
        public string Status { get; set; }
        public int DiasEmAberto { get; set; }

        public ProcessoHistoricoVm()
        {

        }

        public ProcessoHistoricoVm(int id, string numero, string numeroSinistro, string seguradora, string sindicante, string iniciadoEm, string finalizadoEm, string status, int diasEmAberto)
        {
            Id = id;
            Numero = numero;
            NumeroSinistro = numeroSinistro;
            Seguradora = seguradora;
            Sindicante = sindicante;
            IniciadoEm = iniciadoEm;
            FinalizadoEm = finalizadoEm;
            Status = status;
            DiasEmAberto = diasEmAberto;
        }

        public static IEnumerable<ProcessoHistoricoVm> RetornaHistorico(Processo processo)
        {


            return processo?.Historicos.Select(processoHistorico => new ProcessoHistoricoVm(
                processoHistorico.Id, 
                processo.NumeroReferencia,
                processo.NumeroSinistro, 
                processo.Seguradora.DadosPessoaJuridica.RazaoSocial, 
                RetornaNome(processo, processoHistorico),
                processoHistorico.IniciadoEm.ToShortDateString(), 
                processoHistorico.FinalizadoEm?.ToShortDateString() ?? "Em Aberto",
                processoHistorico.Status.Nome,
                processo.FinalizadoEm == null ? Helper.CalcularDiasEmAberto(processoHistorico.IniciadoEm.Date, DateTime.Now.Date) : 
                    Helper.CalcularDiasEmAberto(processoHistorico.IniciadoEm.Date, processo.FinalizadoEm.Value.Date))
            ).ToList();
        }

        private static string RetornaNome(Processo processo, ProcessoHistorico processoHistorico)
        {
            if (processo.Sindicantes.Count > 1)
            {
                return processoHistorico.StatusId == (int) StatusEnum.EmAnalise
                    ? processo.Sindicantes.FirstOrDefault(x => x.SindicanteTipoId == (int) SindicanteTipoEnum.Interno).DadosPessoaFisica.Nome
                    : processo.Sindicantes.FirstOrDefault(x => x.SindicanteTipoId == (int) SindicanteTipoEnum.Externo).DadosPessoaFisica.Nome;
            }


            return processo.Sindicantes.FirstOrDefault().DadosPessoaFisica.Nome;
        }
     
    }
}
