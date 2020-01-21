using DataAccess.DTOs;
using DataAccess.Repositories.Contracts;

namespace DataAccess.Repositories
{
    class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}