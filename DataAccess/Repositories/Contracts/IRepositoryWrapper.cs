namespace DataAccess.Repositories.Contracts
{
    public interface IRepositoryWrapper
    {
        IUserRepository User { get; }
    }
}