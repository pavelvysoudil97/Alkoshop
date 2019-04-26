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

        
        public ActionResult Create(string cusoremp)
        {
            
            TempData["cusoremp"] = cusoremp;
            return View();
        }
        [HttpPost]
        public ActionResult Add(Address address, string cusoremp)
        {
            if (ModelState.IsValid)
            {
                if((string)TempData["cusoremp"] == "cus") { 
                    return RedirectToAction("Create", "Customer", address);
                    
                } else if ((string)TempData["cusoremp"] == "emp")
                {
                    return RedirectToAction("Create", "Employee", address);
                }
                
            }
            return View("Create");
            
        }
        public ActionResult CreateEmployee()
        {
            return View();
        }

        
    }
}