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

namespace Alkoshop.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            OracleConnection conn = DBMain.GetConnection();
 

            IList<Product> products = DBGetData.getAllProducts(conn);

            return View(products);
        }
    }
}