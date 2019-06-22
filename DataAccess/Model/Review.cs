using DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class Review : IEntity
    {
        public virtual int Id { get; set; }
        public virtual int Value { get; set; }
        public virtual string Description { get; set; }
        public virtual Product Product { get; set; }
        public virtual Customer Customer { get; set; }

    }
}
