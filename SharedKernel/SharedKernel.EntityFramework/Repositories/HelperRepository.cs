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
        
        public void CreateDb()
        {
            if (_context == null) return;

            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }
    }
}
