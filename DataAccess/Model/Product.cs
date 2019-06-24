using DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace DataAccess.Model
{
    public class Product : IEntity
    {
        [Required]
        public virtual int Id { get; set; }

        [Required]
        public virtual string Name { get; set; }

        [Required]
        public virtual string Producer { get; set; }

        [Required]
        public virtual int Availability { get; set; }
        
        public virtual Country Country { get; set; }
        public virtual string Description { get; set; }
        public virtual string Image { get; set; }

        [Required]
        public virtual int PricePerUnit { get; set; }

        public virtual Category Category { get; set; }
    }
}