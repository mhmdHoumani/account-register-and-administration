using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPTutorial.Models
{
    public class EmployeeRep : IEmployeeRep
    {
        private readonly List<Employee> _employeeList;
        public EmployeeRep()
        {
            _employeeList = new List<Employee>()
            {
                new Employee() {Id = 1, Name = "Maria", Department = Dept.HR, Email = "maria@hotmail.com"},
                new Employee() {Id = 2, Name = "John", Department = Dept.IT, Email = "john@hotmail.com" },
                new Employee(){Id = 3, Name = "Sam", Department = Dept.Payroll, Email = "sam@hotmail.com"}
            };
        }

        public Employee GetEmployee(int Id)
        {
            return _employeeList.FirstOrDefault(e => e.Id == Id);
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employeeList;
        }

        public Employee AddEmployee(Employee employee)
        {
            employee.Id = _employeeList.Max(e => e.Id) + 1;
            _employeeList.Add(employee);
            return employee;
        }

        public Employee UpdateEmployee(Employee employee)
        {
            Employee emp = _employeeList.FirstOrDefault(e => e.Id == employee.Id);
            if (emp != null)
            {
                emp.Name = employee.Name;
                emp.Email = employee.Email;
                emp.Department = employee.Department;
            }
            return emp;
        }

        public Employee DeleteEmployee(int Id)
        {
            Employee employee = _employeeList.FirstOrDefault(e => e.Id == Id);
            if (employee != null)
                _employeeList.Remove(employee);
            return employee;
        }
    }
}
