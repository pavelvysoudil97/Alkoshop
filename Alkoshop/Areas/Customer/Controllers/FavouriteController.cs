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
    public class FavouriteController : Controller
    {
        // GET: Customer/Favourite
        public ActionResult Index()
        {

           // User user = DBGetData.getCustomer((OracleConnection)TempData["conn"], User.Identity.Name);
           // IList<Product> products = DBGetData.getAllProducts((OracleConnection)TempData["conn"]);
           // IList<Product> favProducts = DBGetData.getFavProductsForCustomer((OracleConnection)TempData["conn"], user, products);

            return View();
        }
    }
}