using System.Collections.Generic;
using System.Linq;
using Domain.Administrativo.Scopes;
using Domain.Core.Entities;
using Domain.Core.Entities.DadosCadastros;

namespace Domain.Administrativo.Entities
{
    public class Seguradora: EntityId
    {
        public int DadosPessoaJuridicaId { get; private set; }
        public string Telefone { get; private set; }
        public string Contato { get; private set; }
        public string Email { get; private set; }
        public decimal ValorPorKm { get; private set; }
        public string Observacoes { get; private set; }
        public string UrlAdministrativo { get; set; }
        public string UrlFinanceiro { get; set; }
        public ICollection<Evento> Eventos { get; private set; }
        public DadosPessoaJuridica DadosPessoaJuridica { get; private set; }


        #region Construtores
        protected Seguradora()
        {

        }
        
        public Seguradora(string nome, string cnpj, string telefone, string contato, string email, decimal valorPorKm, string urlAdm, string urlFinan, string observacoes, ICollection<Evento> eventos)
        {
            DadosPessoaJuridica = new DadosPessoaJuridica(nome, null, cnpj, null);
            Telefone = telefone;
            Contato = contato;
            Email = email;
            ValorPorKm = valorPorKm;
            UrlAdministrativo = urlAdm;
            UrlFinanceiro = urlFinan;
            Observacoes = observacoes;
            Eventos = eventos;
        } 
        #endregion

        public override bool IsValid()
        {
            return this.CriarSeguradoraEscopoSeValida();
        }

        public bool AlterIsValid(string nome, string cnpj, string telefone, string contato, string email, decimal valorPorKm, ICollection<Evento> eventos)
        {
            return this.AlterarSeguradoraSeEscopoValido(nome, cnpj, telefone, contato, email, valorPorKm, eventos);
        }

        public void AdicionarServicos(IList<Evento> eventos)
        {

            if (Eventos == null || !Eventos.Any())
            {
                Eventos = eventos;
            }
            else
            {
                Eventos = (ICollection<Evento>)Eventos.Union(eventos);
            }      
        }


        public void Alterar(string nome, string cnpj, string telefone, string contato, string email, decimal valorPorKm, string observacoes, ICollection<Evento> eventos)
        {
            if(!AlterIsValid(nome, cnpj,telefone,contato,email,valorPorKm,eventos))
                return;

            DadosPessoaJuridica.Alterar(nome, cnpj);
            Telefone = telefone;
            Contato = contato;
            Email = email;
            ValorPorKm = valorPorKm;
            Observacoes = observacoes;
            Eventos = eventos;
        }
    }
}
