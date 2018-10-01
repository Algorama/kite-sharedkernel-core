using Microsoft.EntityFrameworkCore;
using SharedKernel.Domain.Entities;
using SharedKernel.Domain.Repositories;

namespace SharedKernel.EntityFramework.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly DbContext _dbContext;

        public SessionRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryRepository<T> GetQueryRepository<T>() where T : EntityBase
        {
            return new QueryRepository<T>(_dbContext);
        }

        public IRepository<T> GetRepository<T>() where T : EntityBase, IAggregateRoot
        {
            return new Repository<T>(_dbContext);
        }

        public void StartTransaction()
        {
            _dbContext.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _dbContext.Database.CommitTransaction();
            _dbContext.SaveChanges();
        }

        public void RollBackTransaction()
        {
            _dbContext.Database.RollbackTransaction();
        }

        public void Dispose()
        {
        }
    }
}