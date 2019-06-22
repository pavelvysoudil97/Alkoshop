using DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccess.Model
{
    public class Order : IEntity
    {
        public virtual int Id { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual string Status { get; set; }
        public virtual Address Address { get; set; }
        public virtual Customer Customer{ get; set; }
        public virtual int TotalPrice { get; set; }

    }
}