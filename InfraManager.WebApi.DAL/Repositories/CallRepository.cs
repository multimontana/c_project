namespace InfraManager.WebApi.DAL.Repositories
{
    using InfraManager.WebApi.DAL;
    using InfraManager.WebApi.DAL.DTOs;
    using InfraManager.WebApi.DAL.Repositories.Contracts;

    public class CallRepository : RepositoryBase<Call>, ICallRepository
    {
        public CallRepository(TmContext tmContext)
            : base(tmContext)
        {
        }
    }
}
