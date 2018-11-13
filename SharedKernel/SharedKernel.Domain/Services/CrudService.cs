using System;
using SharedKernel.Domain.Entities;
using SharedKernel.Domain.Extensions;
using SharedKernel.Domain.Repositories;
using SharedKernel.Domain.Validation;

namespace SharedKernel.Domain.Services
{
    public class CrudService<T> : QueryService<T> where T : EntityBase, IAggregateRoot
    {
        protected Validator<T> Validator { get; set; }

        public CrudService(IHelperRepository helperRepository, Validator<T> validator) : base(helperRepository)
        {
            Validator = validator;
        }
        
        public virtual void Insert(T entity, string user = "sistema")
        {
            entity.AddInverseReferences(user);

            var result = Validator.Validate(entity, ValidationTypes.Insert);
            if (!result.IsValid)
                throw new ValidatorException(result.Errors);

            using (var session = HelperRepository.OpenSession())
            {
                var repo = session.GetRepository<T>();
                try
                {
                    session.StartTransaction();

                    entity.DataInclusao = DateTime.Now;
                    entity.UsuarioInclusao = user;
                    repo.Insert(entity);

                    session.CommitTransaction();
                }
                catch (Exception ex)
                {
                    session.RollBackTransaction();
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public virtual void Insert(string user, params T[] entities)
        {
            var result = Validator.Validate(entities, ValidationTypes.Insert);
            if (!result.IsValid)
                throw new ValidatorException(result.Errors);

            using (var session = HelperRepository.OpenSession())
            {
                var repo = session.GetRepository<T>();
                try
                {
                    session.StartTransaction();

                    foreach (var entity in entities)
                    {
                        entity.AddInverseReferences(user);

                        entity.DataInclusao = DateTime.Now;
                        entity.UsuarioInclusao = user;
                        repo.Insert(entity);
                    }

                    session.CommitTransaction();
                }
                catch (Exception ex)
                {
                    session.RollBackTransaction();
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public virtual void Update(T entity, string user = "sistema")
        {
            entity.AddInverseReferences(user);

            var result = Validator.Validate(entity, ValidationTypes.Update);
            if (!result.IsValid)
                throw new ValidatorException(result.Errors);

            using (var session = HelperRepository.OpenSession())
            {
                var repo = session.GetRepository<T>();
                try
                {
                    session.StartTransaction();

                    entity.DataAlteracao = DateTime.Now;
                    entity.UsuarioAlteracao = user;
                    repo.Update(entity);

                    session.CommitTransaction();
                }
                catch (Exception ex)
                {
                    session.RollBackTransaction();
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public virtual void Update(string user, params T[] entities)
        {
            var result = Validator.Validate(entities, ValidationTypes.Update);
            if (!result.IsValid)
                throw new ValidatorException(result.Errors);

            using (var session = HelperRepository.OpenSession())
            {
                var repo = session.GetRepository<T>();
                try
                {
                    session.StartTransaction();

                    foreach (var entity in entities)
                    {
                        entity.AddInverseReferences(user);
                        entity.DataAlteracao = DateTime.Now;
                        entity.UsuarioAlteracao = user;
                        repo.Update(entity);
                    }

                    session.CommitTransaction();
                }
                catch (Exception ex)
                {
                    session.RollBackTransaction();
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public virtual void Delete(T entity)
        {
            var result = Validator.Validate(entity, ValidationTypes.Delete);
            if (!result.IsValid)
                throw new ValidatorException(result.Errors);

            using (var session = HelperRepository.OpenSession())
            {
                var repo = session.GetRepository<T>();
                try
                {
                    session.StartTransaction();

                    repo.Delete(entity);

                    session.CommitTransaction();
                }
                catch (Exception ex)
                {
                    session.RollBackTransaction();
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}