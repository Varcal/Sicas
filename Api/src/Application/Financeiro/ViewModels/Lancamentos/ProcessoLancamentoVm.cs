using System;

namespace Application.Financeiro.ViewModels.Lancamentos
{
    class ProcessoLancamentoVm
    {
        public int Id { get; set; }
        public string Numero { get; set; }
        public string NumeroSinistro { get; set; }
        public string Seguradora { get; set; }
        public string Segurado { get; set; }
        public string Sindicante { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
        public DateTime? EmitidoParecerEm { get; set; }
        public int Dias { get; set; }
        public bool PossuiDespesas { get; set; }
    }
}
