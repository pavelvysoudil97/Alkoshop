using DataAccess.Model;
using DataAccess.Model;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dao
{
    public class OrderDao : DaoBase<DataAccess.Model.Order>
    {
        public OrderDao() : base()
        {

        }
        public IList<DataAccess.Model.Order> GetByCustomer(DataAccess.Model.Customer customer)
        {
            return session.CreateCriteria<DataAccess.Model.Order>()
                .Add(Restrictions.Eq("Customer", customer))
                .List<DataAccess.Model.Order>();
        }
    }
}
