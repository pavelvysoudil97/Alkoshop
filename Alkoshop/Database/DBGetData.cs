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
                reader = getReader("SELECT * FROM ALKOHOLICI.\"Product\" p JOIN \"ALKOHOLICI\".\"PRODUKTY_CENY_MNOZSTVI\" m ON p.NAME=m.NAME JOIN \"ALKOHOLICI\".\"Country\" c ON p.\"CountryID\"=c.\"CountryID\"", conn);
            }else{
                reader = getReader("SELECT * FROM ALKOHOLICI.\"Product\" p JOIN \"ALKOHOLICI\".\"PRODUKTY_CENY_MNOZSTVI\" m ON p.NAME=m.NAME JOIN \"ALKOHOLICI\".\"Country\" c ON p.\"CountryID\"=c.\"CountryID\" WHERE p.\"CategoryID\"=" + categoryID, conn);
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
                    string country = (string)reader["COUNTRY"];
                    int pictureID = (int)reader["PictureID"];
                    if(pictureID!=0)
                    {
                        string path = System.Web.HttpContext.Current.Server.MapPath("~/Design/") + pictureID + ".jpg";       
                        getPhotoAndSave(conn, path, pictureID);
                        products.Add(new Product(id, name, producer, pricePU, (int)amount, availability, (int)alcotabac, description, country, "/Design/" + pictureID + ".jpg"));
                        continue;
                    }
                    products.Add(new Product(id,name,producer,pricePU,(int)amount,availability,(int)alcotabac,description,country));
                }                
                return products;
            }
            return null;
        }

        internal static Product getProductByID(OracleConnection conn, int productID)
        {
            OracleDataReader reader = getReader("SELECT * FROM ALKOHOLICI.\"Product\" p JOIN \"ALKOHOLICI\".\"PRODUKTY_CENY_MNOZSTVI\" m ON p.NAME=m.NAME JOIN \"ALKOHOLICI\".\"Country\" c ON p.\"CountryID\"=c.\"CountryID\" WHERE p.\"ProductID\"=" + productID, conn);
            reader.Read();
            int id = (int)reader["ProductID"];
            string name = (string)reader["Name"];
            string producer = (string)reader["Producer"];
            string availability = (string)reader["Availability"];
            double pricePU = (double)reader["Price"];
            decimal amount = (decimal)reader["Amount"];
            decimal alcotabac = (decimal)reader["Alcotabac"];
            int alcotabacID = (int)reader["ALCOTABACID"];
            string description = (string)reader["Description"];
            string country = (string)reader["COUNTRY"];
            int pictureID = (int)reader["PictureID"];
            double priceWOdph = countProdutcPriceWOdph(conn, (int)alcotabac, alcotabacID);
            if (pictureID != 0)
            {
                string path = System.Web.HttpContext.Current.Server.MapPath("~/Design/") + pictureID + ".jpg";
                return new Product(id, name, producer, pricePU, (int)amount, availability, (int)alcotabac, description, country, "/Design/" + pictureID + ".jpg", priceWOdph);
            }
            return new Product(id, name, producer, pricePU, (int)amount, availability, (int)alcotabac, description, country, priceWOdph);
        }

        internal static double countProdutcPriceWOdph(OracleConnection conn, int alcotabac, int alcotabacID)
        {
            string comm = "";
            if (alcotabac == 1)
            {
                comm = "SELECT DPH_COUNTER_ALCOHOL(" + alcotabacID + ") AS cena FROM DUAL";
            }
            if(alcotabac == 2)
            {
                comm = "SELECT DPH_COUNTER_TABACCO(" + alcotabacID + ") AS cena FROM DUAL";
            }
            OracleDataReader reader = getReader(comm, conn);
            reader.Read();
            decimal cena = (decimal)reader["CENA"];
            return (double)cena;
        }

        internal static IList<Category> getCategories(OracleConnection conn, int alcotabac /*1 - alkohol; 2 - tabak*/)
        {
            IList<Category> categories = new List<Category>();
            string comm = "SELECT \"CategoryID\", \"Name\" FROM ALKOHOLICI.\"Category\"";
            if(alcotabac == 1)
            {
                comm = "SELECT DISTINCT c.\"CategoryID\", c.\"Name\" FROM ALKOHOLICI.\"Product\" p JOIN ALKOHOLICI.\"Category\" c ON p.\"CategoryID\" = c.\"CategoryID\" WHERE p.\"AlcoholID\" IS NOT NULL";
            }
            if (alcotabac == 2)
            {
                comm = "SELECT DISTINCT c.\"CategoryID\", c.\"Name\" FROM ALKOHOLICI.\"Product\" p JOIN ALKOHOLICI.\"Category\" c ON p.\"CategoryID\" = c.\"CategoryID\" WHERE p.\"TabaccoID\" IS NOT NULL";
            }
            OracleDataReader reader = getReader(comm, conn);
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

        internal static void addProductToFav(OracleConnection conn, int customerID, int productID)
        {
            OracleCommand cmd = new OracleCommand("INSERT INTO ALKOHOLICI.\"Favourite\" (\"CustomerID\",\"PRODUCTID\") VALUES('" + customerID + "','" + productID + "')", conn);
            cmd.ExecuteNonQuery();
        }

        internal static void removeProductFromFav(OracleConnection conn, int customerID, int productID)
        {
            OracleCommand cmd = new OracleCommand("DELETE FROM ALKOHOLICI.\"Favourite\" WHERE \"CustomerID\"='" + customerID + "' AND \"PRODUCTID\"='" + productID + "'", conn);
            cmd.ExecuteNonQuery();
        }

        internal static IList<Product> getFavForCustomer(OracleConnection conn, int customerID)
        {
            IList<Product> products = new List<Product>();
            OracleDataReader reader = getReader("SELECT * FROM ALKOHOLICI.\"Product\" p JOIN \"ALKOHOLICI\".\"PRODUKTY_CENY_MNOZSTVI\" m ON p.NAME=m.NAME JOIN ALKOHOLICI.\"Favourite\" f ON p.\"ProductID\"=f.PRODUCTID JOIN \"ALKOHOLICI\".\"Country\" c ON p.\"CountryID\"=c.\"CountryID\" WHERE f.\"CustomerID\" = " + customerID + "", conn);
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
                    string country = (string)reader["COUNTRY"];
                    int pictureID = (int)reader["PictureID"];
                    if (pictureID != 0)
                    {
                        string path = System.Web.HttpContext.Current.Server.MapPath("~/Design/") + pictureID + ".jpg";
                        getPhotoAndSave(conn, path, pictureID);
                        products.Add(new Product(id, name, producer, pricePU, (int)amount, availability, (int)alcotabac, description, country, "/Design/" + pictureID + ".jpg"));
                        continue;
                    }
                    products.Add(new Product(id, name, producer, pricePU, (int)amount, availability, (int)alcotabac, description, country));
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
                    Address address = new Address(addressID,(string)reader2["City"], (string)reader2["Street"], (string)reader2["Street_number"], (string)reader2["Zip_code"]);
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
                Address address = new Address(addressID,(string)reader2["City"], (string)reader2["Street"], (string)reader2["Street_number"], (string)reader2["Zip_code"]);
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

        internal static void createOrder(OracleConnection conn, Order order, IList<ProductOrder> productOrders)
        {
            OracleCommand command = new OracleCommand("INSERT INTO ALKOHOLICI.\"Order\" (\"Date\",\"Status\",\"AddressID\",\"CustomerID\") VALUES(:orderdate,'" + order.Status + "','" + order.AddressID + "','" + order.CustomerID + "')", conn);
            command.Parameters.Add(new OracleParameter("orderdate", OracleDbType.Date)).Value = order.Date;
            command.ExecuteNonQuery();

            OracleCommand command2 = new OracleCommand("SELECT MAX(\"OrderID\") as id FROM ALKOHOLICI.\"Order\"", conn);
            OracleDataReader reader = command2.ExecuteReader();
            reader.Read();
            decimal orderID = (decimal)reader["id"];

            foreach (ProductOrder productOrder in productOrders)
            {
                OracleCommand cmd = new OracleCommand("INSERT INTO ALKOHOLICI.\"ProductOrder\" (\"ProductID\",\"OrderID\",\"PRICE_PER_UNIT\",\"NUMBER_OF_UNIT\") VALUES('" + productOrder.ProductID + "','" + (int)orderID + "','" + productOrder.Price_per_unit + "','" + productOrder.Number_of_unit + "')", conn);
                cmd.ExecuteNonQuery();
            }
        }

        internal static IList<Order> getAllOrders(OracleConnection conn)
        {
            IList<Order> orders = new List<Order>();
            OracleDataReader reader = getReader("SELECT * FROM ALKOHOLICI.\"Order\"", conn);
            while (reader.Read())
            {
                int id = (int)reader["OrderID"];
                DateTime date = (DateTime)reader["Date"];
                string status = (string)reader["Status"];
                int customerID = (int)reader["CustomerID"];
                int addressID = (int)reader["AddressID"];
                int employeeID = (int)reader["EmployeeID"];
                orders.Add(new Order(id, date, status, addressID, customerID, employeeID));
            }
            return orders;
        }

        internal static IList<Order> getOrdersForCustomer(OracleConnection conn, int customerID)
        {
            IList<Order> orders = new List<Order>();
            OracleDataReader reader = getReader("SELECT * FROM ALKOHOLICI.\"Order\" WHERE \"CustomerID\"=" + customerID, conn);
            while (reader.Read())
            {
                int id = (int)reader["OrderID"];
                DateTime date = (DateTime)reader["Date"];
                string status = (string)reader["Status"];
                int employeeID = (int)reader["EmployeeID"];
                int addressID = (int)reader["AddressID"];
                orders.Add(new Order(id, date, status, addressID, customerID, employeeID));
            }
            return orders;
        }

        internal static void changeOrderStatus(OracleConnection conn, int orderID, string status, int employeeID)
        {
            OracleCommand command = new OracleCommand("UPDATE ALKOHOLICI.\"Order\" SET \"Status\"='" +status+ "',\"EmployeeID\"='" + employeeID + "' WHERE \"OrderID\"=" + orderID, conn);
            command.ExecuteNonQuery();
        }

        // Kdyz chcces zmenit i adresu tak musi byt vyplneno oldAddressID a newAddress!
        internal static void changeCustomerData(OracleConnection conn, int customerID, string name, string surname, string pass, string email, int phoneNumber, int oldAddressID = 0, Address newAddress = null)
        {
            string comm = "UPDATE ALKOHOLICI.\"Customer\" SET \"Name\"='" + name + "',\"Surname\"='" + surname + "',\"Password\"='" + pass + "',\"Email\"='" + email + "',\"Phone_number\"='" + phoneNumber + "' WHERE \"CustomerID\"=" + customerID;
            if (oldAddressID!=0 && newAddress!=null)
            {
                OracleCommand cmnd = new OracleCommand("DELETE FROM ALKOHOLICI.\"Address\" WHERE \"AddressID\"=" + oldAddressID, conn);
                cmnd.ExecuteNonQuery();
                int addressID = createAddress(conn, newAddress);
                comm = "UPDATE ALKOHOLICI.\"Customer\" SET \"Name\"='" + name + "',\"Surname\"='" + surname + "',\"Password\"='" + pass + "',\"Email\"='" + email + "',\"Phone_number\"='" + phoneNumber + "',\"AddressID\"='" + addressID + "' WHERE \"CustomerID\"=" + customerID;
            }
            OracleCommand command = new OracleCommand(comm, conn);
            command.ExecuteNonQuery();
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

        internal static bool createCustomerWithAddress(OracleConnection conn, Customer customer, Address address)
        {
            int addressID = createAddress(conn,address);
            string comm = "INSERT INTO ALKOHOLICI.\"Customer\" (\"Birth_date\",\"Name\",\"Surname\",\"Email\",\"Password\",\"Phone_number\",\"Gdpr\",\"AddressID\") VALUES(:birthDate,'" + customer.Name+"','"+customer.Surname+"','"+customer.Email+"','"+customer.Password+"','"+customer.PhoneNumber+"','yes','"+addressID+"')";    
            OracleCommand command = new OracleCommand(comm, conn);
            command.Parameters.Add(new OracleParameter("birthDate", OracleDbType.Date)).Value = customer.BirthDate;
            try
            {
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex1)
            {
                System.Diagnostics.Debug.WriteLine("## ERROR: " + ex1.Message);
                return false;
            }
        }

        internal static void createEmployeeWithAddress(OracleConnection conn, Employee employee, Address address)
        {
            int addressID = createAddress(conn, address);
            string comm = "INSERT INTO ALKOHOLICI.\"Employee\" (\"Name\",\"Surname\",\"Nickname\",\"Email\",\"Password\",\"Phone_number\",\"Salary\",\"Gdpr\",\"AddressID\") VALUES('" + employee.Name + "','" + employee.Surname + "','" + employee.Nickname + "','" + employee.Email + "','" + employee.Password + "','" + employee.PhoneNumber + "','" + employee.Salary + "','yes','" + addressID + "')";
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

            OracleCommand cmd = new OracleCommand("INSERT INTO ALKOHOLICI.\"Picture\" (\"Data\",\"Date_of_upload\",\"Suffix\") VALUES (:blobtodb,:dateomg,'jpg')", conn);
            cmd.Parameters.Add(new OracleParameter("blobtodb", OracleDbType.Blob)).Value = ImageData;
            cmd.Parameters.Add(new OracleParameter("dateomg", OracleDbType.Date)).Value = DateTime.Now;
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

        internal static IDictionary<int,string> getCountries(OracleConnection conn)
        {
            IDictionary<int, string> countries = new Dictionary<int, string>();
            OracleDataReader reader = getReader("SELECT * FROM ALKOHOLICI.\"Country\"", conn);
            if (reader != null)
            {
                while (reader.Read())
                {
                    countries.Add((int)reader["CountryID"], (string)reader["COUNTRY"]);
                }
                return countries;
            }
            return null;
        }

        //Product Constructor: Product(name, producer, pricePU, amount, availability, description, alcotabac)!!!!!!
        internal static void addProduct(OracleConnection conn, Product product, int countryID, int categoryID, string pictureLocation)
        {
            if (product.Alcotabac == 1)
            {
                string comm1 = "INSERT INTO ALKOHOLICI.\"Alcohol\" (\"Bottle_amount\",\"Price_per_bottle\") VALUES('" + product.Amount + "','" + product.PricePU + "')";
                new OracleCommand(comm1, conn).ExecuteNonQuery();
                int alcoholID = maxID(conn, "Alcohol", "AlcoholID");
                insertPhoto(conn, pictureLocation);
                int pictureID = maxID(conn, "Picture", "PictureID");
                string comm2 = "INSERT INTO ALKOHOLICI.\"Product\" (\"Availability\",\"AlcoholID\",\"CountryID\",\"PictureID\",\"NAME\",\"PRODUCER\",\"CategoryID\",\"DESCRIPTION\") VALUES('" + product.Availability + "','" + alcoholID + "','" + countryID + "','" + pictureID + "','" + product.Name + "','" + product.Producer + "','" + categoryID + "','" + product.Description + "')";
                new OracleCommand(comm2, conn).ExecuteNonQuery();
            }
            if (product.Alcotabac == 2)
            {
                string comm1 = "INSERT INTO ALKOHOLICI.\"Tabacco\" (\"Gram_amount\",\"Price_per_gram\") VALUES('" + product.Amount + "','" + product.PricePU + "')";
                new OracleCommand(comm1, conn).ExecuteNonQuery();
                int tabaccoID = maxID(conn, "Tabacco", "TabaccoID");
                insertPhoto(conn, pictureLocation);
                int pictureID = maxID(conn, "Picture", "PictureID");
                string comm2 = "INSERT INTO ALKOHOLICI.\"Product\" (\"Availability\",\"TabaccoID\",\"CountryID\",\"PictureID\",\"NAME\",\"PRODUCER\",\"CategoryID\",\"DESCRIPTION\") VALUES('" + product.Availability + "','" + tabaccoID + "','" + countryID + "','" + pictureID + "','" + product.Name + "','" + product.Producer + "','" + categoryID + "','" + product.Description + "')";
                new OracleCommand(comm2, conn).ExecuteNonQuery();
            }
        }

        private static int maxID(OracleConnection conn, string table, string id)
        {
            OracleCommand comm = new OracleCommand("SELECT MAX(\""+id+"\") as id FROM ALKOHOLICI.\""+table+"\"", conn);
            OracleDataReader reader = comm.ExecuteReader();
            decimal ID = 0;
            reader.Read();
            ID = (decimal)reader["id"];
            return (int)ID;
        }

        internal static void changeProduct(OracleConnection conn, int productID, int alcotabac, int amount, int pricePU)
        {
            OracleDataReader reader = getReader("SELECT \"AlcoholID\",\"TabaccoID\" FROM ALKOHOLICI.\"Product\" WHERE \"ProductID\"=" + productID, conn);
            reader.Read();
            string comm = "";
            if (alcotabac == 1)
            {
                int alcoholID = (int)reader["AlcoholID"];
                comm = "UPDATE ALKOHOLICI.\"Alcohol\" SET \"Bottle_amount\"='" + amount + "',\"Price_per_bottle\"='" + pricePU + "' WHERE \"AlcoholID\"=" + alcoholID;
            }            
            if (alcotabac == 2)
            {
                int tabaccoID = (int)reader["TabaccoID"];
                comm = "UPDATE ALKOHOLICI.\"Tabacco\" SET \"Gram_amount\"='" + amount + "',\"Price_per_gram\"='" + pricePU + "' WHERE \"TabaccoID\"=" + tabaccoID;
            }
            OracleCommand command = new OracleCommand(comm, conn);
            command.ExecuteNonQuery();
        }

        internal static void removeProduct(OracleConnection conn, int productID)
        {
            string comm = "UPDATE ALKOHOLICI.\"Product\" SET \"Availability\"='no' WHERE \"ProductID\"=" + productID;
            OracleCommand command = new OracleCommand(comm, conn);
            command.ExecuteNonQuery();
        }

        // POZOR na hodnoty v Productu!!!! viz nize
        internal static IList<Product> getOrderedProducts(OracleConnection conn)
        {
            IList<Product> products = new List<Product>();
            OracleDataReader reader = getReader("SELECT * FROM \"ALKOHOLICI\".\"OBJEDNANE_PRODUKTY\"", conn);
            if (reader != null)
            {
                while (reader.Read())
                {
                    int productID = (int)reader["ProductID"];
                    int customerID = (int)reader["CustomerID"];
                    int orderID = (int)reader["OrderID"];
                    string NAME = (string)reader["NAME"];
                    DateTime date = (DateTime)reader["Date"];
                    string producer = (string)reader["Producer"];
                    string fullName = (string)reader["Name"]+" "+(string)reader["Surname"];                    
                    int amount = (int)reader["NUMBER_OF_UNIT"];
                    // COUNTRY, CENA, ALKOTABAK,CENAWOdph = nenastavene!!, Misto Availability je tam datum a misto Description je tam ten zbytek!!!!!
                    products.Add(new Product(productID,NAME,producer,0,amount,date.ToString(),0,"OrderID: "+orderID+", CustomerID: " + customerID + ", Jméno zákazníka: " +fullName,""));
                }
                return products;
            }
            return null;
        }

        internal static IDictionary<string, int> getCountOfProductsInCountries(OracleConnection conn)
        {
            IDictionary<string, int> count = new Dictionary<string, int>();
            OracleDataReader reader = getReader("SELECT * FROM \"ALKOHOLICI\".\"POCTY_PRODUKTU_V_ZEMICH\"", conn);
            if (reader != null)
            {
                while (reader.Read())
                {
                    decimal pocet = (decimal)reader["POCET"];
                    count.Add((string)reader["COUNTRY"], (int)pocet);
                }
                return count;
            }
            return null;
        }

    }
}
