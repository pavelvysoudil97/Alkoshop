using Alkoshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alkoshop.Controllers
{
    public class AddressController : Controller
    {
        // GET: Address
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Add(Address address)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Create", "Customer", address);
            }
            return View("Create", address);
            
        }
    }
}