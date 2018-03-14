using System;

namespace Infra.Data.Core.UnitOfWork
{
    public interface IUnitOfWork: IDisposable
    {
        void BeginTran();
        void Save();
        void Save(string user);
        void SaveAsync();
        void Commit();
        void Rollback();
    }
}
