using System.Collections.Generic;
using Domain.Administrativo.Scopes;
using Domain.Core.Entities;

namespace Domain.Administrativo.Entities
{
    public class EventoTipo: EntityId
    {
        public string Nome { get; private set; }
        public ICollection<ServicoSeguradora> ServicosSeguradoras { get; private set; }
        
        
        #region Construtores
        protected EventoTipo()
        {

        }


        public EventoTipo(string nome)
        {
            Nome = nome;
        }
        #endregion

        public void Alterar(string nome)
        {
            Nome = nome;
        }


        public override bool IsValid()
        {
            return this.CriarEventoTipoScopeSeValido();
        }

       
    }
}