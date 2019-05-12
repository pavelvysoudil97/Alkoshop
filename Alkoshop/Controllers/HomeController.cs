using Alkoshop.Database;
using System.Collections.Generic;
using System.Web.Mvc;
using Oracle.DataAccess.Client;
using Alkoshop.Models;

namespace Alkoshop.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            Session["conn"] = DBMain.GetConnection();

            IList<Category> alcoCategories = DBGetData.getCategories((OracleConnection)Session["conn"], 1);
            IList<Category> tabaccoCategories = DBGetData.getCategories((OracleConnection)Session["conn"], 2);
            ViewBag.AlcoCategories = alcoCategories;
            ViewBag.TabaccoCategories = tabaccoCategories;

            if (TempData["foundProducts"] != null)
            {
                IList<Product> incomingProducts = TempData["foundProducts"] as List<Product>;
                return View(incomingProducts);
            }
            //    DBGetData.insertPhoto(conn, "C:/amundsen.jpg"); //pro vlozeni obrazku do DB
            
            
            IList<Product> products = DBGetData.getAllProducts((OracleConnection)Session["conn"]);
            return View(products); 
        }
    }
}