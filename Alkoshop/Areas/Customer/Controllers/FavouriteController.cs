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
   public class FavouriteController : Controller
    {
       // GET: Customer/Favourite
       public ActionResult Index()
       {
           DataAccess.Model.Customer customer = Session["User"] as DataAccess.Model.Customer;
            FavouriteDao favouriteDao = new FavouriteDao();
            ProductDao productDao = new ProductDao();
            IList<Favourite> favourites = favouriteDao.GetAllByCustomer(customer);
            IList<Product> favouriteProducts = new List<Product>();
            foreach(Favourite f in favourites)
            {
                Product p = productDao.GetById(f.Product.Id);
                favouriteProducts.Add(p);
            }

            return View(favouriteProducts);
       }

        public ActionResult Add(int productId)
        {
            DataAccess.Model.Customer customer = Session["User"] as DataAccess.Model.Customer;
            FavouriteDao favouriteDao = new FavouriteDao();
            ProductDao productDao = new ProductDao();
            IList<Favourite> favourites = favouriteDao.GetAllByCustomer(customer);
            IList<Product> favouriteProducts = new List<Product>();
            foreach (Favourite f in favourites)
            {
                Product p = productDao.GetById(f.Product.Id);
                favouriteProducts.Add(p);
            }
            foreach(Product p in favouriteProducts)
             {
                       if(p.Id == productId)
                        {
                            TempData["message-nosuccess"] = "Tento produkt jiz mate v oblibenych";
                            return RedirectToAction("Index", "Favourite");
                        }
             }
            favouriteDao.Create(new Favourite(customer, productDao.GetById(productId)));
            TempData["message-success"] = "Produkt byl pridan k vasim oblibenym";
            return RedirectToAction("Index", "Favourite");
            }
        public ActionResult Remove(int productId)
        {
            DataAccess.Model.Customer customer = Session["User"] as DataAccess.Model.Customer;
            FavouriteDao favouriteDao = new FavouriteDao();
            ProductDao productDao = new ProductDao();
            IList<Favourite> favourites = favouriteDao.GetAllByCustomer(customer);
            IList<Product> favouriteProducts = new List<Product>();
            foreach (Favourite f in favourites)
            {
                if (f.Product.Id == productId)
                {
                    favouriteDao.Delete(f);
                    TempData["message-success"] = "Produkt byl odebran z Vasich oblibenych";
                    return RedirectToAction("Index", "Favourite");
                }
            }
            TempData["message-nosuccess"] = "Tento produkt jiz mate v oblibenych";
            return RedirectToAction("Index", "Favourite");
        }
    }
}