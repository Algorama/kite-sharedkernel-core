using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            return Data.FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<T> GetAll()
        {
            return Data.AsQueryable();
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return Data.AsQueryable().Where(predicate);
        }
    }
}