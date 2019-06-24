using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess.Dao;
using DataAccess.Model;

namespace Alkoshop.Areas.Customer.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer/Customer
        public ActionResult Index()
        {
           return View();
        }
        public ActionResult Edit()
        {
           DataAccess.Model.Customer customer = Session["User"] as DataAccess.Model.Customer;
            TempData["oldAddress"] = customer.Address;
            ViewBag.Address = customer.Address;
            return View(customer);
        }

        [HttpPost]
        public ActionResult Update(DataAccess.Model.Customer customer)
        {
            CustomerDao customerDao = new CustomerDao();
            AddressDao addressDao = new AddressDao();

            if (customer.BirthDate.Year > DateTime.Today.Year - 18)
            {
                TempData["message-no-success"] = "Pro objednávání alkoholu musíte být starší než 18 let!";
                return RedirectToAction("Index", "Home");
            }
            
            customerDao.Update(customer);
            addressDao.Update(customer.Address);

            TempData["message-success"] = "Vaše data byla úspěšně změněna";
            return RedirectToAction("Index", "Home");
        }
    }
}