using System.Collections.Generic;
using System.Linq;
using Application.Core.ViewModels;
using Domain.Administrativo.Entities;

namespace Application.Administrativo.ViewModels.Sindicantes
{
    public class SindicanteVm
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
        


        protected SindicanteVm()
        {
            
        }

        public SindicanteVm(int sindicanteTipoId, string nome, string cpf, string rg, string telefone, string celular, string email,EnderecoVm endereco, IList<HonorarioVm> honorarios, int bancoId, int contaTipo, string agencia, string conta, string digito, decimal valorPorKm)
        {
            SindicanteTipoId = sindicanteTipoId;
            Nome = nome;
            Cpf = cpf;
            Rg = rg;
            Telefone = telefone;
            Celular = celular;
            Email = email;
            Endereco = endereco;
            Honorarios = honorarios;
            BancoId = bancoId;
            ContaTipo = contaTipo;
            Agencia = agencia;
            Conta = conta;
            Digito = digito;
            ValorPorKm = valorPorKm;
        }

        public SindicanteVm(int id, int sindicanteTipoId, string nome, string cpf, string rg, string telefone, string celular, string email, EnderecoVm endereco, IList<HonorarioVm> honorarios, int bancoId, int contaTipo, string agencia, string conta, string digito, decimal valorPorKm)
           :this(sindicanteTipoId,  nome,  cpf, rg, telefone,  celular,  email,  endereco, honorarios,  bancoId,  contaTipo,  agencia,  conta, digito,  valorPorKm)
        {
            Id = id;
        }

        public SindicanteVm(Sindicante sindicante)
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
            
        }
      
        public List<Honorario> RetornHonorarios(SindicanteVm sindicanteVm)
        {
            return sindicanteVm.Honorarios.Select
                             (
                                    model =>new Honorario
                                    (
                                        model.ServicoSeguradoraId, 
                                        model.EventoTipoId, 
                                        model.ServicoSindicanteId, 
                                        sindicanteVm.Id,
                                        model.Valor
                                    )
                             ).ToList();
        }
    }
}
