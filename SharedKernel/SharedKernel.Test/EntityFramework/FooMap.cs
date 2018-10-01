using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel.Test.Moks;

namespace SharedKernel.Test.EntityFramework
{
    public class FooMap : IEntityTypeConfiguration<Foo>
    {
        public void Configure(EntityTypeBuilder<Foo> map)
        {
            map.HasKey(x => x.Id)
                .HasName("pk_foo");
            map.Property(x => x.Id)
                .UseSqlServerIdentityColumn();

            map.Property(x => x.Bar);
        }
    }
}