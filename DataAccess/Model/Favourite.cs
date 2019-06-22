using DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class Favourite : IEntity
    {
        public Favourite() { }
        public Favourite(Customer customer, Product product) {
            Product = product;
            Customer = customer;
        }
        public virtual int Id{ get; set;}
        public virtual Product Product { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
