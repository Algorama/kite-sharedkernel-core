using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SharedKernel.Domain.Entities;

namespace SharedKernel.Domain.Repositories
{
    public interface IQueryRepository<T> where T : EntityBase
    {
        T Get(long id);        
        IQueryable<T> Query { get; }

        Task<T> GetAsync(long id);
        Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> @where);
    }
}