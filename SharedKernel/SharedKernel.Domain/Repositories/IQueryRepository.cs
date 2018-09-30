using System.Linq;
using SharedKernel.Domain.Entities;

namespace SharedKernel.Domain.Repositories
{
    public interface IQueryRepository<T> where T : IEntity
    {
        T Get(long id);        
        IQueryable<T> Query { get; }
    }
}