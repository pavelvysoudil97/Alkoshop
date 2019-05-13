using Alkoshop.Database;
using Alkoshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alkoshop.Areas.Employee.Controllers
{
    public class OrderController : Controller
    {
        // GET: Employee/Order
        public ActionResult Index()
        {
            IList<Order> orders = DBGetData.getAllOrders(DBMain.GetConnection());
            return View(orders);
        }

        public ActionResult Detail(int orderId)
        {
            return View();
        }
    }
}