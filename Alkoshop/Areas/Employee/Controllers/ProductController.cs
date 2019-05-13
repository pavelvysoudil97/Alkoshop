using Alkoshop.Class;
using Alkoshop.Database;
using Alkoshop.Models;
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
            IDictionary<int, string> countries = DBGetData.getCountries(DBMain.GetConnection());
            ViewBag.Countries = countries.Values;
            IList<Category> categories = DBGetData.getCategories(DBMain.GetConnection(), 0);
            ViewBag.Categories = categories;
            return View();
        }

        [HttpPost]
        public ActionResult Add(Product product, HttpPostedFileBase picture, string countryName, int categoryId,int alcoTabac)
        {
            if (ModelState.IsValid)
            {
                IDictionary<int, string> countries = DBGetData.getCountries(DBMain.GetConnection());
                int countryId = 0;

                foreach (KeyValuePair<int, string> item in countries)
                {
                    if (item.Value == countryName)
                    {
                        countryId = item.Key;
                    }
                }

                if (picture != null)
                {
                    if (picture.ContentType == "image/jpeg" || picture.ContentType == "image/png")
                    {
                        Image image = Image.FromStream(picture.InputStream);

                        if (image.Height > 200 && image.Width > 200)
                        {
                            Image smallImage = ImageHelper.ScaleImage(image, 200, 200);
                            Bitmap b = new Bitmap(smallImage);

                            Guid guid = Guid.NewGuid();
                            

                            b.Save(Server.MapPath("~/Design/" + picture.FileName), ImageFormat.Jpeg);

                            smallImage.Dispose();
                            b.Dispose();


                        }
                        else
                        {
                            picture.SaveAs(Server.MapPath("~/Design/" + picture.FileName));
                        }


                        product.Amount = 10;
                        product.Availability = "yes";
                        product.Alcotabac = alcoTabac;
                        DBGetData.addProduct(DBMain.GetConnection(), product, countryId, categoryId, Server.MapPath("~/Design/" + picture.FileName));
                        new System.IO.FileInfo(Server.MapPath("~/Design/" + picture.FileName)).Delete();
                        TempData["message-success"] = "Produkt byl úspěšně přidán";
                    }
                }
            }
            return RedirectToAction("Index","Home");
        }

        public ActionResult Edit(int productId)
        {
            OracleConnection connection = DBMain.GetConnection();

            Product product = DBGetData.getProductByID(connection, productId);
            IDictionary<int, string> countries = DBGetData.getCountries(DBMain.GetConnection());
            ViewBag.Countries = countries.Values;
            IList<Category> categories = DBGetData.getCategories(DBMain.GetConnection(), 0);
            ViewBag.Categories = categories;
            TempData["tempProductId"] = productId;
            return View(product);
        }
        [HttpPost]
        public ActionResult Update(int alcoTabac, int amount, int pricePU)
        {
            int productId =(int) TempData["tempProductId"];
            DBGetData.changeProduct(DBMain.GetConnection(), productId, alcoTabac, amount, pricePU);

            TempData["message-success"] = "Produkt byl uspesne upraven";
            return RedirectToAction("Index", "Home");
        }
    }
}