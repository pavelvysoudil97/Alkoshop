using System;
using System.Collections.Generic;
using System.Web;
using Alkoshop.Models;
using Oracle.DataAccess.Client;

namespace Alkoshop.Database
{
    public class DBGetData { 

        internal static IList<Product> getAllProducts(OracleConnection conn, int categoryID = 0)
        {
            IList<Product> products = new List<Product>();
            OracleDataReader reader;
            if (categoryID == 0)
            {
                reader = getReader("SELECT * FROM ALKOHOLICI.\"Product\" p JOIN \"ALKOHOLICI\".\"PRODUKTY_CENY_MNOZSTVI\" m ON p.NAME=m.NAME", conn);
            }else{
                reader = getReader("SELECT * FROM ALKOHOLICI.\"Product\" p JOIN \"ALKOHOLICI\".\"PRODUKTY_CENY_MNOZSTVI\" m ON p.NAME=m.NAME WHERE p.\"CategoryID\"=" + categoryID, conn);
            }
            if (reader != null) { 
                while (reader.Read())
                {
                    int id = (int) reader["ProductID"];
                    string name = (string) reader["Name"];
                    string producer = (string) reader["Producer"];                
                    string availability = (string)reader["Availability"];
                    double pricePU = (double)reader["Price"];
                    decimal amount = (decimal)reader["Amount"];
                    decimal alcotabac = (decimal)reader["Alcotabac"];
                    products.Add(new Product(id,name,producer,pricePU,(int)amount,availability,(int)alcotabac));
                }                
                return products;
            }
            return null;
        }

        internal static IList<Category> getCategories(OracleConnection conn)
        {
            IList<Category> categories = new List<Category>();
            OracleDataReader reader = getReader("SELECT * FROM ALKOHOLICI.\"Category\" ;", conn);
            if (reader != null)
            {
                while (reader.Read())
                {
                    categories.Add(new Category((int)reader["CategoryID"], (string)reader["Name"]));
                }
                return categories;
            }
            return null;
        }

        internal static IList<int> getFavForCustomer(OracleConnection conn, int customerID)
        {
            IList<int> favProductIDs = new List<int>();
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

        internal static Customer getCustomer(OracleConnection conn, string email, string password)
        {
            OracleDataReader reader = getReader("SELECT * FROM ALKOHOLICI.\"Customer\" WHERE \"Email\"='"+email+"' AND \"Password\"='"+password+"'", conn);
            reader.Read();
            string name = (string)reader["Name"];
            string surname = (string)reader["Surname"];
            int phoneNumber = Int32.Parse((string)reader["Phone_number"]);
            DateTime birthDate = (DateTime)reader["Birth_date"];
            string addressID = ((int)reader["AddressID"]).ToString();
            OracleDataReader reader2 = getReader("SELECT * FROM ALKOHOLICI.\"Address\" WHERE \"AddressID\"=" + addressID, conn);
            reader2.Read();
            Address address = new Address((string)reader2["City"], (string)reader2["Street"], (string)reader2["Street_number"], (string)reader2["Zip_code"]);
            return new Customer(name,surname,email,password,phoneNumber,birthDate,address);
        }

        private static OracleDataReader getReader(string comm, OracleConnection conn)
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

        internal static int createAddress(OracleConnection conn, Address address)
        {
            OracleCommand command = new OracleCommand("INSERT INTO ALKOHOLICI.\"Address\" (\"City\",\"Street\",\"Street_number\",\"Zip_code\") VALUES ('"+address.City+"','"+address.Street+"','"+address.StreetNumber+"','"+address.ZipCode+"')", conn);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex1)
            {
                System.Diagnostics.Debug.WriteLine("## ERROR: " + ex1.Message);
            }
            OracleCommand command2 = new OracleCommand("SELECT MAX(\"AddressID\") as id FROM ALKOHOLICI.\"Address\"", conn);
            OracleDataReader reader = command2.ExecuteReader();
            decimal id = 0;
            reader.Read();            
            id = (decimal)reader["id"];
            return (int)id;
        }        

        internal static void createCustomerWithAddress(OracleConnection conn, Customer customer, Address address)
        {
            int addressID = createAddress(conn,address);
            string comm = "INSERT INTO ALKOHOLICI.\"Customer\" (\"Birth_date\",\"Name\",\"Surname\",\"Email\",\"Password\",\"Phone_number\",\"Gdpr\",\"AddressID\") VALUES(:birthDate,'" + customer.Name+"','"+customer.Surname+"','"+customer.Email+"','"+customer.Password+"','"+customer.PhoneNumber+"','yes',"+addressID+")";    
            OracleCommand command = new OracleCommand(comm, conn);
            command.Parameters.Add(new OracleParameter("birthDate", OracleDbType.Date)).Value = customer.BirthDate;
          try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex1)
            {
                System.Diagnostics.Debug.WriteLine("## ERROR: " + ex1.Message);
            }
        }
    }
}
