using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel.Domain.Entities;

namespace SharedKernel.EntityFramework.Maps
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> map)
        {
            map.HasKey(x => x.Id)
                .HasName("pk_user");
            map.Property(x => x.Id)
                .UseSqlServerIdentityColumn();

            map.Property(x => x.Name).HasMaxLength(100).IsRequired();
            map.Property(x => x.Login).HasMaxLength(100).IsRequired();
            map.HasIndex(x => x.Login).IsUnique();
            map.Property(x => x.Password).HasMaxLength(32).IsRequired();
        }
    }
}