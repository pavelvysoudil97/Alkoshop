using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alkoshop.Models
{
    public class ProductOrder
    {
        public ProductOrder(int productID, int orderID, int price_per_unit, int number_of_unit)
        {
            ProductID = productID;
            OrderID = orderID;
            Price_per_unit = price_per_unit;
            Number_of_unit = number_of_unit;
        }

        public int ProductID { get; set; }
        public int OrderID { get; set; }
        public int Price_per_unit { get; set; }
        public int Number_of_unit { get; set; }
    }
}