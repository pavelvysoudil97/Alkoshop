using DataAccess.Dao;
using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alkoshop.Areas.Employee.Controllers
{
    public class EmployeeRegistrationController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult Create(Address address)
        {
            DataAccess.Model.Employee employee = new DataAccess.Model.Employee();
            employee.Address = TempData["tempAddress"] as Address;
            TempData["addresscontainer"] = employee.Address;
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult Add(DataAccess.Model.Employee employee)
        {
            var addressContainer = TempData["addresscontainer"];

            if (employee.Address == null)
            {
                if(TempData["addresscontainer"] == null)
                {
                    return RedirectToAction("Create", "Address", new{area=""});
               }
                employee.Address = (Address)TempData["addresscontainer"];
            }
            if (ModelState.IsValid)
            {

                EmployeeDao employeeDao = new EmployeeDao();
                employeeDao.Create(employee);
                 TempData["message-success"] = "Employee was added successfully";

                 return RedirectToAction("Index", "Home");
            }


            return View("Create", employee);
        }
    }
}