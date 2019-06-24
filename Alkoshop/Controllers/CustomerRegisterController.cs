using DataAccess.Dao;
using DataAccess.Model;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alkoshop.Controllers
{
    public class CustomerRegisterController : Controller
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
            if (customer.Email.Contains("alkoshop.com"))
            {
                TempData["message-no-success"] = "Nemas pravo pridavat zamestnance";
                return RedirectToAction("Index", "Home");
            }
            if(customer.BirthDate.Year > DateTime.Today.Year - 18)
            {
                TempData["message-no-success"] = "Pro registraci musíte být starší než 18 let!";
                TempData["addresscontainer"] = customer.Address;
                return RedirectToAction("Create", customer);
            }

            customer.Address = (Address)TempData["addresscontainer"];

            if (ModelState.IsValid)
            {
                AddressDao addressDao = new AddressDao();
                addressDao.Create(customer.Address);

                CustomerDao customerDao = new CustomerDao();
                customerDao.Create(customer);
               
                TempData["message-success"] = "Registrace proběhla úspěšně prosím přihlaste se";
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Create", customer);
        }

    }
}