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
    public class CartController : Controller
    {
        // GET: Customer/Cart
        public ActionResult Index()
        {
            IList<CartItem> cartItems = (List<CartItem>)Session["cart"];

            return View(cartItems);

        }

        [HttpPost]
        public ActionResult Add(string name, string image, int productId, int pricePerUnit, int numberOfUnit)
        {

            CartItem cartItem = new CartItem(name, image, productId, pricePerUnit, numberOfUnit);
            if(Session["cart"] == null)
            {
                IList<CartItem> startingCartProducts = new List<CartItem>();
                Session["cart"] = startingCartProducts;
            }
            IList<CartItem> cartProducts = (List<CartItem>)Session["cart"];
            cartProducts.Add(cartItem);
            Session["cart"] = cartProducts;

            TempData["message-success"] = "Polozka byla uspesne pridana do Vaseho kosiku";
            return RedirectToAction("Index", "Home");
        }
    }
}