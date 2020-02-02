namespace DataAccess.Repositories.Contracts
{
    public interface IRepositoryWrapper
    {
        public ICallRepository Call { get; }
    }
}