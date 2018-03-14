using System.Collections.Generic;
using System.Linq;
using Application.Administrativo.ViewModels.Eventos;
using Domain.Administrativo.Entities;
using Domain.Administrativo.ValueObjects;

namespace Application.Administrativo.ViewModels.Seguradoras
{
    public class SeguradoraVm
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Cnpj { get; set; }

        public string Telefone { get; set; }

        public string Contato { get; set; }

        public string Email { get; set; }

        public decimal ValorPorKm { get; set; }
        public string Observacoes { get; set; }
        public ICollection<EventoVm> Eventos { get; set; }


        public SeguradoraVm()
        {
            Eventos = new List<EventoVm>();
        }


        public SeguradoraVm(string nome, string cnpj, string telefone, string contato, string email, decimal valorPorKm, string observacoes, ICollection<EventoVm> eventos)
        {
            Nome = nome;
            Cnpj = cnpj;
            Telefone = telefone;
            Contato = contato;
            Email = email;
            ValorPorKm = valorPorKm;
            Observacoes = observacoes;
            Eventos = eventos;
        }

        public SeguradoraVm(int id, string nome, string cnpj, string telefone, string contato, string email, decimal valorPorKm, string observacoes, ICollection<EventoVm> eventos)
            :this(nome, cnpj, telefone, contato, email, valorPorKm, observacoes, eventos)
        {
            Id = id;
        }

        public SeguradoraVm(Seguradora seguradora) 
            :this()
        {
            Id = seguradora.Id;
            Nome = seguradora.DadosPessoaJuridica.RazaoSocial;
            Cnpj = seguradora.DadosPessoaJuridica.Cnpj;
            Telefone = seguradora.Telefone;
            Contato = seguradora.Contato;
            Email = seguradora.Email;
            ValorPorKm = seguradora.ValorPorKm;
            Observacoes = seguradora.Observacoes;

           
            foreach (var evento in seguradora.Eventos)
            {
                Eventos.Add(new EventoVm(evento));
            }
        }

        public Seguradora ToSeguradora(SeguradoraVm model, Diretorio diretorio)
        {
            var eventos = model.Eventos.Select(eventoViewModel => new Evento(eventoViewModel.ServicoSeguradoraId, eventoViewModel.EventoTipoId, eventoViewModel.FranquiaKm, eventoViewModel.Honorario)).ToList();

            return new Seguradora(
                    model.Nome, 
                    model.Cnpj,
                    model.Telefone,
                    model.Contato, model.Email,
                    model.ValorPorKm,
                    diretorio.Administrativo,
                    diretorio.Financeiro,
                    model.Observacoes,
                    eventos
                );
        }
    }
}
