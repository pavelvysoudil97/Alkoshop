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
    [Authorize]
    public class OrderController : Controller
    {
        // GET: Customer/Order
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(bool newAddress)
        {
            OracleConnection connection = DBMain.GetConnection();
            Address address;
            if (newAddress)
            {
                address = TempData["addressOrder"] as Address;
            }else
            {
                address = (Session["User"] as Alkoshop.Models.Customer).Address;
            }
            
            Order order = new Order();
            
            order.CustomerID = (Session["User"] as Alkoshop.Models.Customer).ID;

            DateTime dateTime = DateTime.Now;
            order.Date = dateTime;
            order.Status = "new";

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
            TempData["newAddress"] = newAddress;
            return View(cartItems);
        }

        public ActionResult TrueCreate()
        {
            OracleConnection connection = DBMain.GetConnection();

            Order order = TempData["potentialOrder"] as Order;
            IList<ProductOrder> productOrder = TempData["potentialProductOrders"] as List<ProductOrder>;
            Address address = TempData["potentialAddress"] as Address;

            int addressID;
            if ((bool)TempData["newAddress"])
            {
                addressID = DBGetData.createAddress(connection, address);
            }
            else
            {
                addressID = (Session["User"] as Alkoshop.Models.Customer).Address.ID;

            };
            
            order.AddressID = addressID;

            DBGetData.createOrder(connection, order, productOrder);
            TempData["message-success"] = "Vaše objednávka byla úspěšně vytvořena";
            Session["cart"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult CustomerOrders()
        {
            OracleConnection connection = DBMain.GetConnection();
            int customerId = (Session["User"] as Alkoshop.Models.Customer).ID;

            IList<Order> orders = DBGetData.getOrdersForCustomer(connection, customerId);

            Session["conn"] = DBMain.GetConnection();

            IList<Category> alcoCategories = DBGetData.getCategories((OracleConnection)Session["conn"], 1);
            IList<Category> tabaccoCategories = DBGetData.getCategories((OracleConnection)Session["conn"], 2);
            ViewBag.AlcoCategories = alcoCategories;
            ViewBag.TabaccoCategories = tabaccoCategories;

            return View(orders);
        }

    }
}