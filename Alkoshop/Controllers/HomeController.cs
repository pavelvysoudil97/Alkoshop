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

            //    DBGetData.insertPhoto(conn, "C:/amundsen.jpg"); //pro vlozeni obrazku do DB

            IList<Product> products = DBGetData.getAllProducts((OracleConnection)Session["conn"]);
            
            return View(products);
        }
    }
}