using System.ComponentModel.DataAnnotations;

namespace DataAccess.DTOs
{
    public class Employee
    {
        public int EmployeeId => 0;

        [Required(ErrorMessage = "Please enter the name.")]
        public string EmployeeName { get; set; }

        [Required(ErrorMessage = "Please enter the job.")]
        public string JobName { get; set; }

        [Required(ErrorMessage = "Please enter the salary.")]
        public string Salary { get; set; }

        [Required(ErrorMessage = "Please enter the department.")]
        public int DepartmentId { get; set; }

        public Department Department => null;
    }
}
