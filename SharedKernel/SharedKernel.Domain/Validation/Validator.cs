using SharedKernel.Domain.Entities;

namespace SharedKernel.Domain.Validation
{
    public class Validator<T> where T : EntityBase, IAggregateRoot
    {
        public ValidatorResult Validate(T entity, ValidationTypes type = ValidationTypes.Default)
        {
            var result = new ValidatorResult();

            Validate(result, entity, type);

            return result;
        }

        public ValidatorResult Validate(T[] entities, ValidationTypes type = ValidationTypes.Default)
        {
            var result = new ValidatorResult();

            foreach (var entity in entities)
                Validate(result, entity, type);

            return result;
        }

        private void Validate(ValidatorResult result, T entity, ValidationTypes type)
        {
            switch (type)
            {
                case ValidationTypes.Default:
                    DefaultValidations(result, entity);
                    break;
                case ValidationTypes.Insert:
                    InsertValidations(result, entity);
                    break;
                case ValidationTypes.Update:
                    UpdateValidations(result, entity);
                    break;
                case ValidationTypes.Delete:
                    DeleteValidations(result, entity);
                    break;
                default:
                    DefaultValidations(result, entity);
                    break;
            }
        }

        protected virtual void AnnotationsValidations(ValidatorResult result, T entity)
        {
            result.ValidateAnnotations(entity);
        }

        protected virtual void DefaultValidations(ValidatorResult result, T entity)
        {
            AnnotationsValidations(result, entity);

            if (entity == null)
                result.AddError("Entity can not be null!");
        }

        protected virtual void InsertValidations(ValidatorResult result, T entity)
        {
            DefaultValidations(result, entity);
        }

        protected virtual void UpdateValidations(ValidatorResult result, T entity)
        {
            DefaultValidations(result, entity);
        }

        protected virtual void DeleteValidations(ValidatorResult result, T entity)
        {
        }
    }
}
