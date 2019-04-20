using Alkoshop.Database;
using Alkoshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alkoshop.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(Address address)
        {
            TempData["addresscontainer"] = address;
            return View();
        }

        [HttpPost]
        public ActionResult Add(Customer customer)
        {
            if (ModelState.IsValid)
            {

                var addressObject = TempData["addresscontainer"];

               // DBGetData.createCustomerWithAddress(customer, addressObject);

                return RedirectToAction("Index", "Home");
            }

            return View("Create", customer);
        }
    }
}