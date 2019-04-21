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
            Customer customer = new Customer();
            customer.Address = address;
            return View();
        }

        [HttpPost]
        public ActionResult Add(Customer customer)
        {

            var addressContainer = TempData["addresscontainer"];

            if (ModelState.IsValid)
            {
                
               // DBGetData.createCustomerWithAddress(customer, addressObject);

                return RedirectToAction("Index", "Home");
            }
            if(customer.Address == null)
            {
                customer.Address = (Address)TempData["addresscontainer"];
            }
            
            return View("Create", customer);
        }
    }
}