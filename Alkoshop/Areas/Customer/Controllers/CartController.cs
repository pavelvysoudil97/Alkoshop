
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
    public class CartController : Controller
    {
        // GET: Customer/Cart
        public ActionResult Index()
        {
            IList<CartItem> cartItems = (List<CartItem>)Session["cart"];
            IList<ProductOrder> productOrders = new List<ProductOrder>();
            int totalPrice = 0;

            foreach (CartItem cartItem in cartItems)
            {
                
                totalPrice += (cartItem.PricePerUnit * cartItem.NumberOfUnits);

            }
            ViewBag.TotalPrice = totalPrice;
            return View(cartItems);

        }

       [HttpPost]
        public ActionResult Add(string name, string image, int productId, int pricePerUnit, int numberOfUnit)
        {
               ProductDao productDao = new ProductDao();
               int amountOfProduct = productDao.GetById(productId).Availability;
               if (numberOfUnit > amountOfProduct)
                {
                    TempData["message-nosuccess"] = "Ve skladu je jiz pouze " + amountOfProduct + " kusů";
                    return RedirectToAction("Detail","Product", new { productId });
                }
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

        public ActionResult Delete(int cartItemId)
        {
            IList<CartItem> cartProducts = (List<CartItem>)Session["cart"];
            IList<CartItem> temporaryCartProducts = new List<CartItem>();
            foreach(CartItem item in cartProducts)
            {
                if ((item.ProductId).CompareTo(cartItemId) != 0)
                {
                    temporaryCartProducts.Add(item);
                }
            }
            Session["cart"] = temporaryCartProducts;
            TempData["message-success"] = "Produkt byl úspěšně odebrán";
            return RedirectToAction("Index", "Cart");
        }
        [HttpPost]
        public ActionResult Update(int cartItemProductId, int count)
        {
            IList<CartItem> cartProducts = (List<CartItem>)Session["cart"];
            ProductDao productDao = new ProductDao();

            foreach(CartItem item in cartProducts)
            {   
                if (item.ProductId.CompareTo(cartItemProductId) == 0)
                {
                    if(productDao.GetById(item.ProductId).Availability < count)
                    {
                        TempData["message-no-success"] = "Na skladě není požadované množství";
                        return RedirectToAction("Index", "Cart");
                    }
                    item.NumberOfUnits = count;
                }
            }
            Session["cart"] = cartProducts;
            TempData["message-success"] = "Produkt byl úspěšně upraven";

            return RedirectToAction("Index", "Cart");
        }
    }
}