using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Domain.Core.Contracts.Repositories.Base;
using Domain.Core.Entities;
using Infra.Data.Contexts;

namespace Infra.Data.Core.Repositories.Base
{
    public abstract class RepositoryBase<T>: IRepositoryBase<T> where T: EntityBase
    {
        protected EfContext Context;

        protected RepositoryBase(EfContext context)
        {
            Context = context;
        }

        public virtual void Add(T entity)
        {
            Context.Set<T>().Add(entity);
        }

        public virtual void Update(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified; 
        }

        public virtual void Delete(T entity)
        {
            entity.Desativar();
            Context.Entry(entity).State = EntityState.Modified;
        }

        public virtual IEnumerable<T>  GetAllActive()
        {
            return Context.Set<T>().Where(x => x.Ativo).ToList();
        }

        protected IQueryable<T> Get()
        {
            return Context.Set<T>();
        }

        public virtual void AddRange(IEnumerable<T> entities)
        {
            Context.Set<T>().AddRange(entities);
        }
    }
}
