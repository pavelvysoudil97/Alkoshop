using Alkoshop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alkoshop.Areas.Employee.Controllers
{
    public class HomeController : Controller
    {
        // GET: Employee/Home
        public ActionResult Index()
        {
            Session["connection"] = DBMain.GetConnection();
            return View();
        }
    }
}