using System.Collections.Generic;
using Domain.Administrativo.Scopes;
using Domain.Core.Entities;

namespace Domain.Administrativo.Entities
{
    public class ServicoSeguradora: EntityId
    {
        public string Nome { get; private set; }
        public ICollection<EventoTipo> EventoTipos { get; private set; }
    
        protected ServicoSeguradora()
        {

        }


        public ServicoSeguradora(string nome, ICollection<EventoTipo> eventoTipos)
        {
            Nome = nome;
            EventoTipos = eventoTipos;
        }

        public void Alterar(string nome, ICollection<EventoTipo> eventoTipos)
        {
            if(!this.AlterIsValid(nome, eventoTipos))
                return;

            Nome = nome;
            EventoTipos = eventoTipos;
        }


        public override bool IsValid()
        {
            return this.CriarTipoServicoSeValido();
        }

        public bool AlterIsValid(string nome, ICollection<EventoTipo> eventoTipos)
        {
            return this.AlterarTipoServicoSeValido(nome, eventoTipos);
        }
    }
}