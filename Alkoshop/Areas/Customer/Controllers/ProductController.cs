using Alkoshop.Database;
using Alkoshop.Models;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alkoshop.Areas.Customer.Controllers
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

                Session["conn"] = DBMain.GetConnection();

                IList<Category> alcoCategories = DBGetData.getCategories((OracleConnection)Session["conn"], 1);
                IList<Category> tabaccoCategories = DBGetData.getCategories((OracleConnection)Session["conn"], 2);
                ViewBag.AlcoCategories = alcoCategories;
                ViewBag.TabaccoCategories = tabaccoCategories;

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
