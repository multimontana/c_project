namespace InfraManager.WebApi.DAL.Repositories.Contracts
{
    public interface IRepositoryWrapper
    {
        public ICallRepository Call { get; }
    }
}