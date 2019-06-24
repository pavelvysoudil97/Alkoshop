using DataAccess.Dao;
using DataAccess.Model;
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
            OrderDao orderDao = new OrderDao();

               IList<Order> orders = orderDao.GetAll();
               ViewBag.OrderStates = new List<string> { "new", "exp", "strn" };
            return View( orders);
        }

       public ActionResult Update(int orderId, string stateDropdown)
        {
            OrderDao orderDao = new OrderDao();
            Order order = orderDao.GetById(orderId);
            order.Status = stateDropdown;
            orderDao.Update(order);
            TempData["message-success"] = "Status objednavky zmenen";
            return RedirectToAction("Index");
        }

        public ActionResult TodoOrder()
        {
            OrderDao orderDao = new OrderDao();
            IList<Order> orders = orderDao.GetAllOrderToExp();
            orders.OrderByDescending<Order, DateTime>(x => x.Date);
            
            return View(orders);
        }

        public ActionResult Detail(int orderId)
        {
            
                ProductOrderDao productOrderDao = new ProductOrderDao();
                OrderDao orderDao = new OrderDao();
                IList<Order> ordersToExp = orderDao.GetAllOrderToExp();

                IList<ProductOrder> products = new List<ProductOrder>();

                foreach (Order o in ordersToExp)
                {
                    IList<ProductOrder> productsByOrder = productOrderDao.GetAllByOrder(o);
                    foreach (ProductOrder p in productsByOrder)
                    {
                        products.Add(p);
                    }
                }
                
            Order order = orderDao.GetById(orderId);
            ViewBag.Order = order;
            ViewBag.TotalPrice = order.TotalPrice;

            return View(products);
        }
    }
}