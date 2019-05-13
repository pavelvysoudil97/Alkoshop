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
            IList<Category> categories = DBGetData.getCategories(Session["connection"] as OracleConnection, 0);
            ViewBag.Categories = categories;
            return View();
        }

        [HttpPost]
        public ActionResult Add(Product product, HttpPostedFileBase picture, string countryName, int categoryId,int alcoTabac)
        {
            if (ModelState.IsValid)
            {
                IDictionary<int, string> countries = DBGetData.getCountries(Session["connection"] as OracleConnection);
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

                        DBGetData.insertPhoto(Session["connection"] as OracleConnection, Server.MapPath("~/Design/" + picture.FileName));

                        product.Amount = 10;
                        product.Availability = "yes";
                        product.Alcotabac = alcoTabac;
                        DBGetData.addProduct(Session["connection"] as OracleConnection, product, countryId, categoryId, Server.MapPath("~/Design/" + picture.FileName));
                        TempData["message-success"] = "Produkt byl úspěšně přidán";
                    }
                }
            }
            return RedirectToAction("Index","Home");
        }

        public ActionResult Update(Product productId)
        {
            return View();
        }
    }
}