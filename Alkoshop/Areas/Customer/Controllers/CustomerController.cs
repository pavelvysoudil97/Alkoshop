using Alkoshop.Database;
using Alkoshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            Alkoshop.Models.Customer customer = Session["User"] as Alkoshop.Models.Customer;
            TempData["oldAddressId"] = customer.Address.ID;
            ViewBag.Address = customer.Address;
            return View(customer);
        }
        [HttpPost]
        public ActionResult Update(Alkoshop.Models.Customer customer)
        {
            Address address = new Address();
            address.City = customer.Address.City;
            address.Street = customer.Address.Street;
            address.StreetNumber = customer.Address.StreetNumber;
            address.ZipCode = customer.Address.ZipCode;
            int addressId = DBGetData.createAddress(DBMain.GetConnection(), address);
            DBGetData.changeCustomerData(DBMain.GetConnection(),
                                        (Session["User"]as Alkoshop.Models.Customer).ID,
                                        customer.Name,
                                        customer.Surname,
                                        customer.Password,
                                        customer.Email,
                                        customer.PhoneNumber,
                                        addressId);
            TempData["message-success"] = "Vaše data byla úspěšně změněna";
            return RedirectToAction("Index", "Home");
        }
    }
}