namespace SharedKernel.Domain.Repositories.Mock
{
    public class MockHelperRepository : IHelperRepository
    {
        public ISessionRepository OpenSession()
        {
            return new MockSessionRepository();
        }
    }
}