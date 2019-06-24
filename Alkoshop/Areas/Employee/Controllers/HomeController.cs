using DataAccess.Dao;
using DataAccess.Model;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alkoshop.Areas.Employee.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // GET: Customer/Home
        public ActionResult Index()
        {
            CategoryDao categoryDao = new CategoryDao();
            IList<Category> categories = categoryDao.GetAll();

            ViewBag.Categories = categories;

            if (TempData["foundProducts"] != null)
            {
                IList<Product> incomingProducts = TempData["foundProducts"] as List<Product>;
                return View(incomingProducts);
            }

            ProductDao productDao = new ProductDao();
            IList<Product> products = productDao.GetAll();
            return View(products);
        }
    }
}