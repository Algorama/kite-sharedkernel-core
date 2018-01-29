using System;
using SharedKernel.Domain.Entities;

namespace SharedKernel.Domain.Repositories
{
    public interface ISessionRepository : IDisposable
    {
        IQueryRepository<T> QueryRepository<T>() where T : EntityBase;
        IRepository<T> Repository<T>() where T : EntityBase, IAggregateRoot;

        void StartTransaction();
        void CommitTransaction();
        void RollBackTransaction();
    }
}