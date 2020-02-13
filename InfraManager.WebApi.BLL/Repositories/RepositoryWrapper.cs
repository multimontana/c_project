namespace InfraManager.WebApi.BLL.Repositories
{
    using InfraManager.WebApi.BLL.Repositories.Contracts;
    using InfraManager.WebApi.DAL;

    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly TmContext context;

        private readonly ICallRepository call;


        public RepositoryWrapper(TmContext tmContext, ICallRepository call)
        {
            this.context = tmContext;
            this.call = call;
        }

        public ICallRepository Call => this.call ?? new CallRepository(this.context);
    }
}