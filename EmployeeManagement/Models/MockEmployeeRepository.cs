using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagement.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;

        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>
            {
                new Employee{Id = 1, Department = Dept.HR,Email ="Sipu@gmail.com",Name ="Gobind Mishra"},
                new Employee{Id = 2, Department = Dept.IT,Email ="Kashish@gmail.com",Name ="Kasish Kaushal"},
                new Employee{Id = 3, Department = Dept.IT,Email ="Jitendra@gmail.com",Name ="Jitendra Singh"}
            };
        }

        public Employee Add(Employee employee)
        {
            employee.Id = _employeeList.Max(e => e.Id) + 1;
            _employeeList.Add(employee);
            return employee;
        }

        public Employee Delete(int id)
        {
            var employeeToDelete = _employeeList.FirstOrDefault(e => e.Id == id);
            _employeeList?.Remove(employeeToDelete);
            return employeeToDelete;

        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _employeeList;
        }

        public Employee GetEmployee(int id)
        {
            return _employeeList.FirstOrDefault(e => e.Id == id);
        }

        public Employee Update(Employee employeeChanges)
        {
            var employeeToUpdate = _employeeList.FirstOrDefault(e => e.Id == employeeChanges.Id);
            if (employeeToUpdate != null)
            {
                employeeToUpdate.Name = employeeChanges.Name;
                employeeToUpdate.Email = employeeChanges.Email;
                employeeToUpdate.Department = employeeChanges.Department;
            }

            return employeeToUpdate;
        }
    }
}
