using System.Collections.Generic;
using DataAccess.DTOs;

namespace DataAccess.Initialization
{
    public static class DataInitialization
    {
        public static IEnumerable<Employee> GetEmployees()
        {
            IEnumerable<Employee> employees = new List<Employee>
            {
                new Employee
                {
                    EmployeeName = "KAY LING", JobName = "PRESIDENT", Salary = "6000", DepartmentId = 1
                },
                new Employee
                {
                    EmployeeName = "BLAZE", JobName = "MANAGER", Salary = "2750", DepartmentId = 2
                },
                new Employee
                {
                    EmployeeName = "CLARE", JobName = "MANAGER", Salary = "2950", DepartmentId = 1
                },
                new Employee
                {
                    EmployeeName = "JONAS", JobName = "MANAGER", Salary = "2900", DepartmentId = 3
                },
                new Employee
                {
                    EmployeeName = "SCARLET", JobName = "ANALYST ", Salary = "3100", DepartmentId = 3
                },
                new Employee
                {
                    EmployeeName = "FRANK", JobName = "SALESMAN", Salary = "1700", DepartmentId = 1
                },
                new Employee
                {
                    EmployeeName = "JOSE", JobName = "SALESMAN", Salary = "1000", DepartmentId = 2
                },
                new Employee
                {
                    EmployeeName = "DAVID", JobName = "PRESIDENT", Salary = "6000", DepartmentId = 2
                },
                new Employee
                {
                    EmployeeName = "SIA", JobName = "ANALYST", Salary = "5000", DepartmentId = 2
                },
                new Employee
                {
                    EmployeeName = "ROBERTO", JobName = "Programmer", Salary = "4000", DepartmentId = 2
                },
                new Employee
                {
                    EmployeeName = "ANGELINA", JobName = "SALESMAN", Salary = "3000", DepartmentId = 2
                },
                new Employee
                {
                    EmployeeName = "SABINA", JobName = "CLERK", Salary = "950", DepartmentId = 2
                },
                new Employee
                {
                    EmployeeName = "MICHEL", JobName = "MANAGER", Salary = "2500", DepartmentId = 2
                },
                new Employee
                {
                    EmployeeName = "SANDRA", JobName = "PRESIDENT", Salary = "6500", DepartmentId = 2
                },
                new Employee
                {
                    EmployeeName = "ADRIANA", JobName = "CLERK", Salary = "1000", DepartmentId = 2
                },
                new Employee
                {
                    EmployeeName = "CAMILLA", JobName = "ANALYST", Salary = "3000", DepartmentId = 2
                },
                new Employee
                {
                    EmployeeName = "Francesca", JobName = "MANAGER", Salary = "2000", DepartmentId = 2
                },
                new Employee
                {
                    EmployeeName = "Mario", JobName = "CLERK", Salary = "1000", DepartmentId = 2
                },
                new Employee
                {
                    EmployeeName = "Angelo", JobName = "ANALYST", Salary = "3000", DepartmentId = 2
                }
            };
            return employees;
        }

        public static IEnumerable<Department> GetDepartment()
        {
            IEnumerable<Department> departments = new List<Department>
            {
                new Department {DepartmentName = "FINANCE", Location = "SYDNEY"},
                new Department {DepartmentName = "AUDIT", Location = "MELBOURNE"},
                new Department {DepartmentName = "PRODUCTION", Location = "BRISBANE"}
            };

            return departments;
        }
    }
}
