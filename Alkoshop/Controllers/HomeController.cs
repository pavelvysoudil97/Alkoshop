using Alkoshop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oracle.DataAccess.Client;

namespace Alkoshop.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            OracleConnection conn = DBMain.GetConnection();
         /*   List<string> names = new List<string>();
            OracleCommand command = new OracleCommand("SELECT * FROM ALKOHOLICI.\"Category\"", conn); // use \" for "
            try
            {
                OracleDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string myField = (string)reader["Name"];
                    names.Add(myField);
                    System.Diagnostics.Debug.WriteLine(myField);
                }
                System.Diagnostics.Debug.WriteLine("--End of query--");
            }
            catch (Exception ex1)
            {
                System.Diagnostics.Debug.WriteLine("## ERROR: " + ex1.Message);
            }*/
            
            return View();
        }
    }
}