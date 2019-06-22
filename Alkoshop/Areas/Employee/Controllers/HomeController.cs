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

            //IList<Category> alcoCategories = DBGetData.getCategories((OracleConnection)Session["conn"], 1);
            //IList<Category> tabaccoCategories = DBGetData.getCategories((OracleConnection)Session["conn"], 2);
            //ViewBag.AlcoCategories = alcoCategories;
            //ViewBag.TabaccoCategories = tabaccoCategories;

            //if (TempData["foundProducts"] != null)
            //{
            //    IList<Product> incomingProducts = TempData["foundProducts"] as List<Product>;
            //    return View(incomingProducts);
            //}
            ////    DBGetData.insertPhoto(conn, "C:/amundsen.jpg"); //pro vlozeni obrazku do DB


            //IList<Product> products = DBGetData.getAllProducts((OracleConnection)Session["conn"]);
            return View();// products);
        }
    }
}