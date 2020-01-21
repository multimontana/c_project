using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Department
    {
        public Department()
        {
            Employee = new HashSet<Employee>();
        }

        public int DepId { get; set; }
        public string DepName { get; set; }
        public string DepLocation { get; set; }

        public virtual ICollection<Employee> Employee { get; set; }
    }
}
