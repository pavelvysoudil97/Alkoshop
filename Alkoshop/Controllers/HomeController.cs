using Alkoshop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            OracleConnection conn = DBMain.GetConnection();
 

            List<Product> products = DBGetData.getAllProducts(conn);
            foreach (Product product in products){
                
                System.Diagnostics.Debug.WriteLine(product.Name);
            }
            return View();
        }
    }
}