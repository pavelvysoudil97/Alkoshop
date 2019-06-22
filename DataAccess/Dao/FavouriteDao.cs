using DataAccess.Model;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dao
{
    public class FavouriteDao : DaoBase<Favourite>
    {
        public FavouriteDao() : base()
        {

        }

        public IList<Favourite> GetAllByCustomer(Customer customer)
        {
            return session.CreateCriteria<Favourite>()
                .Add(Restrictions.Eq("Customer", customer))
                .List<Favourite>();
        }

    }
}
