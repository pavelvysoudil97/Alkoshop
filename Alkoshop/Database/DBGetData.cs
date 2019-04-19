using System;
using System.Collections.Generic;
using System.Web;
using Alkoshop.Models;
using Oracle.DataAccess.Client;

namespace Alkoshop.Database
{
    public class DBGetData { 

        internal static List<Product> getAllProducts(OracleConnection conn)
        {
            List<Product> products = new List<Product>();
            OracleDataReader reader = getReader("SELECT * FROM ALKOHOLICI.\"Product\"", conn);
            if (reader != null) { 
                while (reader.Read())
                {
                    int id = (int) reader["ProductID"];
                    string name = (string) reader["Name"];
                    string producer = (string) reader["Producer"];
                 //   int pricePU = (int) reader["Price_per_unit"];
                    string availability = (string)reader["Availability"];
                    products.Add(new Product(id,name,producer,10,availability));
                }
                return products;
            }
            return null;
        }

        internal static List<int> getFavForCustomer(OracleConnection conn, int customerID)
        {
            List<int> favProductIDs = new List<int>();
            OracleDataReader reader = getReader("SELECT f.\"ProuctID\" FROM ALKOHOLICI.\"Favourite\" f WHERE f.\"CustomerID\" = "+customerID+";", conn);
            if (reader != null)
            {
                while (reader.Read())
                {
                    favProductIDs.Add((int)reader["ProductID"]);
                }
                return favProductIDs;
            }
            return null;
        }


        internal static OracleDataReader getReader(string comm, OracleConnection conn)
        {
            OracleCommand command = new OracleCommand(comm, conn); // use \" for "
            try
            {
                return command.ExecuteReader();
            }
            catch (Exception ex1)
            {
                System.Diagnostics.Debug.WriteLine("## ERROR: " + ex1.Message);
            }
            return null;
        }

        // TODO - getCategories 

        // TODO - getProductsByCategory

    
      //  internal static createCustomer(string name, string surname, string email, string password, int phoneNumber, DateTime birthDate, Address address ){
       
        
    }
}
