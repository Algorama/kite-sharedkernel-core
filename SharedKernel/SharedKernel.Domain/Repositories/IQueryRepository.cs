using System;
using System.Linq;
using System.Linq.Expressions;
using SharedKernel.Domain.Entities;

namespace SharedKernel.Domain.Repositories
{
    public interface IQueryRepository<T> where T : IEntity
    {
        T Get(long id);        
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll();
    }
}