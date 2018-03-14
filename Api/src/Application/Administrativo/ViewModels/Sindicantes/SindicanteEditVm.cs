using System.Collections.Generic;
using System.Linq;
using Application.Core.ViewModels;
using Domain.Administrativo.Entities;

namespace Application.Administrativo.ViewModels.Sindicantes
{
    public class SindicanteEditVm
    {
        public int Id { get; set; }
        public int SindicanteTipoId { get; set; }
        public string SindicanteTipo { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public int BancoId { get; set; }
        public int ContaTipo { get; set; }
        public string Agencia { get; set; }
        public string Conta { get; set; }
        public string Digito { get; set; }
        public decimal ValorPorKm { get; set; }
        public EnderecoVm Endereco { get; set; }
        public ICollection<HonorarioVm> Honorarios { get; set; }

        public SindicanteEditVm()
        {
            
        }

        public SindicanteEditVm(Sindicante sindicante)
        {
            Id = sindicante.Id;
            SindicanteTipoId = sindicante.SindicanteTipo.Id;
            SindicanteTipo = sindicante.SindicanteTipo.Nome;
            Nome = sindicante.DadosPessoaFisica.Nome;
            Cpf = sindicante.DadosPessoaFisica.Cpf;
            Rg = sindicante.DadosPessoaFisica.Rg;
            Telefone = sindicante.Telefone;
            Celular = sindicante.Celular;
            Email = sindicante.Email;
            ValorPorKm = sindicante.ValorPorKm;
            BancoId = sindicante.DadosBancarios.BancoId;
            ContaTipo = sindicante.DadosBancarios.ContaTipo;
            Agencia = sindicante.DadosBancarios.Agencia;
            Conta = sindicante.DadosBancarios.Conta;
            Digito = sindicante.DadosBancarios.Digito;
            Endereco = sindicante.Enderecos.Select(endereco => new EnderecoVm(endereco)).SingleOrDefault();
            Honorarios = sindicante.Honorarios.Select(x => new HonorarioVm(x)).ToList();
        }
    }
}
