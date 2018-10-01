using SharedKernel.Domain.Repositories;

namespace SharedKernel.NHibernate.Repositories
{
    public class HelperRepository : IHelperRepository
    {
        public ISessionRepository OpenSession()
        {
            return new SessionRepository(NHibernateHelper.OpenSession());
        }
    }
}