using SharedKernel.Domain.Repositories;

namespace SharedKernel.EntityFramework.Mock
{
    public class MockHelperRepository : IHelperRepository
    {
        public ISessionRepository OpenSession()
        {
            return new MockSessionRepository();
        }
    }
}