using DataAccess.Repositories.Contracts;

namespace DataAccess.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly TmContext _context;

        private IUserRepository _user;


        public RepositoryWrapper(TmContext tmContext)
        {
            _context = tmContext;
        }

        public IUserRepository User => _user ??= new UserRepository(_context);
    }
}