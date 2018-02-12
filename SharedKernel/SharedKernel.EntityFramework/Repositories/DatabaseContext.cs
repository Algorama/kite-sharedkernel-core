using Microsoft.EntityFrameworkCore;
using SharedKernel.Domain.Repositories;

namespace SharedKernel.EntityFramework.Repositories
{
    public class DatabaseContext : DbContext, IHelperRepository
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }
       
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        ISessionRepository IHelperRepository.OpenSession()
        {
            return new SessionRepository(this);
        }
    }
}