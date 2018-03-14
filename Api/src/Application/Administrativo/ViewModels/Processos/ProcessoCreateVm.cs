using System;
using Application.Core.ViewModels;

namespace Application.Administrativo.ViewModels.Processos
{
    public class ProcessoCreateVm
    {
        public DateTime DataCadastro { get; set; }
        public int SeguradoraId { get;  set; }
        public int ServicoSeguradoraId { get;  set; }
        public int EventoTipoId { get;  set; }
        public int SindicanteId { get;  set; }
        public int ServicoSindicanteId { get;  set; }
        public string NumeroSinistro { get;  set; }
        public string Analista { get; set; }
        public string Placa { get; set; }
        public int CidadeId { get; set; }
        public string NumeroReferencia { get;  set; }
        public SeguradoVm Segurado { get;  set; }
        public EnderecoVm LocalFatos { get; set; }
        public int Situacao { get;  set; }
        public int StatusId { get;  set; }
        public byte[] Arquivo { get; set; }
    }
}
