using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccess.Model
{
    public class CartItem
    {
        public CartItem(string name, string image, int productId, int pricePerUnit, int numberOfUnits)
        {
            Name = name;
            Image = image;
            ProductId = productId;
            PricePerUnit = pricePerUnit;
            NumberOfUnits = numberOfUnits;
        }
        public string Name { get; set; }
        public string Image { get; set; }
        public int ProductId { get; set; }
        public int PricePerUnit { get; set; }
        public int NumberOfUnits { get; set; }


    }
}