using System.Collections.Generic;
using Domain.Core.Entities;

namespace Domain.Core.Contracts.Repositories.Base
{
    public interface IRepositoryBase<T> where T: EntityBase
    {
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Update(T entity);
        void Delete(T entity);
        IEnumerable<T> GetAllActive();
    }
}
