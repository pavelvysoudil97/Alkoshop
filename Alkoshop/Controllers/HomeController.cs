using System.Collections.Generic;
using System.Web.Mvc;
using Oracle.DataAccess.Client;
using DataAccess.Model;
using DataAccess.Dao;
using System.Data.SqlClient;
using System;

namespace Alkoshop.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
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

        public ActionResult AboutUs()
        {
            return View();
        }

        public ActionResult Conditions()
        {
            return View();
        }
    }
}