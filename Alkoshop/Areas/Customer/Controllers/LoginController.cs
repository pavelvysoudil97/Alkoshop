using Alkoshop.Database;
using Alkoshop.Models;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Alkoshop.Areas.Customer.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
            
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            OracleConnection conn = DBMain.GetConnection();
            if (email.Contains("@alkoshop.com"))
            {
                Alkoshop.Models.Employee employee = DBGetData.getEmployee(conn, email, password);
                if (employee != null)
                {
                    Session["User"] = employee;
                    Session["UserRole"] = "Employee";
                }
            }
            else
            {
                Alkoshop.Models.Customer customer = DBGetData.getCustomer(conn, email, password);
                if(customer!= null)
                {
                    Session["User"] = customer;
                    Session["UserRole"] = "Customer";
                }
            }

            if(Membership.ValidateUser(email, password))
            {
                FormsAuthentication.SetAuthCookie(email, false);

                return RedirectToAction("Index", "Home", new { area = Session["UserRole"] });
            }


            TempData["login-error"] = "Login nebo heslo není správné";
            return RedirectToAction("Index", "Login");
        }

        public ActionResult Logout()
        {
            
            Session["cart"] = null; 
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home", new { Area = ""});
        }
    }
}