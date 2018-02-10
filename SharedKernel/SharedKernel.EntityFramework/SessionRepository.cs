using Microsoft.EntityFrameworkCore;
using SharedKernel.Domain.Entities;
using SharedKernel.Domain.Repositories;

namespace SharedKernel.EntityFramework
{
    public class SessionRepository : ISessionRepository
    {
        // Segundo orientação da documentação da Microsoft. devemos utilizar apenas o método SaveChanges
        // e deixar que o próprio EF controle as transações. 
        // https://docs.microsoft.com/pt-br/ef/core/saving/transactions
        // Caso seja necessário declarar explicitamente as transações, essa classe poderá ser modificada para tal

        private readonly DatabaseContext _context;

        public SessionRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IQueryRepository<T> QueryRepository<T>() where T : EntityBase
        {
            return new QueryRepository<T>(_context);
        }

        public IRepository<T> Repository<T>() where T : EntityBase, IAggregateRoot
        {
            return new Repository<T>(_context);
        }

        public void StartTransaction()
        {
        }

        public void CommitTransaction()
        {
            _context.SaveChanges();
        }

        public void RollBackTransaction()
        {
        }

        public void Dispose()
        {
        }
    }
}