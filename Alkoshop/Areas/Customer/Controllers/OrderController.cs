using DataAccess.Dao;
using DataAccess.Model;
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
            Address address;
            AddressDao addressDao = new AddressDao();

            if (newAddress)
            {
                address = TempData["addressOrder"] as Address;
                addressDao.Create(address);
            }else
            {
                address = (Session["User"] as DataAccess.Model.Customer).Address;
            }
            
            Order order = new Order();
            order.Status = "new";
            DateTime dateTime = DateTime.Now;

            order.Customer = (Session["User"] as DataAccess.Model.Customer);
            order.Date = dateTime;
            order.Address = address;
            IList<CartItem> cartItems = (Session["cart"] as List<CartItem>);
            IList<ProductOrder> productOrders = new List<ProductOrder>();
            
            int totalPrice = 0;
            
            foreach(CartItem cartItem in cartItems)
            {
                ProductOrder productOrder = new ProductOrder(cartItem.ProductId, cartItem.PricePerUnit, cartItem.NumberOfUnits, 0);
                productOrders.Add(productOrder);
                totalPrice += (cartItem.PricePerUnit * cartItem.NumberOfUnits);

            }
            order.TotalPrice = totalPrice;

            ViewBag.TotalPrice = totalPrice;
            ViewBag.Order = order;

            TempData["potentialOrder"] = order;
            TempData["potentialProductOrders"] = productOrders;
            TempData["potentialAddress"] = address;
            TempData["newAddress"] = newAddress;
            return View(cartItems);
        }

        public ActionResult TrueCreate()
        {
            OrderDao orderDao = new OrderDao();
            Order order = TempData["potentialOrder"] as Order;
            IList<ProductOrder> productOrders = TempData["potentialProductOrders"] as List<ProductOrder>;
            Address address = TempData["potentialAddress"] as Address;
            ProductOrderDao productOrderDao = new ProductOrderDao();
            
            
            order.Address = address;
            orderDao.Create(order);

            foreach (ProductOrder p in productOrders)
            {
                p.Order = order;
                productOrderDao.Create(p);
            }

            TempData["message-success"] = "Vaše objednávka byla úspěšně vytvořena";
            Session["cart"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult CustomerOrders()
        {
            DataAccess.Model.Customer customer = (Session["User"] as DataAccess.Model.Customer);
            OrderDao orderDao = new OrderDao();
            IList<Order> orders = orderDao.GetByCustomer(customer);
            
            return View(orders);
        }

        public ActionResult Detail(int orderId)
        {
            ProductOrderDao productOrderDao = new ProductOrderDao();
            OrderDao orderDao = new OrderDao();
            Order order = orderDao.GetById(orderId);
            ViewBag.Order = order;
            ViewBag.TotalPrice = order.TotalPrice;
            IList<ProductOrder> products = productOrderDao.GetAllByOrder(order);
            return View(products);
        }

    }
}