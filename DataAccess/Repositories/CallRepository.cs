namespace DataAccess.Repositories
{
    using DataAccess.DTOs;
    using DataAccess.Repositories.Contracts;

    public class CallRepository : RepositoryBase<Call>, ICallRepository
    {
        public CallRepository(TmContext tmContext)
            : base(tmContext)
        {
        }
    }
}
