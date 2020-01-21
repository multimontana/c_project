namespace DataAccess.Repositories.Contracts
{
    public interface IRepositoryWrapper
    {
        IEmployeeRepository Employee { get; }
        IDepartmentRepository Department { get; }
    }
}