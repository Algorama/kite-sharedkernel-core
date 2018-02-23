using Microsoft.EntityFrameworkCore;
using SharedKernel.Domain.Repositories;

namespace SharedKernel.EntityFramework.Repositories
{
    public class HelperRepository : IHelperRepository
    {
        private static DbContext _context;

        public HelperRepository(DbContext context)
        {
            _context = context;
        }

        public ISessionRepository OpenSession()
        {
            if (_context != null) return new SessionRepository(_context);

            var builder = new DbContextOptionsBuilder<AppContext>();
            builder.UseInMemoryDatabase(databaseName: "db_test");
                
            // builder.UseSqlServer(
            //     "Server=(localdb)\\mssqllocaldb;Database=config;Trusted_Connection=True;MultipleActiveResultSets=true");
    
            _context = new AppContext(builder.Options);

            return new SessionRepository(_context);
        }
        
        public void CreateDb()
        {
            if(_context == null)
                OpenSession();

            if (_context == null) return;

            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }
    }
}
