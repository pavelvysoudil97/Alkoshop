using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alkoshop.Models
{
    public class Customer
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int PhoneNumber { get; set; }
        public string BirthDate { get; set; }
        
    }
}