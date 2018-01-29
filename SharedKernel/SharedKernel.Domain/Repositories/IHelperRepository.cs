namespace SharedKernel.Domain.Repositories
{
    public interface IHelperRepository
    {
        ISessionRepository OpenSession();
    }
}