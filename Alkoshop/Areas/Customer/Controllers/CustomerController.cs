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

            if (customer.BirthDate.Year > DateTime.Today.Year - 18)
            {
                TempData["message-no-success"] = "Pro objednávání alkoholu musíte být starší než 18 let!";
                return RedirectToAction("Index", "Home");
            }
            Address oldAddress = TempData["oldAddress"] as DataAccess.Model.Address;
            AddressDao addressDao = new AddressDao();
           // addressDao.Create(customer.Address);
            
            Address address = new Address();
            address.City = customer.Address.City;
            address.Street = customer.Address.Street;
            address.StreetNumber = customer.Address.StreetNumber;
            address.ZipCode = customer.Address.ZipCode;
           
            if(addressDao.SearchByAllParams(address.City, address.StreetNumber, address.ZipCode, address.Street) != null)
            {
                customer.Address = addressDao.SearchByAllParams(address.City, address.StreetNumber, address.ZipCode, address.Street);
            }
            CustomerDao customerDao = new CustomerDao();
            customerDao.Update(customer);
            //int addressId = DBGetData.createAddress(DBMain.GetConnection(), address);
            
            TempData["message-success"] = "Vaše data byla úspěšně změněna";
            return RedirectToAction("Index", "Home");
        }
    }
}