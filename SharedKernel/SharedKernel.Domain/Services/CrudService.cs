using System;
using SharedKernel.Domain.Entities;
using SharedKernel.Domain.Extensions;
using SharedKernel.Domain.Repositories;
using SharedKernel.Domain.Validation;

namespace SharedKernel.Domain.Services
{
    public class CrudService<T> : QueryService<T>, ICrudService<T> where T : EntityBase, IAggregateRoot
    {
        protected Validator<T> Validator { get; set; }

        public CrudService(IHelperRepository helper, Validator<T> validator) : base(helper)
        {
            Validator = validator;
        }
        
        public virtual void Insert(T entity, string user = "sistema")
        {
            entity.AddInverseReferences(user);

            var result = Validator.Validate(entity, ValidationTypes.Insert);
            if (!result.IsValid)
                throw new ValidatorException(result.Errors);

            using (var session = Helper.OpenSession())
            {
                var repo = session.Repository<T>();
                try
                {
                    session.StartTransaction();
                    entity.DataInclusao = DateTime.Now;
                    entity.UsuarioInclusao = user;
                    repo.Insert(entity);
                    session.CommitTransaction();
                }
                catch (Exception)
                {
                    session.RollBackTransaction();
                    throw;
                }
            }
        }

        public virtual void Insert(string user, params T[] entities)
        {
            var result = Validator.Validate(entities, ValidationTypes.Insert);
            if (!result.IsValid)
                throw new ValidatorException(result.Errors);

            using (var session = Helper.OpenSession())
            {
                var repo = session.Repository<T>();
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
                catch (Exception)
                {
                    session.RollBackTransaction();
                    throw;
                }
            }
        }

        public virtual void Update(T entity, string user = "sistema")
        {
            entity.AddInverseReferences(user);

            var result = Validator.Validate(entity, ValidationTypes.Update);
            if (!result.IsValid)
                throw new ValidatorException(result.Errors);

            using (var session = Helper.OpenSession())
            {
                var repo = session.Repository<T>();
                try
                {
                    session.StartTransaction();
                    entity.DataAlteracao = DateTime.Now;
                    entity.UsuarioAlteracao = user;
                    repo.Update(entity);
                    session.CommitTransaction();
                }
                catch (Exception)
                {
                    session.RollBackTransaction();
                    throw;
                }
            }
        }

        public virtual void Update(string user, params T[] entities)
        {
            var result = Validator.Validate(entities, ValidationTypes.Update);
            if (!result.IsValid)
                throw new ValidatorException(result.Errors);

            using (var session = Helper.OpenSession())
            {
                var repo = session.Repository<T>();
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
                catch (Exception)
                {
                    session.RollBackTransaction();
                    throw;
                }
            }
        }

        public virtual void Delete(long id)
        {
            var entity = base.Get(id);

            var result = Validator.Validate(entity, ValidationTypes.Delete);
            if (!result.IsValid)
                throw new ValidatorException(result.Errors);

            using (var session = Helper.OpenSession())
            {
                var repo = session.Repository<T>();
                try
                {
                    session.StartTransaction();
                    repo.Delete(entity);
                    session.CommitTransaction();
                }
                catch (Exception)
                {
                    session.RollBackTransaction();
                    throw;
                }
            }
        }
                
        public virtual void RunCommand(string hqlCommand)
        {
            using (var sessao = Helper.OpenSession())
            {
                var repo = sessao.Repository<T>();
                try
                {
                    sessao.StartTransaction();
                    repo.RunCommand(hqlCommand);
                    sessao.CommitTransaction();
                }
                catch (Exception)
                {
                    sessao.RollBackTransaction();
                    throw;
                }
            }
        }
    }
}