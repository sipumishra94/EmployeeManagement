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
                new Employee{Id = 1, Department = "HR",Email ="Sipu@gmail.com",Name ="Gobind Mishra"},
                new Employee{Id = 2, Department = "Engineering",Email ="Kashish@gmail.com",Name ="Kasish Kaushal"},
                new Employee{Id = 3, Department = "Legal",Email ="Jitendra@gmail.com",Name ="Jitendra Singh"}
            };
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _employeeList;
        }

        public Employee GetEmployee(int id)
        {
            return _employeeList.FirstOrDefault(e => e.Id == id);
        }
    }
}
