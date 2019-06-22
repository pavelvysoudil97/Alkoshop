using DataAccess.Dao;
using DataAccess.Model;
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
            if (Membership.ValidateUser(email, password))
            {
                FormsAuthentication.SetAuthCookie(email, false);

                if (email.Contains("@alkoshop.com"))
                {
                    EmployeeDao employeeDao = new EmployeeDao();
                    DataAccess.Model.Employee employee = employeeDao.GetByEmailAndPassword(email, password);
                    Session["User"] = employee;
                    Session["UserRole"] = "Employee";
                }
                else
                {
                    CustomerDao customerDao = new CustomerDao();
                    DataAccess.Model.Customer customer = customerDao.GetByEmailAndPassword(email, password);
                        Session["User"] = customer;
                        Session["UserRole"] = "Customer";
                }
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