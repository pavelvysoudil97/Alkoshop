using DataAccess.Model;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dao
{
    public class AddressDao : DaoBase<Address>
    {
        public AddressDao() : base()
        {

        }
        public Address SearchByAllParams(string city, int streetNumber, int zipCode, string street = null)
        {
            Address address = session.CreateCriteria<Address>()
                .Add(Restrictions.Eq("City", city))
                .Add(Restrictions.Eq("Street", street))
                .Add(Restrictions.Eq("StreetNumber", streetNumber))
                .Add(Restrictions.Eq("ZipCode", zipCode))
                .UniqueResult<Address>();
            return address;
        }
        

    }
}
