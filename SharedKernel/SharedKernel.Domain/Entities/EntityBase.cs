using System;

namespace SharedKernel.Domain.Entities
{
    public class EntityBase : Entity<long>
    {
        public DateTime?    DataInclusao        { get; set; }
        public string       UsuarioInclusao     { get; set; }
        public DateTime?    DataAlteracao       { get; set; }
        public string       UsuarioAlteracao    { get; set; }

        public EntityBase Clone()
        {
            return (EntityBase)MemberwiseClone();
        }

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}