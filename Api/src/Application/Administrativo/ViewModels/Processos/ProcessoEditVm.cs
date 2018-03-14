using System;
using System.Linq;
using Application.Core.ViewModels;
using Domain.Administrativo.Entities;
using SharedKernel.Enums;

namespace Application.Administrativo.ViewModels.Processos
{
    public class ProcessoEditVm
    {
        public int Id { get; set; }
        public int SeguradoraId { get; set; }
        public int ServicoSeguradoraId { get; set; }
        public int EventoTipoId { get; set; }
        public int SindicanteId { get; set; }
        public string SindicanteEnviado { get; set; }
        public int ServicoSindicanteId { get; set; }
        public string NumeroSinistro { get; set; }
        public string NumeroReferencia { get; set; }
        public string Analista { get; set; }
        public string Placa { get; set; }
        public int CidadeId { get; set; }
        public SeguradoVm Segurado { get; set; }
        public EnderecoVm LocalFatos { get; set; }
        public int Situacao { get; set; }
        public int StatusId { get; set; }
        public DateTime DataCadastro { get; set; }
        public CidadeVm Cidade { get; set; }
        public EstadoVm Estado { get; set; }
        public byte[] Arquivo { get; set; }


        public ProcessoEditVm()
        {
            
        }

        public ProcessoEditVm(Processo processo)
        {
            Id = processo.Id;
            SeguradoraId = processo.SeguradoraId;
            ServicoSeguradoraId = processo.ServicoSeguradoraId;
            EventoTipoId = processo.EventoTipoId;
            SindicanteId = SindicantePrincipal(processo);
            SindicanteEnviado = SindicanteProcessoEnviado(processo);
            ServicoSindicanteId = processo.ServicoSindicanteId;
            NumeroSinistro = processo.NumeroSinistro;
            NumeroReferencia = processo.NumeroReferencia;
            Analista = processo.Analista;
            Segurado = new SeguradoVm(processo.Segurado);
            //LocalFatos = new EnderecoVm(processo.LocalFatos);
            Situacao = (int)processo.Situacao;
            StatusId = processo.StatusId;
            DataCadastro = processo.DataCadastro;
            Cidade = new CidadeVm(processo.Cidade);
            Estado = new EstadoVm(processo.Cidade.Estado);
            Placa = processo.Placa;
        }

        private static string SindicanteProcessoEnviado(Processo processo)
        {
            return processo.Sindicantes.Count > 1
                ? processo.Sindicantes.FirstOrDefault(x => x.SindicanteTipoId == (int) SindicanteTipoEnum.Externo).DadosPessoaFisica.Nome
                : null;
        }

        private static int SindicantePrincipal(Processo processo)
        {
            return processo.Sindicantes.Count > 1
                ? processo.Sindicantes.FirstOrDefault(x => x.SindicanteTipoId == (int) SindicanteTipoEnum.Interno).Id
                : processo.Sindicantes.FirstOrDefault().Id;
        }
    }
}
