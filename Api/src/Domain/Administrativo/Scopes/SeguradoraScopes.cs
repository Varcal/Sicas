using System.Collections.Generic;
using Domain.Administrativo.Entities;
using SharedKernel.Validations;

namespace Domain.Administrativo.Scopes
{
    public static class SeguradoraScopes
    {
        public static bool CriarSeguradoraEscopoSeValida(this Seguradora seguradora)
        {
            return AssertionConcern.IsValid(
                    AssertionConcern.AssertNotEmpty(seguradora.DadosPessoaJuridica.RazaoSocial, "RazaoSocial é obrigatório"),
                    AssertionConcern.AssertNotEmpty(seguradora.DadosPessoaJuridica.Cnpj, "CNPJ é obrigatório"),
                    AssertionConcern.AssertCnpjIsValid(seguradora.DadosPessoaJuridica.Cnpj, "CNPJ inválido"),
                    AssertionConcern.AssertNotEmpty(seguradora.Telefone, "Telefone é obrigatório"),
                    AssertionConcern.AssertPhoneIsValid(seguradora.Telefone, "Telefone inválido"),
                    AssertionConcern.AssertNotEmpty(seguradora.Contato, "Contato é obrigatório"),
                    AssertionConcern.AssertNameIsValid(seguradora.Contato, "Contato não pode conter números"),
                    AssertionConcern.AssertNotEmpty(seguradora.Email, "E-mail é obrigatório"),
                    AssertionConcern.AssertEmailIsValid(seguradora.Email, "E-mail inválido"),
                    AssertionConcern.AssertIsGreaterThan(seguradora.ValorPorKm, 0, "Valor por Km é obrigatório"),
                    AssertionConcern.AssertIsGreaterThan(seguradora.Eventos.Count, 0, "Valor por Km é obrigatório")
                );
        }


        public static bool AlterarSeguradoraSeEscopoValido(this Seguradora seguradora, string nome, string cnpj, string telefone, string contato, string email, decimal valorPorKm, ICollection<Evento> eventos)
        {
            return AssertionConcern.IsValid(
                    AssertionConcern.AssertNotEmpty(nome, "Nome é obrigatório"),
                    AssertionConcern.AssertNotEmpty(cnpj, "CNPJ é obrigatório"),
                    AssertionConcern.AssertCnpjIsValid(cnpj, "CNPJ inválido"),
                    AssertionConcern.AssertNotEmpty(telefone, "Telefone é obrigatório"),
                    AssertionConcern.AssertPhoneIsValid(telefone, "Telefone inválido"),
                    AssertionConcern.AssertNotEmpty(contato, "Contato é obrigatório"),
                    AssertionConcern.AssertNameIsValid(contato, "Contato não pode conter números"),
                    AssertionConcern.AssertNotEmpty(email, "E-mail é obrigatório"),
                    AssertionConcern.AssertEmailIsValid(email, "E-mail inválido"),
                    AssertionConcern.AssertIsGreaterThan(valorPorKm, 0, "Valor por Km é obrigatório"),
                    AssertionConcern.AssertIsGreaterThan(eventos.Count, 0, "Valor por Km é obrigatório")
                );
        }
    }
}
