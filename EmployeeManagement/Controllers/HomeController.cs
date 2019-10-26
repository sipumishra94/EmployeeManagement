using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public HomeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public ViewResult Index()
        {
            var employees = _employeeRepository.GetAllEmployee();
            return View(employees);
        }

        public ViewResult Details(int? id)
        {
            id = id ?? 1;
            var homeDetailsViewModel = new HomeDetailsViewModel
            {
                Employee = _employeeRepository.GetEmployee(id.Value),
                PageTitle = "Employee details"

            };
            return View(homeDetailsViewModel);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if(ModelState.IsValid)
            {
                var employeeId = _employeeRepository.Add(employee).Id;
                return RedirectToAction("Details", new { id = employeeId });
            }

            return View();
        }
    }
}
