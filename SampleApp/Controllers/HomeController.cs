using Microsoft.AspNetCore.Mvc;
using SampleApp.Data;
using SampleApp.Models;
using System.Diagnostics;

namespace SampleApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmployeeRepo employeeRepo;
        public HomeController(ILogger<HomeController> logger, IEmployeeRepo employeeRepo)
        {
            _logger = logger;
            this.employeeRepo = employeeRepo;
        }

        public async Task< IActionResult> Index()
        {
            List<Employee> model= await employeeRepo.GetAllEmployee();
            return View(model);
        }

        [HttpGet]
        public async Task< IActionResult> AddEmployee()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var EmpStatus = employee.Emp_Status;
                if (EmpStatus != "true")
                {
                   employee.Emp_Status = "InActive";
                }
                else
                {
                    employee.Emp_Status = "Active";
                }
               

                 await employeeRepo.AddEmployee(employee);
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<IActionResult> EmployeeDetails(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var emp = await employeeRepo.GetEmployee(id);
            if (emp == null)
            {
                return BadRequest();
            }
            return View(emp);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var delId = await employeeRepo.GetEmployee(id);
            if (delId != null)
            {
                await employeeRepo.DeleteEmployee(delId.Emp_Id);
               
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<IActionResult> Update(int id)
        {
            var emp = await employeeRepo.GetEmployee(id);
            var EmpStatus = emp.Emp_Status;
            if (EmpStatus != "Active")
            {
                emp.Emp_Status = "false";
            }
            else
            {
                emp.Emp_Status = "true";
            }
            ViewBag.PageTitle = "Update Employee";
            return View(emp);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var EmpStatus = employee.Emp_Status;
                if (EmpStatus != "true")
                {
                    employee.Emp_Status = "InActive";
                }
                else
                {
                    employee.Emp_Status = "Active";
                }


                await employeeRepo.UpdateEmployee(employee);
                return RedirectToAction("Index");
            }
            return View("Not Updated");
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
    }
}