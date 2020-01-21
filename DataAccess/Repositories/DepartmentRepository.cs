using DataAccess.DTOs;
using DataAccess.Repositories.Contracts;

namespace DataAccess.Repositories
{
    class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository
    {
        public DepartmentRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}