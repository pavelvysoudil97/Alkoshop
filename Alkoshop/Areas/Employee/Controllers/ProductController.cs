using Alkoshop.Class;
using DataAccess.Dao;
using DataAccess.Model;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alkoshop.Areas.Employee.Controllers
{
    public class ProductController : Controller
    {
        // GET: Employee/Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            CategoryDao categoryDao = new CategoryDao();
            CountryDao countryDao = new CountryDao();

            ViewBag.Categories = categoryDao.GetAll();
            ViewBag.Countries = countryDao.GetAll();
            return View(); 
        }

        [HttpPost]
        public ActionResult Add(Product product, HttpPostedFileBase picture, int countryId, int categoryId)
        {
            if (ModelState.IsValid)
            {
                if (picture != null)
                {
                    if (picture.ContentType == "image/jpeg" || picture.ContentType == "image/png")
                    {
                        Image image = Image.FromStream(picture.InputStream);

                        Guid guid = Guid.NewGuid();
                        string imageName = guid.ToString() + ".jpg";
                        if (image.Height > 200 || image.Width > 200)
                        {
                            Image smallImage = ImageHelper.ScaleImage(image, 200, 200);
                            Bitmap b = new Bitmap(smallImage);



                            b.Save(Server.MapPath("~/uploads/product/" + imageName), System.Drawing.Imaging.ImageFormat.Jpeg);

                            smallImage.Dispose();
                            b.Dispose();

                            product.Image = imageName;

                        }
                        else
                        {
                            picture.SaveAs(Server.MapPath("~/uploads/product") + picture.FileName);
                        }
                    };

                }

                CategoryDao categoryDao = new CategoryDao();
                Category category = categoryDao.GetById(categoryId);

                product.Category = category;

                CountryDao countryDao = new CountryDao();
                Country country = countryDao.GetById(countryId);

                product.Country = country;

                ProductDao productDao = new ProductDao();
                productDao.Create(product);
                TempData["message-success"] = "Kniha byla uspesne pridana";
            }
            else
            {
                return View("Create", product);
            }

            return RedirectToAction("Index","Home");

        }
         
        public ActionResult Edit(int productId)
        {
            ProductDao productDao = new ProductDao();
            Product product = productDao.GetById(productId);

            CategoryDao categoryDao = new CategoryDao();
            CountryDao countryDao = new CountryDao();

            ViewBag.Categories = categoryDao.GetAll();
            ViewBag.Countries = countryDao.GetAll();
            
            TempData["tempProductId"] = productId;
            return View(product);
        }
        [HttpPost]
        public ActionResult Update(Product product,HttpPostedFileBase picture, int categoryId, int availability, int pricePerUnit, int countryId)
        {
            ProductDao productDao = new ProductDao();
            CategoryDao categoryDao = new CategoryDao();
            CountryDao countryDao = new CountryDao();
            if (picture != null)
            {
                if (picture.ContentType == "image/jpeg" || picture.ContentType == "image/png")
                {
                    Image image = Image.FromStream(picture.InputStream);

                    Guid guid = Guid.NewGuid();
                    string imageName = guid.ToString() + ".jpg";
                    if (image.Height > 200 || image.Width > 200)
                    {
                        Image smallImage = ImageHelper.ScaleImage(image, 200, 200);
                        Bitmap b = new Bitmap(smallImage);



                        b.Save(Server.MapPath("~/uploads/product/" + imageName), System.Drawing.Imaging.ImageFormat.Jpeg);

                        smallImage.Dispose();
                        b.Dispose();

                        product.Image = imageName;

                    }
                    else
                    {
                        picture.SaveAs(Server.MapPath("~/uploads/product") + picture.FileName);
                    }
                };

            }
            
            product.Availability = availability;
            product.PricePerUnit = pricePerUnit;
            product.Category = categoryDao.GetById(categoryId);
            product.Country = countryDao.GetById(countryId);
            productDao.Update(product);
            TempData["message-success"] = "Produkt byl uspesne upraven";
            return RedirectToAction("Index", "Home");
        }

      

        public ActionResult ShowProductByCategory(int categoryId)
        {
            ProductDao productDao = new ProductDao();
            IList<Product> foundProducts = new List<Product>();
            foreach (Product p in productDao.GetAll())
            {
                if (p.Category.Id == categoryId)
                {
                    foundProducts.Add(p);
                }
            }
            TempData["foundProducts"] = foundProducts;
            return RedirectToAction("Index", "Home");
        }
    }
}