using System;
using SharedKernel.Domain.Entities;
using SharedKernel.Domain.Extensions;
using SharedKernel.Domain.Repositories;
using SharedKernel.Domain.Validation;

namespace SharedKernel.Domain.Services
{
    public class CrudService<T> : QueryService<T>, ICrudService<T> where T : EntityBase, IAggregateRoot
    {
        public new IRepository<T> Repository { get; }

        protected Validator<T> Validator { get; set; }

        public CrudService(IRepository<T> repository, Validator<T> validator) : base(repository)
        {
            Repository = repository;
            Validator = validator;
        }
        
        public virtual void Insert(T entity, string user = "sistema")
        {
            entity.AddInverseReferences(user);

            var result = Validator.Validate(entity, ValidationTypes.Insert);
            if (!result.IsValid)
                throw new ValidatorException(result.Errors);

            try
            {
                entity.DataInclusao = DateTime.Now;
                entity.UsuarioInclusao = user;
                Repository.Insert(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual void Insert(string user, params T[] entities)
        {
            var result = Validator.Validate(entities, ValidationTypes.Insert);
            if (!result.IsValid)
                throw new ValidatorException(result.Errors);

            try
            {
                foreach (var entity in entities)
                {
                    entity.AddInverseReferences(user);

                    entity.DataInclusao = DateTime.Now;
                    entity.UsuarioInclusao = user;
                    Repository.Insert(entity);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual void Update(T entity, string user = "sistema")
        {
            entity.AddInverseReferences(user);

            var result = Validator.Validate(entity, ValidationTypes.Update);
            if (!result.IsValid)
                throw new ValidatorException(result.Errors);

            try
            {
                entity.DataAlteracao = DateTime.Now;
                entity.UsuarioAlteracao = user;
                Repository.Update(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual void Update(string user, params T[] entities)
        {
            var result = Validator.Validate(entities, ValidationTypes.Update);
            if (!result.IsValid)
                throw new ValidatorException(result.Errors);

            try
            {
                foreach (var entity in entities)
                {
                    entity.AddInverseReferences(user);
                    entity.DataAlteracao = DateTime.Now;
                    entity.UsuarioAlteracao = user;
                    Repository.Update(entity);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual void Delete(T entity)
        {
            var result = Validator.Validate(entity, ValidationTypes.Delete);
            if (!result.IsValid)
                throw new ValidatorException(result.Errors);

            try
            {
                Repository.Delete(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}