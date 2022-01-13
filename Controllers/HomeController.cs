using ASPTutorial.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using ASPTutorial.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace ASPTutorial.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IEmployeeRep _employeeRep;
        private readonly IWebHostEnvironment webHost;

        public HomeController(IEmployeeRep employeeRepository, IWebHostEnvironment webHost)
        {
            _employeeRep = employeeRepository;
            this.webHost = webHost;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            IEnumerable<Employee> model = _employeeRep.GetAllEmployees();
            return View(model);
        }

        public IActionResult Details(int? id)
        {
            if (_employeeRep.GetEmployee(id.Value) == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id);
            }
            EmployeeDetailsViewModel model = new()
            {
                Employee = _employeeRep.GetEmployee(id ?? 1),
                PageTitle = "Details Page"
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueName = ProcessUploadPhoto(model);
                if (("1_1_1").Equals(uniqueName))
                {
                    return View();
                }
                Employee employee = new()
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    PhotoPath = uniqueName
                };
                _employeeRep.AddEmployee(employee);
                return RedirectToAction("Details", new { id = employee.Id });
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            Employee employee = _employeeRep.GetEmployee(Id);
            EmployeeEditViewModel employeeEditViewModel = new()
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                ExistingPhotoPath = employee.PhotoPath,
                PageTitle = "Edit Page"
            };
            return View(employeeEditViewModel);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = _employeeRep.GetEmployee(model.Id);
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department;
                employee.PhotoPath = null;
                if (model.Image != null)
                {
                    if (model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(webHost.WebRootPath, "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    if (ProcessUploadPhoto(model).Equals("1_1_1"))
                    {
                        return View();
                    }
                    employee.PhotoPath = ProcessUploadPhoto(model);
                }
                _employeeRep.UpdateEmployee(employee);
                return RedirectToAction("index");
            }
            return View();
        }

        public IActionResult Delete(int Id)
        {
            Employee employee = _employeeRep.GetEmployee(Id);
            if (employee != null)
            {
                _employeeRep.DeleteEmployee(Id);
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private string ProcessUploadPhoto(EmployeeCreateViewModel model)
        {
            string uniqueName = null;
            if (model.Image != null)
            {
                string imgExt = Path.GetExtension(model.Image.FileName);
                if (imgExt.Equals(".jpg") || imgExt.Equals(".png"))
                {
                    string uploadFolder = Path.Combine(webHost.WebRootPath, "images");
                    uniqueName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                    string filePath = Path.Combine(uploadFolder, uniqueName);
                    using var fileStream = new FileStream(filePath, FileMode.Create);
                    model.Image.CopyTo(fileStream);
                    return uniqueName;
                }
                ModelState.AddModelError(model.Image.FileName, "Your image should be a .png or .jpg file.");
                return "1_1_1";
            }
            return uniqueName;
        }
    }
}
