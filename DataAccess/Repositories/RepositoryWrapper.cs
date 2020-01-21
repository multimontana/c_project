using DataAccess.Repositories.Contracts;

namespace DataAccess.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly AppDbContext _context;
        private IEmployeeRepository _employee;
        private IDepartmentRepository _department;

        public RepositoryWrapper(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public IEmployeeRepository Employee => _employee ??= new EmployeeRepository(_context);
        public IDepartmentRepository Department => _department ??= new DepartmentRepository(_context);
    }
}