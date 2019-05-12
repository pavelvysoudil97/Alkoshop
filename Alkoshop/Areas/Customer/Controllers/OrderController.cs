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
    public class OrderController : Controller
    {
        // GET: Customer/Order
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            OracleConnection connection = DBMain.GetConnection();

            Address address = TempData["addressOrder"] as Address;
           

            Order order = new Order();
            
            order.CustomerID = (Session["User"] as Alkoshop.Models.Customer).ID;

            DateTime dateTime = DateTime.UtcNow.Date;
            order.Date = dateTime;
            order.Status = "waiting";

            IList<CartItem> cartItems = (Session["cart"] as List<CartItem>);
            IList<ProductOrder> productOrders = new List<ProductOrder>();
            
            int totalPrice = 0;
            
            foreach(CartItem cartItem in cartItems)
            {
                ProductOrder productOrder = new ProductOrder(cartItem.ProductId, 0, cartItem.PricePerUnit, cartItem.NumberOfUnits);
                productOrders.Add(productOrder);
                totalPrice += (cartItem.PricePerUnit * cartItem.NumberOfUnits);

            }
            ViewBag.TotalPrice = totalPrice;
            ViewBag.Order = order;
            ViewBag.Address = address;
            ViewBag.OrderUser = (Session["User"] as Alkoshop.Models.Customer);

            TempData["potentialOrder"] = order;
            TempData["potentialProductOrders"] = productOrders;
            TempData["potentialAddress"] = address;
            return View(cartItems);
        }

        public ActionResult TrueCreate()
        {
            OracleConnection connection = DBMain.GetConnection();

            Order order = TempData["potentialOrder"] as Order;
            IList<ProductOrder> productOrder = TempData["potentialProductOrders"] as List<ProductOrder>;
            Address address = TempData["potentialAddress"] as Address;

            int addressID = DBGetData.createAddress(connection, address);
            order.AddressID = addressID;

            DBGetData.createOrder(connection, order, productOrder);
            TempData["message-success"] = "Vaše objednávka byla úspěšně vytvořena";
            Session["cart"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult CustomerOrders()
        {
            OracleConnection connection = Session["conn"] as OracleConnection;
            int customerId = (Session["User"] as Alkoshop.Models.Customer).ID;

            IList<Order> orders = DBGetData.getOrdersForCustomer(connection, customerId);

            return View(orders);
        }

    }
}