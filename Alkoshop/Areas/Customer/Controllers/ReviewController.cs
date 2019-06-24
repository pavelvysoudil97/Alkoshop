using DataAccess.Dao;
using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alkoshop.Areas.Customer.Controllers
{
    public class ReviewController : Controller
    {
        // GET: Customer/Review
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Evaluate(int productId)
        {
            ProductDao productDao = new ProductDao();
            ViewBag.Product = productDao.GetById(productId);
            return View();
        }

        public ActionResult Vote(Review review, int productId)
        {
            ReviewDao reviewDao = new ReviewDao();
            ProductDao productDao = new ProductDao();
            review.Product = productDao.GetById(productId);
            review.Customer = Session["User"] as DataAccess.Model.Customer;

            reviewDao.Create(review);
            TempData["message-success"] = "Úspěšně jste ohodnotil produkt";
            return RedirectToAction("Index", "Home");

        }
    }
}