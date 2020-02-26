namespace InfraManager.WebApi.BLL.Repositories
{
    using InfraManager.WebApi.DAL;
    using InfraManager.WebApi.DAL.DTOs.Calls;

    using ICallRepository = InfraManager.WebApi.BLL.Repositories.Contracts.ICallRepository;

    public class CallRepository : RepositoryBase<Call>, ICallRepository
    {
        public CallRepository(TmContext tmContext)
            : base(tmContext)
        {
        }
    }
}
