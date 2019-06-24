using DataAccess.Model;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dao
{
    public class ProductOrderDao : DaoBase<ProductOrder>
    {
        public ProductOrderDao() : base()
        {

        }
        public IList<ProductOrder> GetAllByOrder(DataAccess.Model.Order order)
        {
            return session.CreateCriteria<ProductOrder>()
                .Add(Restrictions.Eq("Order", order))
                .List<ProductOrder>();
        }
    }
}
