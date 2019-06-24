using DataAccess.Model;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dao
{
    public class ReviewDao : DaoBase<Review>
    {
        public ReviewDao() : base()
        {

        }
        public IList<Review> GetProductReview(Product product)
        {
                return session.CreateCriteria<Review>()
                    .Add(Restrictions.Eq("Product", product))
                    .List<Review>();
            }
    }
}
