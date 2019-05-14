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
            ViewBag.OrderStates = new List<string> { "new", "exp", "strn" };
            return View(orders);
        }

       public ActionResult Update(int orderId, string stateDropdown)
        {
            DBGetData.changeOrderStatus(DBMain.GetConnection(), orderId, stateDropdown, (Session["User"] as Alkoshop.Models.Employee).ID);
            TempData["message-success"] = "Status objednavky zmenen";
            return RedirectToAction("Index");
        }
    }
}