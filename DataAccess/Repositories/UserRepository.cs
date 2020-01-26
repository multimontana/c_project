using DataAccess.DTOs;
using DataAccess.Repositories.Contracts;

namespace DataAccess.Repositories
{
    class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(TmContext tmContext) : base(tmContext)
        {
        }
    }
}
