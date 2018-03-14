using System.Collections.Generic;
using System.Runtime.InteropServices;
using Domain.Account.Entities;
using Domain.Administrativo.Scopes;

namespace Domain.Administrativo.Entities
{
    public class SindicanteInterno : Sindicante
    {

        public int UsuarioId { get; private set; }

        public Usuario Usuario { get; private set; }


        protected SindicanteInterno()
        {
            
        }

        public SindicanteInterno(Usuario usuario, int sindicanteTipoId, string nome, string cpf, string rg, string telefone, string celular, string email, DadosBancarios dadosBancarios, ICollection<EnderecoSindicante> enderecos, ICollection<Honorario> honorarios, [Optional]decimal valorPorKm) 
            : base(sindicanteTipoId, nome, cpf, rg, telefone, celular, email, dadosBancarios, enderecos, honorarios, valorPorKm)
        {
            Usuario = usuario;
        }

        public override void Alterar(int sindicanteTipoId, string nome, string cpf, string rg, string telefone, string celular, string email,
            DadosBancarios dadosBancarios, ICollection<EnderecoSindicante> enderecos, ICollection<Honorario> honorarios, decimal valorPorKm = 0)
        {
            if (Usuario != null)
            {
                Usuario.Alterar(nome, Usuario.Login, Usuario.Perfis);
            }

            
            base.Alterar(sindicanteTipoId, nome, cpf, rg, telefone, celular, email, dadosBancarios, enderecos, honorarios, valorPorKm);

        }


        public void Alterar(Usuario usuario, int sindicanteTipoId, string nome, string cpf, string rg, string telefone, string celular,
            string email, DadosBancarios dadosBancarios, ICollection<EnderecoSindicante> enderecos, ICollection<Honorario> honorarios, [Optional] decimal valorPorKm)
        {
            if (Usuario == null)
            {
                Usuario = usuario;
            }
           
            base.Alterar(sindicanteTipoId, nome, cpf, rg, telefone, celular, email, dadosBancarios, enderecos, honorarios, valorPorKm);
        }


        public override bool IsValid()
        {            
            return base.IsValid() && this.CriarSindicanteSeEscopoValido();
        }

        public override void Excluir()
        {
            Usuario.Desativar();
        }
    }
}
