using DataAccess.Dao;
using DataAccess.Model;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alkoshop.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detail(int productId)
        {
            ProductDao productDao = new ProductDao();
             Product product = productDao.GetById(productId);
            ReviewDao reviewDao = new ReviewDao();
            IList<Review> reviews = reviewDao.GetProductReview(product);
            ViewBag.Reviews = reviews;
            int count = reviews.Count;
            int evaluation = 0;
            foreach(Review r in reviews)
            {
                evaluation += r.Value;
            }
            ViewBag.Value = (evaluation / count);
            return View(product);
        }

        public ActionResult ShowProductByCategory(int categoryId)
        {
            ProductDao productDao = new ProductDao();
            IList<Product> foundProducts = new List<Product>();
            foreach(Product p in productDao.GetAll())
            {
                if(p.Category.Id == categoryId)
                {
                    foundProducts.Add(p);
                }
            }
            TempData["foundProducts"] = foundProducts;
            return RedirectToAction("Index", "Home");
        }
        

    }
}