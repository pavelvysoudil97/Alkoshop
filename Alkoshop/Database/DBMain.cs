using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;

namespace Alkoshop.Database
{
    public class DBMain
    {
        internal static OracleConnection GetConnection()
        {
            OracleConnection conn = DBUtils.GetDBConnection();
            try
            {
                conn.Open();
                System.Diagnostics.Debug.WriteLine("Successful Connection");
                return conn;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("## ERROR: " + ex.Message);
                return null;
            }          
        }       
    }
}