using DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DataAccess.Model
{
    public class Address : IEntity
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public virtual string City { get; set; }

        public virtual string Street { get; set; }

        [Required(ErrorMessage = "Street number is required")]
        public virtual int StreetNumber { get; set; }

        [Required(ErrorMessage = "Zip code is required")]
        [Range(11111, 99999, ErrorMessage = "Neni regulerni cislo popisne")]
        public virtual int ZipCode { get; set; }
    }
}