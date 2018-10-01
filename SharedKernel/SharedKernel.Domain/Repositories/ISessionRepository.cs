using System;
using SharedKernel.Domain.Entities;

namespace SharedKernel.Domain.Repositories
{
    public interface ISessionRepository : IDisposable
    {
        IQueryRepository<T> GetQueryRepository<T>() where T : EntityBase;
        IRepository<T> GetRepository<T>() where T : EntityBase, IAggregateRoot;

        void StartTransaction();
        void CommitTransaction();
        void RollBackTransaction();
    }
}