using SharedKernel.Domain.Entities;

namespace SharedKernel.Test.Moks
{
    public class Foo : EntityBase, IAggregateRoot
    {
        public int Bar { get; set; }

        public override string ToString()
        {
            return $"Id: {Id} - Bar: {Bar}";
        }
    }
}