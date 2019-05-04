using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Alkoshop.Models
{
    public class Customer : MembershipUser
    {
        public Customer()
        {

        }
        public Customer(int id, string name, string surname, string email, string password, int phoneNumber, DateTime birthDate, Address address)
        {
            ID = id;
            Name = name;
            Surname = surname;
            Email = email;
            Password = password;
            PhoneNumber = phoneNumber;
            BirthDate = birthDate;
            Address = address;
        }
        public int ID { get; set; }

        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is required")]
        public string Surname { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage ="Its not valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Range(111111111, 999999999,ErrorMessage ="Neni regulerni telefonni cislo")]
        public int PhoneNumber { get; set; }

        [Required(ErrorMessage = "Birth Date is required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime BirthDate { get; set; }
        
        public Address Address { get; set; }
        
    }
}