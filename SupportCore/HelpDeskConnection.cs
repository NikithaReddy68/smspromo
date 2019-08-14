using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Data.SqlClient;

namespace HelpDeskCore
{
    /// <summary>
    /// Summary description for CPGConnection
    /// </summary>
    public class HelpDeskConnection
    {
        public HelpDeskConnection()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public SqlConnection GetConnection(string ConnString)
        {
            string conStr = string.Empty;
            SqlConnection Conn = new SqlConnection();

            try
            {

                Conn.ConnectionString = ConnString;

                // Open the Connection

                if (Conn.State != ConnectionState.Open)
                    Conn.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Conn;
        }
    }
}
