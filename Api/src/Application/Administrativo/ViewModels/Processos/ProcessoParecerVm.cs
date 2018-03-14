using System;
using System.Linq;
using System.Web;
using Application.Core.ViewModels;
using Domain.Administrativo.Entities;

namespace Application.Administrativo.ViewModels.Processos
{
    public class ProcessoParecerVm
    {
        public int Id { get; set; }
        public string Seguradora { get; set; }
        public string Cnpj { get; set; }
        public string ServicoSeguradora { get; set; }
        public string EventoTipo { get; set; }
        public string Sindicante { get; set; }
        public string ServicoSindicante { get; set; }
        public string NumeroSinistro { get; set; }
        public string NumeroReferencia { get; set; }
        public string Analista { get; set; }
        public int Situacao { get; set; }
        public int StatusId { get; set; }
        public string Comentario { get; set; }
        public string Placa { get; set; }
        public DateTime DataCadastro { get; set; }
        public HttpPostedFile Arquivos { get; set; }
        public SeguradoVm Segurado { get; set; }
        public EnderecoVm LocalFatos { get; set; }
        public CidadeVm Cidade { get; set; }
        public EstadoVm Estado { get; set; }

        public ProcessoParecerVm()
        {
            
        }

        public ProcessoParecerVm(Processo processo)
        {
            Id = processo.Id;
            Seguradora = processo.Seguradora.DadosPessoaJuridica.RazaoSocial;
            Cnpj = processo.Seguradora.DadosPessoaJuridica.Cnpj;
            ServicoSeguradora = processo.ServicoSeguradora.Nome;
            EventoTipo = processo.EventoTipo.Nome;
            Sindicante = processo.Sindicantes.FirstOrDefault().DadosPessoaFisica.Nome;
            ServicoSindicante = processo.ServicoSindicante.Nome;
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
    }
}
