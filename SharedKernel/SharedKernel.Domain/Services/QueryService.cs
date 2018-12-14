using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using SharedKernel.Domain.Dtos;
using SharedKernel.Domain.Entities;
using SharedKernel.Domain.Repositories;
using SharedKernel.Domain.ValueObjects;

namespace SharedKernel.Domain.Services
{
    public class QueryService<T> where T : EntityBase
    {
        public IHelperRepository HelperRepository { get; }

        public QueryService(IHelperRepository helperRepository)
        {
            HelperRepository = helperRepository;
        }

        public virtual T Get(long id)
        {
            using (var session = HelperRepository.OpenSession())
            {
                var repo = session.GetQueryRepository<T>();
                var entity = repo.Get(id);
                return entity;
            }
        }

        public virtual IList<T> GetAll()
        {
            using (var session = HelperRepository.OpenSession())
            {
                var repo = session.GetQueryRepository<T>();
                var entities = repo.Query.ToList();
                return entities;
            }
        }

        public virtual IList<T> GetAll(Expression<Func<T, bool>> @where)
        {
            using (var session = HelperRepository.OpenSession())
            {
                var repo = session.GetQueryRepository<T>();
                var entities = repo.Query.Where(@where).ToList();
                return entities;
            }
        }

        public virtual PageResult<T> GetPaged(int page, PageSize size)
        {
            if (page < 1)
                throw new ValidationException("Invalid Page Number! First Page Number is 1.");

            var result = new PageResult<T> { Page = page };

            using (var session = HelperRepository.OpenSession())
            {
                var repo = session.GetQueryRepository<T>();

                var query = repo.Query;

                result.TotalItems = query.Count();
                result.PageSize = (int)size;
                result.TotalPages =
                    (int)Math.Ceiling((double)result.TotalItems / (int)size);

                result.Data = query
                    .Skip((int)size * --page)
                    .Take((int)size)
                    .ToList();
            }

            return result;
        }
    }
}