using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;

namespace Alkoshop.Database
{
    class DBUtils
    {
        public static OracleConnection GetDBConnection()
        {
            string host = "ora1.uhk.cz";
            int port = 1521;
            string sid = "orcl.uhk.cz";
            string user = "alkoholici";
            string password = "alkoholici";

            return DBOracleUtils.GetDBConnection(host, port, sid, user, password);
        }
    }
}