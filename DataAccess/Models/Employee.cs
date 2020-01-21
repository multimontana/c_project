using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Employee
    {
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public string JobName { get; set; }
        public string Salary { get; set; }
        public int DepId { get; set; }

        public virtual Department Dep { get; set; }
    }
}
