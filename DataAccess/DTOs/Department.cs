using System.Collections.Generic;

namespace DataAccess.DTOs
{
    public class Department
    {
        public int DepartmentId => 0;

        public string DepartmentName { get; set; }
        public string Location { get; set; }
        public ICollection<Employee> Employees => null;
    }
}
