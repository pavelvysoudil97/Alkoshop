using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Alkoshop.Models
{
    public class Employee
    {
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Nickname is required")]
        public string Nickname { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Its not valid email address")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Gdpr is required")]
        public string Gdpr { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Range(111111111, 999999999, ErrorMessage = "Neni regulerni telefonni cislo")]
        public int PhoneNumber { get; set; }
        

        public int Salary { get; set; }
        
        public Address Address { get; set; }

    }
}