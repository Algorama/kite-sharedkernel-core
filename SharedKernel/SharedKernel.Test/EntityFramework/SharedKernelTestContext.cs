using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SharedKernel.Test.EntityFramework
{
    public class SharedKernelTestContext : DbContext
    {
        public SharedKernelTestContext(DbContextOptions<SharedKernelTestContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new FooMap());

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}