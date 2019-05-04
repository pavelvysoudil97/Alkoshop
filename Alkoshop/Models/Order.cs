using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alkoshop.Models
{
    public class Order
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public int AddressID { get; set; }
        public int CustomerID { get; set; }
        public int EmployeeID { get; set; }

    }
}