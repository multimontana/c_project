namespace InfraManager.WebApi.BLL.Repositories
{
    using InfraManager.WebApi.BLL.Repositories.Contracts;
    using InfraManager.WebApi.DAL;

    public class RepositoryWrapper : IRepositoryWrapper
    {
        private static TmContext context;

        private readonly ICallRepository call;


        public RepositoryWrapper(TmContext tmContext, ICallRepository call)
        {
            context = tmContext;
            this.call = call;
        }

        public ICallRepository Call => this.call ?? new CallRepository(context);
    }
}