using Microsoft.EntityFrameworkCore;
using SharedKernel.Domain.Repositories;

namespace SharedKernel.EntityFramework.Repositories
{
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions<AppContext> options)
            : base(options)
        {
        }
      
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //modelBuilder.ApplyConfiguration(new UsuarioMap());
            //modelBuilder.ApplyConfiguration(new AplicacaoMap());
        }
    }
}