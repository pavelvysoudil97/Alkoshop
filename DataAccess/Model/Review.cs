using DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class Review : IEntity
    {
        [Required]
        public virtual int Id { get; set; }
        [Required]
        public virtual int Value { get; set; }
        public virtual string Description { get; set; }
        [Required]
        public virtual Product Product { get; set; }
        [Required]
        public virtual Customer Customer { get; set; }

    }
}
