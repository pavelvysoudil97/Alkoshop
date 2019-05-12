using Alkoshop.Database;
using Alkoshop.Models;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alkoshop.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detail(int productId)
        {
            OracleConnection connection = DBMain.GetConnection();
             Product product = DBGetData.getProductByID(connection, productId);

            return View(product); //product
        }

        public ActionResult ShowProductByCategory(int categoryId)
        {
            OracleConnection connection = DBMain.GetConnection();
            IList<Product> foundProducts = DBGetData.getAllProducts(connection, categoryId);
            TempData["foundProducts"] = foundProducts;
            return RedirectToAction("Index", "Home", new { incomingProducts = foundProducts });
        }
        

    }
}