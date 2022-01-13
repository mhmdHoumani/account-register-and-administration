using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPTutorial.Models
{
    public class SQLEmployeeRep : IEmployeeRep
    {
        public AppDBContext Db { get; }
        public SQLEmployeeRep(AppDBContext db)
        {
            Db = db;
        }


        public Employee AddEmployee(Employee employee)
        {
            Db.Employees.Add(employee);
            Db.SaveChanges();
            return employee;
        }

        public Employee DeleteEmployee(int Id)
        {
            Employee employee = Db.Employees.Find(Id);
            if (employee != null)
            {
                Db.Employees.Remove(employee);
                Db.SaveChanges();
            }
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return Db.Employees;
        }

        public Employee GetEmployee(int Id)
        {
            return Db.Employees.Find(Id);
        }

        public Employee UpdateEmployee(Employee employee)
        {
            var emp = Db.Employees.Attach(employee);
            emp.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            Db.SaveChanges();
            return employee;
        }
    }
}
