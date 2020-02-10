namespace InfraManager.WebApi.DAL.Repositories
{
    using InfraManager.WebApi.DAL;
    using InfraManager.WebApi.DAL.Repositories.Contracts;

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