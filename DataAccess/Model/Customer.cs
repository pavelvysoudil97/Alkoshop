using DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace DataAccess.Model
{
    public class Customer : IEntity //MembershipUser, IEntity
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage ="Name is required")]
        public virtual string Name { get; set; }

        [Required(ErrorMessage = "Surname is required")]
        public virtual string Surname { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage ="Its not valid email address")]
        public virtual string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public virtual string Password { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Range(111111111, 999999999,ErrorMessage ="Neni regulerni telefonni cislo")]
        public virtual int PhoneNumber { get; set; }
        

        [Required(ErrorMessage = "Birth Date is required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public virtual DateTime BirthDate { get; set; }
        
        public virtual Address Address { get; set; }
        
    }
}