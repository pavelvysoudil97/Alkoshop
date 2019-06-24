using DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DataAccess.Model
{
    public class Order : IEntity
    {
        public virtual int Id { get; set; }
        [Required]
        public virtual DateTime Date { get; set; }
        [Required]
        public virtual string Status { get; set; }
        [Required]
        public virtual Address Address { get; set; }
        [Required]
        public virtual Customer Customer{ get; set; }
        [Required]
        public virtual int TotalPrice { get; set; }

    }
}