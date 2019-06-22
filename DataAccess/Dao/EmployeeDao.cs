using DataAccess.Model;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dao
{
    public class EmployeeDao : DaoBase<Employee>
    {
        public EmployeeDao() : base()
        {

        }

        public Employee GetByEmailAndPassword(string email, string password)
        {
            return session.CreateCriteria<Employee>()
                .Add(Restrictions.Eq("Email", email))
                .Add(Restrictions.Eq("Password", password))
                .UniqueResult<Employee>();
        }

        public Employee GetByEmail(string email)
        {
            return session.CreateCriteria<Employee>()
                .Add(Restrictions.Eq("Email", email))
                .UniqueResult<Employee>();
        }
    }
}
