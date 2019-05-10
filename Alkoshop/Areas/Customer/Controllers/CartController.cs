using Alkoshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alkoshop.Areas.Customer.Controllers
{
    public class CartController : Controller
    {
        // GET: Customer/Cart
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(int productId, int pricePerUnit, int numberOfUnit)
        {
            ProductOrder productOrder = new ProductOrder(productId, 0, pricePerUnit, numberOfUnit);
            if(Session["cart"] == null)
            {
                IList<ProductOrder> startingCartProducts = new List<ProductOrder>();
                Session["cart"] = startingCartProducts;
            }
            IList<ProductOrder> cartProducts = (List<ProductOrder>)Session["cart"];
            cartProducts.Add(productOrder);
            Session["cart"] = cartProducts;
            return RedirectToAction("Index", "Home");
        }
    }
}