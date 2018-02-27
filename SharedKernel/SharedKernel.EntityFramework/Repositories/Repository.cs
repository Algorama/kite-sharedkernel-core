using System;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Domain.Entities;
using SharedKernel.Domain.Repositories;

namespace SharedKernel.EntityFramework.Repositories
{
    public class Repository<T> : QueryRepository<T>, IRepository<T> where T : EntityBase, IAggregateRoot
    {
        public Repository(DbContext context) : base(context)
        {
        }

        public void Insert(T entity)
        {
            Context.Add(entity);
        }
        
        public void Update(T entity)
        {
            Context.Update(entity);
        }

        public void Save(T entity)
        {
            if(entity.Id == 0)
                Insert(entity);
            else
                Update(entity);
        }

        public void Delete(T entity)
        {
            Context.Remove(entity);
            Context.Entry(entity).State = EntityState.Deleted;
        }

        public void RunCommand(string hqlCommand)
        {
            throw new NotImplementedException();
        }
    }
}