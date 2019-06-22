using DataAccess.Model;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dao
{
    public class CustomerDao : DaoBase<Customer>
    {
        public CustomerDao() : base()
        {

        }

        public Customer GetByEmailAndPassword(string email, string password)
        {
            return session.CreateCriteria<Customer>()
                .Add(Restrictions.Eq("Email", email))
                .Add(Restrictions.Eq("Password", password))
                .UniqueResult<Customer>();
        }

        public Customer GetByEmail(string email)
        {
            return session.CreateCriteria<Customer>()
                .Add(Restrictions.Eq("Email", email))
                .UniqueResult<Customer>();
        }
    }
}
