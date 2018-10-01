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
            return new SessionRepository(_context);
        }
    }
}
