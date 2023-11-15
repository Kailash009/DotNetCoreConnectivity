using CoreConnectivity.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreConnectivity.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly DbConnect _db;
        public EmployeeController(DbConnect db)
        {
            _db = db;  // Dependency Injection.
        }
        public IActionResult Index()
        {
            var employee = _db.GetEmployees();
            return View(employee);
        }

        [HttpGet]
        public IActionResult Create()
        {  
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee empObj)
        {
            bool b=_db.addEmployee(empObj);
            if(b==true)
            {
                TempData["insert"] = "<script>alert('Employee Added SuccessFully!');</script>";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["insert"] = "<script>alert('Employee Failed!');</script>";
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var employee = _db.GetEmployee(id);
            return View(employee);
        }

        [HttpPost]
        public IActionResult Edit(Employee empObj)
        {
            bool b = _db.updateEmployee(empObj);
            if (b == true)
            {
                TempData["update"] = "<script>alert('Employee Updated SuccessFully!');</script>";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["update"] = "<script>alert('Employee Failed!');</script>";
            }
            return View();
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var employee = _db.GetEmployee(id);
            return View(employee);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            bool b = _db.deleteEmployee(id);
            if (b == true)
            {
                TempData["delete"] = "<script>alert('Employee Deleted SuccessFully!');</script>";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["delete"] = "<script>alert('Employee Failed!');</script>";
            }
            return View();
        }
    }
}
