using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace EmployeeManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHostingEnvironment _hostingEnviornment;

        public HomeController(IEmployeeRepository employeeRepository, IHostingEnvironment hostingEnviornment)
        {
            _employeeRepository = employeeRepository;
            _hostingEnviornment = hostingEnviornment;
        }

        public ViewResult Index()
        {
            var employees = _employeeRepository.GetAllEmployee();
            return View(employees);
        }

        public ViewResult Details(int? id)
        {
            id = id ?? 1;
            var employee = _employeeRepository.GetEmployee(id.Value);
            if (employee == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id.Value);
            }
            var homeDetailsViewModel = new HomeDetailsViewModel
            {
                Employee = employee,
                PageTitle = "Employee details"

            };
            return View(homeDetailsViewModel);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            var employee = _employeeRepository.GetEmployee(id);
            var employeeEditViewModel = new EmployeeEditViewModel
            {
                Id = employee.Id,
                Department = employee.Department,
                Email = employee.Email,
                Name = employee.Name,
                ExistingPhotoPath = employee.PhotoPath
            };
            return View(employeeEditViewModel);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = _employeeRepository.GetEmployee(model.Id);
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department;

                if (model.Photo != null)
                {
                    if (model.ExistingPhotoPath != null)
                    {
                        var fileToDelete = Path.Combine(_hostingEnviornment.WebRootPath, "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(fileToDelete);
                    }

                    var uniqueFileName = ProcessUploadedFile(model);
                }

                _employeeRepository.Update(employee);
                return RedirectToAction("Details", new { id = employee.Id });
            }

            return View();
        }

        private string ProcessUploadedFile(EmployeeCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.Photo != null)
            {
                var uploadsFolder = Path.Combine(_hostingEnviornment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }

        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.Photo != null)
                {
                    var uploadsFolder = Path.Combine(_hostingEnviornment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                var newEmployee = new Employee
                {
                    Name = model.Name,
                    Department = model.Department,
                    Email = model.Email,
                    PhotoPath = uniqueFileName
                };

                _employeeRepository.Add(newEmployee);
                return RedirectToAction("Details", new { id = newEmployee.Id });
            }

            return View();
        }
    }
}
