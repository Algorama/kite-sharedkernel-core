using System;

namespace SharedKernel.Domain.Entities
{
    public abstract class Entity<TId> : IEquatable<Entity<TId>>, IEntity
    {
        public TId Id { get; set; }

        public bool Equals(Entity<TId> other)
        {
            if (other == null) return false;

            // Se o Id for igual ao valor default de TId (0 para inteiros ou longos), 
            // estamos comparando duas entidades não persistidas.
            // Nesse caso devolvemos a verificação de referencia em memória
            if (other.Id.Equals(default(TId)) && Id.Equals(default(TId)))
                return ReferenceEquals(this, other);

            // Verifica se as entidades são do mesmo tipo e tem o mesmo ID. Nesse caso são iguais
            return (GetType() == other.GetType() && Id.Equals(other.Id));
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Entity<TId>);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}