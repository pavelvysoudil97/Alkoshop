using System;
using System.Collections.Generic;
using System.IO;
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
                    string description = (string)reader["Description"];
                    int pictureID = (int)reader["PictureID"];
                    if(pictureID!=0)
                    {
                        string path = System.Web.HttpContext.Current.Server.MapPath("~/Design/") + pictureID + ".jpg";       
                        getPhotoAndSave(conn, path, pictureID);
                        products.Add(new Product(id, name, producer, pricePU, (int)amount, availability, (int)alcotabac, description, "/Design/" + pictureID + ".jpg"));
                        continue;
                    }
                    products.Add(new Product(id,name,producer,pricePU,(int)amount,availability,(int)alcotabac,description));
                }                
                return products;
            }
            return null;
        }

        internal static Product getProductByID(OracleConnection conn, int productID)
        {
            OracleDataReader reader = getReader("SELECT * FROM ALKOHOLICI.\"Product\" p JOIN \"ALKOHOLICI\".\"PRODUKTY_CENY_MNOZSTVI\" m ON p.NAME=m.NAME WHERE p.\"ProductID\"="+productID, conn);
            reader.Read();
            int id = (int)reader["ProductID"];
            string name = (string)reader["Name"];
            string producer = (string)reader["Producer"];
            string availability = (string)reader["Availability"];
            double pricePU = (double)reader["Price"];
            decimal amount = (decimal)reader["Amount"];
            decimal alcotabac = (decimal)reader["Alcotabac"];
            string description = (string)reader["Description"];
            int pictureID = (int)reader["PictureID"];
            if (pictureID != 0)
            {
                string path = System.Web.HttpContext.Current.Server.MapPath("~/Design/") + pictureID + ".jpg";
                return new Product(id, name, producer, pricePU, (int)amount, availability, (int)alcotabac, description, "/Design/" + pictureID + ".jpg");
            }
            return new Product(id, name, producer, pricePU, (int)amount, availability, (int)alcotabac, description);
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
        
        internal static IList<Product> getFavForCustomer(OracleConnection conn, int customerID)
        {
            IList<Product> products = new List<Product>();
            OracleDataReader reader = getReader("SELECT * FROM ALKOHOLICI.\"Product\" p JOIN \"ALKOHOLICI\".\"PRODUKTY_CENY_MNOZSTVI\" m ON p.NAME=m.NAME JOIN ALKOHOLICI.\"Favourite\" f ON p.\"ProductID\"=f.PRODUCTID WHERE f.\"CustomerID\" = " + customerID + "", conn);
            if (reader != null)
            {
                while (reader.Read())
                {
                    int id = (int)reader["ProductID"];
                    string name = (string)reader["Name"];
                    string producer = (string)reader["Producer"];
                    string availability = (string)reader["Availability"];
                    double pricePU = (double)reader["Price"];
                    decimal amount = (decimal)reader["Amount"];
                    decimal alcotabac = (decimal)reader["Alcotabac"];
                    string description = (string)reader["Description"];
                    int pictureID = (int)reader["PictureID"];
                    if (pictureID != 0)
                    {
                        string path = System.Web.HttpContext.Current.Server.MapPath("~/Design/") + pictureID + ".jpg";
                        getPhotoAndSave(conn, path, pictureID);
                        products.Add(new Product(id, name, producer, pricePU, (int)amount, availability, (int)alcotabac, description, "/Design/" + pictureID + ".jpg"));
                        continue;
                    }
                    products.Add(new Product(id, name, producer, pricePU, (int)amount, availability, (int)alcotabac, description));
                }
                return products;
            }
            return null;
        }

        internal static Customer getCustomer(OracleConnection conn, string email, string password)
        {
            if(email != null && password != null)
            {
                OracleDataReader reader = getReader("SELECT * FROM ALKOHOLICI.\"Customer\" WHERE \"Email\"='" + email + "' AND \"Password\"='" + password + "'", conn);
                reader.Read();
                try
                {
                    int customerID = (int)reader["CustomerID"];
                    string name = (string)reader["Name"];
                    string surname = (string)reader["Surname"];
                    int phoneNumber = Int32.Parse((string)reader["Phone_number"]);
                    DateTime birthDate = (DateTime)reader["Birth_date"];
                    int addressID = (int)reader["AddressID"];
                    OracleDataReader reader2 = getReader("SELECT * FROM ALKOHOLICI.\"Address\" WHERE \"AddressID\"=" + addressID, conn);
                    reader2.Read();
                    Address address = new Address((string)reader2["City"], (string)reader2["Street"], (string)reader2["Street_number"], (string)reader2["Zip_code"]);
                    return new Customer(customerID, name, surname, email, password, phoneNumber, birthDate, address);
                }
                catch
                {
                    return null;
                }
                
            }
            return null;        
        }

        internal static Employee getEmployee(OracleConnection conn, string email, string password)
        {
            if (email != null && password != null)
            {
                OracleDataReader reader = getReader("SELECT * FROM ALKOHOLICI.\"Employee\" WHERE \"Email\"='" + email + "' AND \"Password\"='" + password + "'", conn);
                reader.Read();
                int employeeID = (int)reader["EmployeeID"];
                string name = (string)reader["Name"];
                string surname = (string)reader["Surname"];
                string nickname = (string)reader["Nickname"];
                int salary = (int)reader["Salary"];
                int phoneNumber = Int32.Parse((string)reader["Phone_number"]);
                int addressID = (int)reader["AddressID"];
                OracleDataReader reader2 = getReader("SELECT * FROM ALKOHOLICI.\"Address\" WHERE \"AddressID\"=" + addressID, conn);
                reader2.Read();
                Address address = new Address((string)reader2["City"], (string)reader2["Street"], (string)reader2["Street_number"], (string)reader2["Zip_code"]);
                return new Employee(employeeID, name, surname, nickname, email, password, phoneNumber, salary, address);
            }
            return null;
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

        internal static void createOrder(OracleConnection conn, Order order, ProductOrder[] productOrders)
        {
            OracleCommand command = new OracleCommand("INSERT INTO ALKOHOLICI.\"Order\" (\"Date\",\"Status\",\"AddressID\",\"CustomerID\",\"EmployeeID\") VALUES(:date,'" + order.Status + "','" + order.AddressID + "','" + order.CustomerID + "','" + order.EmployeeID + ")", conn);
            command.Parameters.Add(new OracleParameter("date", OracleDbType.Date)).Value = order.Date;
            command.ExecuteNonQuery();

            foreach (ProductOrder productOrder in productOrders)
            {
                OracleCommand cmd = new OracleCommand("INSERT INTO ALKOHOLICI.\"ProductOrder\" (\"ProductID\",\"OrderID\",\"Price_per_unit\",\"Number_of_unit\") VALUES('" + productOrder.ProductID + "','" + order.ID + "','" + productOrder.Price_per_unit + "','" + productOrder.Number_of_unit + ")", conn);
                cmd.ExecuteNonQuery();
            }
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

        internal static void createEmployeeWithAddress(OracleConnection conn, Employee employee, Address address)
        {
            int addressID = createAddress(conn, address);
            string comm = "INSERT INTO ALKOHOLICI.\"Employee\" (\"Name\",\"Surname\",\"Nickname\",\"Email\",\"Password\",\"Phone_number\",\"Salary\",\"Gdpr\",\"AddressID\") VALUES('" + employee.Name + "','" + employee.Surname + "','" + employee.Nickname + "','" + employee.Email + "','" + employee.Password + "','" + employee.PhoneNumber + "','" + employee.Salary + "','yes'," + addressID + ")";
            OracleCommand command = new OracleCommand(comm, conn);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex1)
            {
                System.Diagnostics.Debug.WriteLine("## ERROR: " + ex1.Message);
            }
        }

        internal static void insertPhoto(OracleConnection conn, string sourceLoc)
        {
            FileStream fs = new FileStream(sourceLoc, FileMode.Open, FileAccess.Read);
            byte[] ImageData = new byte[fs.Length];
            fs.Read(ImageData, 0, System.Convert.ToInt32(fs.Length));
            fs.Close();

            OracleCommand cmd = new OracleCommand("INSERT INTO ALKOHOLICI.\"Picture\" (\"Data\",\"Date_of_upload\",\"Suffix\") VALUES (:blobtodb,:date,'jpg')", conn);
            cmd.Parameters.Add(new OracleParameter("blobtodb", OracleDbType.Blob)).Value = ImageData;
            cmd.Parameters.Add(new OracleParameter("date", OracleDbType.Date)).Value = DateTime.Now;
            try
            {
                cmd.ExecuteNonQuery();
                System.Diagnostics.Debug.WriteLine("Image inserted");
            }
            catch (Exception ex1)
            {
                System.Diagnostics.Debug.WriteLine("## ERROR: " + ex1.Message);
            }
        }
        
        internal static void getPhotoAndSave(OracleConnection conn, string destinationLoc, int pictureID)
        {
            OracleDataReader reader = getReader("SELECT p.\"Data\" FROM ALKOHOLICI.\"Picture\" p WHERE p.\"PictureID\" = '"+pictureID+"'", conn);
            reader.Read();
            byte[] byteData = new byte[0];
            byteData = (byte[])reader["Data"];

            FileStream fs = new FileStream(@destinationLoc, FileMode.OpenOrCreate, FileAccess.Write);
            fs.Write(byteData, 0, byteData.GetUpperBound(0));
            fs.Close();
            System.Diagnostics.Debug.WriteLine("Image saved");
        }
        
        // TODO: FINAL - CRD - ProductOrder - Order 

    }
}
