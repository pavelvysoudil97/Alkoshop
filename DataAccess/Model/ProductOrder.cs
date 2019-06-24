using DataAccess.Dao;
using DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DataAccess.Model
{
    public class ProductOrder : IEntity
    {
        public ProductOrder() { }
        public ProductOrder(int productId, int pricePerUnit, int numberOfUnits, int orderId = 0)
        {
            ProductDao productDao = new ProductDao();
            Product = productDao.GetById(productId);
            PricePerUnit = pricePerUnit;
            NumberOfUnit = numberOfUnits;
        }
        public virtual int Id { get; set; }
        [Required]
        public virtual Product Product { get; set; }
        [Required]
        public virtual Order Order { get; set; }
        [Required]
        public virtual int PricePerUnit { get; set; }
        [Required]
        public virtual int NumberOfUnit { get; set; }
    }
}