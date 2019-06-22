using DataAccess.Interface;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Model
{

    public class Employee : IEntity
    {
        
        public virtual int Id { get; set; }

        [Required(ErrorMessage ="Name is required")]
        public virtual string Name { get; set; }

        [Required(ErrorMessage = "Surname is required")]
        public virtual string Surname { get; set; }


        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Its not valid email address")]
        public virtual string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]
        public virtual string Password { get; set; }
        

        [Required(ErrorMessage = "Phone number is required")]
        [Range(111111111, 999999999, ErrorMessage = "Neni regulerni telefonni cislo")]
        public virtual int PhoneNumber { get; set; }
        

        public virtual int Salary { get; set; }
        
        public virtual Address Address { get; set; }

    }
}