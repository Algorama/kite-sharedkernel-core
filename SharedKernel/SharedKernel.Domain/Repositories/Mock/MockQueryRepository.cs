using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SharedKernel.Domain.Entities;

namespace SharedKernel.Domain.Repositories.Mock
{
    public class MockQueryRepository<T> : IQueryRepository<T> where T : EntityBase
    {
        public static List<T> Data { get; set; }

        public MockQueryRepository()
        {
            Data = new List<T>();
        }

        public T Get(long id)
        {
            return (T)Data.FirstOrDefault(x => x.Id == id)?.Clone();
        }

        public IQueryable<T> Query => Data.AsQueryable();

        public Task<T> GetAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> @where)
        {
            throw new NotImplementedException();
        }
    }
}