using Alkoshop.Database;
using Alkoshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alkoshop.Areas.Customer.Controllers
{
    public class HomeController : Controller
    {
        // GET: Customer/Home
        public ActionResult Index()
        {
            IList<Product> products = DBGetData.getAllProducts(DBMain.GetConnection());

            return View(products);
        }
    }
}