﻿using System.Collections.Generic;

namespace EmployeeManagement.Models
{
    public class SQLEmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public SQLEmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public Employee Add(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return employee;
        }

        public Employee Delete(int id)
        {
            var employeeToBeDeleted = _context.Employees.Find(id);
            if (employeeToBeDeleted != null)
            {
                _context.Employees.Remove(employeeToBeDeleted);
                _context.SaveChanges();
            }

            return employeeToBeDeleted;
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _context.Employees;
        }

        public Employee GetEmployee(int id)
        {
            return _context.Employees.Find(id);
        }

        public Employee Update(Employee employeeChanges)
        {
            var employee = _context.Employees.Attach(employeeChanges);
            employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return employeeChanges;
        }
    }
}
