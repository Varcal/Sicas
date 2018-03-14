using System;
using System.Linq;
using Domain.Administrativo.Entities;
using SharedKernel.Enums;
using SharedKernel.Helpers;

namespace Application.Administrativo.ViewModels.Processos
{
    public class ProcessoListVm
    {
        public int Id { get; set; }
        public string Numero { get; set; }
        public string NumeroSinistro { get; set; }
        public int SeguradoraId { get; set; }
        public string Seguradora { get; set; }
        public string Segurado { get; set; }
        public string Sindicante { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
        public DateTime? EmitidoParecerEm { get; set; }
        public DateTime? FinalizadoEm { get; set; }
        public int Dias { get; set; }
        public bool PossuiDespesas { get; set; }
        public int? AcionamentoId { get; set; }

        public ProcessoListVm()
        {

        }

        public ProcessoListVm(Processo processo)
        {
            Id = processo.Id;
            Numero = processo.NumeroReferencia;
            NumeroSinistro = processo.NumeroSinistro;
            SeguradoraId = processo.SeguradoraId;
            Seguradora = processo.Seguradora.DadosPessoaJuridica.RazaoSocial;
            Sindicante = processo.Sindicantes.FirstOrDefault().DadosPessoaFisica.Nome;
            Segurado = processo.Segurado.Nome;
            StatusId = processo.StatusId;
            Status = ((StatusEnum) processo.StatusId).GetDescription();
            EmitidoParecerEm = processo.EmitidoParecerEm == null ?  processo.EmitidoParecerEm : processo.EmitidoParecerEm.Value;
            FinalizadoEm = processo.FinalizadoEm == null ? processo.FinalizadoEm : processo.FinalizadoEm.Value;
            Dias = processo.EmitidoParecerEm == null
                ? Helper.CalcularDiasEmAberto(processo.DataCadastro.Date, DateTime.Now.Date)
                : Helper.CalcularDiasEmAberto(processo.DataCadastro.Date, processo.EmitidoParecerEm.Value.Date);
            PossuiDespesas = processo.Despesas.Count > 0;
            AcionamentoId = processo.AcionamentoId;
        }
    }
}
