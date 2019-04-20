using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Alkoshop.Models
{
    public class Address
    {
        public Address(string city, string street, string streetNumber, string zipCode)
        {
            City = city;
            Street = street;
            StreetNumber = streetNumber;
            ZipCode = zipCode;
        }

        public string id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string City { get; set; }

        public string Street { get; set; }
        [Required(ErrorMessage = "Street number is required")]
        public string StreetNumber { get; set; }
        [Required(ErrorMessage = "Zip code is required")]
        public string ZipCode { get; set; }
    }
}