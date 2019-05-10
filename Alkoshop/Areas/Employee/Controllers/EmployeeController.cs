using Alkoshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alkoshop.Controllers
{
    [Authorize(Users = "pecha@gmail.com")]
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(Address address)
        {
            TempData["addresscontainer"] = address;
            Employee employee = new Employee();
            employee.Address = address;
            return View();
        }

        [HttpPost]
        public ActionResult Add(Employee employee)
        {

            var addressContainer = TempData["addresscontainer"];

            if (employee.Address == null)
            {
                if(TempData["addresscontainer"] == null)
                {
                    return RedirectToAction("Create", "Address");
                }
                employee.Address = (Address)TempData["addresscontainer"];
            }
            if (ModelState.IsValid)
            {
                //Pripravene na create Employee 
                // DBGetData.createEmployeeWithAddress(customer, addressObject);
                TempData["message-success"] = "Employee was added successfully";

                return RedirectToAction("Index", "Home");
            }


            return View("Create", employee);
        }
    }
}