using System.Collections.Generic;
using System.Runtime.InteropServices;
using Domain.Administrativo.Scopes;
using Domain.Core.Entities;
using Domain.Core.Entities.DadosCadastros;

namespace Domain.Administrativo.Entities
{
    public class Sindicante: EntityId
    {
        public int SindicanteTipoId { get; private set; }
        public int DadosBancariosId { get; private set; }
        public int DadosPessoaFisicaId { get; set; }
        public string Telefone { get; private set; }
        public string Celular { get; private set; }
        public string Email { get; private set; }
        public decimal ValorPorKm { get; private set; }

        public SindicanteTipo SindicanteTipo { get; private set; }
        public ICollection<EnderecoSindicante> Enderecos { get; private set; }
        public ICollection<Honorario> Honorarios { get; private set; }
        public DadosBancarios DadosBancarios { get; private set; }
        public DadosPessoaFisica DadosPessoaFisica { get; private set; }
        public ICollection<Processo> Processos { get; private set; }


        protected Sindicante()
        {
            
        }

        public Sindicante(int sindicanteTipoId, string nome, string cpf, string rg, string telefone, string celular, string email, DadosBancarios dadosBancarios, ICollection<EnderecoSindicante> enderecos, ICollection<Honorario> honorarios,  [Optional]decimal valorPorKm)
        {
            SindicanteTipoId = sindicanteTipoId;
            DadosPessoaFisica = new DadosPessoaFisica(nome, cpf, rg);
            Telefone = telefone;
            Celular = celular;
            Email = email;
            DadosBancarios = dadosBancarios;
            Enderecos = enderecos;
            Honorarios = honorarios;
            ValorPorKm = valorPorKm;           
        }


        public virtual void Alterar(int sindicanteTipoId, string nome, string cpf, string rg, string telefone, string celular, string email, DadosBancarios dadosBancarios, ICollection<EnderecoSindicante> enderecos, ICollection<Honorario> honorarios, [Optional]decimal valorPorKm)
        {
            SindicanteTipoId = sindicanteTipoId;
            DadosPessoaFisica.Alterar(nome, cpf, rg);
            Telefone = telefone;
            Celular = celular;
            Email = email;
            DadosBancarios.Alterar(dadosBancarios);

            foreach (var enderecoSindicante in Enderecos)
            {
                foreach (var e in enderecos)
                {
                    enderecoSindicante.Alterar(e.Endereco, e.EnderecoTipoId, e.Numero, e.Complemento);
                }

            }
            Honorarios = honorarios;
            ValorPorKm = valorPorKm;
        }


        public override bool IsValid()
        {
            return this.CriarSindicanteSeEscopoValido();
        }

        public virtual void Excluir()
        {
            Ativo = false;
        }
    }
}
