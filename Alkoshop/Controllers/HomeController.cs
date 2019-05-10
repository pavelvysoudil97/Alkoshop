using Alkoshop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oracle.DataAccess.Client;
using Alkoshop.Models;
using System.Drawing;
using Alkoshop.Class;
using System.Drawing.Imaging;
using System.IO;

namespace Alkoshop.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            TempData["conn"] = DBMain.GetConnection();

            //    DBGetData.insertPhoto(conn, "C:/amundsen.jpg"); //pro vlozeni obrazku do DB
            
            IList<Product> products = DBGetData.getAllProducts((OracleConnection)TempData["conn"]);
            
            return View(products);
        }
    }
}