
using DataAccess.Model;
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
            
            TempData["cusoremporOr"] = cusoremp;
            if(cusoremp == "emp" && !User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index","Login");
            } 
            return View();
        }

        [HttpPost]
        public ActionResult Add(Address tempAddress)
        {
            if (ModelState.IsValid)
            {
                if((string)TempData["cusoremporOr"] == "cus") { 

                    return RedirectToAction("Create", "CustomerRegister", tempAddress );
                    
                } else if ((string)TempData["cusoremporOr"] == "emp" && User.Identity.IsAuthenticated)
                {
                    TempData["tempAddress"] = tempAddress;
                    return RedirectToAction("Create", "EmployeeRegistration", new { area = "Employee"  });

                } else if((string)TempData["cusoremporOr"] == "order")
                {
                    TempData["addressOrder"] = tempAddress;
                    return RedirectToAction("Create", "Order", new { area = "Customer", newAddress = true});
                }
                
            }
            return View("Create");
            
        }
        public ActionResult CreateForOrder()
        {
            return View();
        }

        
    }
}