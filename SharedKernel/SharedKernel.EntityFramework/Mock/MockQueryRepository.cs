using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SharedKernel.Domain.Dtos;
using SharedKernel.Domain.Entities;
using SharedKernel.Domain.Repositories;

namespace SharedKernel.EntityFramework.Mock
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

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate, params string[] include)
        {
            return Data.AsQueryable().Where(predicate);
        }

        public ODataResult<T> GetOData(List<KeyValuePair<string, string>> queryStringParts)
        {
            var result = new ODataResult<T>
            {
                d =
                {
                    __count = Data.Count,
                    results = Data
                }
            };
            return result;
        }
    }
}