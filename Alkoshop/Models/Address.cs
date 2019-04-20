using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Alkoshop.Models
{
    public class Address
    {
        [Required(ErrorMessage = "Name is required")]
        public string City { get; set; }

        public string Street { get; set; }

        [Required(ErrorMessage = "Street number is required")]
        public int StreetNumber { get; set; }

        [Required(ErrorMessage = "Zip code is required")]
        public int ZipCode { get; set; }
    }
}