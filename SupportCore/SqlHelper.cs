using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;


namespace HelpDeskCore
{
    /// <summary>
    /// The purpose of this class is used for interaction with database 
    /// </summary>
    public class SqlHelper
    {
        #region Get DetaSet
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SPName"></param>
        /// <param name="SQLParams"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(string SPName, ref SqlParameter[] SQLParams,string ConnString)
        {

            SqlCommand Command = new SqlCommand();
            HelpDeskConnection ac = new HelpDeskConnection();
            SqlConnection Conn = ac.GetConnection(ConnString);
            SqlDataAdapter Adapter = new SqlDataAdapter();
            DataSet DataSetDtls = new DataSet();

            try
            {

                if (Command.Parameters.Count > 0)
                    Command.Parameters.Clear();

                Command.CommandText = SPName;
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddRange(SQLParams);
                Command.Connection = Conn;
                Command.CommandTimeout = 300;
                Adapter.SelectCommand = Command;

                Adapter.Fill(DataSetDtls);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }

            return DataSetDtls;
        }
        /// <summary>
        /// this method returns the dataset by executing the sql statement
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(string sql)
        {
            SqlCommand Command = new SqlCommand();
            SqlDataAdapter Adapter = new SqlDataAdapter();
            DataSet DataSetDtls = new DataSet();
         
            try
            {
                using (SqlConnection Conn = new SqlConnection(ConnectionString))
                {
                    Conn.Open();
                    Command.CommandText = sql;
                    Command.CommandType = CommandType.Text;
                    Command.Connection = Conn;
                    Adapter.SelectCommand = Command;
                    Adapter.Fill(DataSetDtls);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return DataSetDtls;
        }

        public static DataSet ExecuteDataSet(string storedProcedure,SqlConnection Conn)
        {
            SqlCommand Command = new SqlCommand();
            SqlDataAdapter Adapter = new SqlDataAdapter();
            DataSet DataSetDtls = new DataSet();

            try
            {

                    Command.CommandText = storedProcedure;
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Connection = Conn;
                    //Adapter.SelectCommand = Command;
                    Adapter.Fill(DataSetDtls);
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return DataSetDtls;
        }

        public static DataSet ExecuteDataSet(string sql, SqlConnection Conn,SqlTransaction Trans)
        {
            SqlCommand Command = new SqlCommand();
            SqlDataAdapter Adapter = new SqlDataAdapter();
            DataSet DataSetDtls = new DataSet();

            try
            {
               
                Command.CommandText = sql;
                Command.CommandType = CommandType.Text;
                Command.Connection = Conn;
                Command.Transaction = Trans;
                Adapter.SelectCommand = Command;
                Adapter.Fill(DataSetDtls);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return DataSetDtls;
        }


        public static DataSet ExecuteDataSet(string sql,string ConnString)
        {

            SqlCommand Command = new SqlCommand();
            SqlDataAdapter Adapter = new SqlDataAdapter();
            DataSet DataSetDtls = new DataSet();

            try
            {
                using (SqlConnection Conn = new SqlConnection(ConnString))
                {
                    Conn.Open(); 
                    Command.CommandText = sql;
                    Command.CommandType = CommandType.Text;
                    Command.Connection = Conn;
                    Adapter.SelectCommand = Command;
                    Adapter.Fill(DataSetDtls);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return DataSetDtls;
        }

        #endregion

        #region get DataTable
        /// <summary>
        /// this method retuns the datatable by executing stored procedure using connection string 
        /// </summary>
        /// <param name="SPName"></param>
        /// <param name="SQLParams"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string SPName, ref SqlParameter[] SQLParams,string ConnString)
        {
            SqlCommand Command = new SqlCommand();
            SqlDataAdapter Adapter = new SqlDataAdapter();
            DataTable DataDetails = new DataTable();

            try
            {

                if (Command.Parameters.Count > 0)
                    Command.Parameters.Clear();

                Command.CommandText = SPName;
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddRange(SQLParams);
                HelpDeskConnection ac = new HelpDeskConnection();
                Command.Connection = ac.GetConnection(ConnString);
                Adapter.SelectCommand = Command;

                Adapter.Fill(DataDetails);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (Command.Connection != null)
                {
                    if (Command.Connection.State == ConnectionState.Open)
                        Command.Connection.Close();
                }
            }

            return DataDetails;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Sql"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string Sql,string ConnString)
        {
            SqlCommand Command = new SqlCommand();
            SqlDataAdapter Adapter = new SqlDataAdapter();
            DataTable DataDetails = new DataTable();
            try
            {
                Command.CommandText = Sql;
                Command.CommandType = CommandType.Text;
                HelpDeskConnection ac = new HelpDeskConnection();
                Command.Connection = ac.GetConnection(ConnString);
                Adapter.SelectCommand = Command;

                Adapter.Fill(DataDetails);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (Command.Connection != null)
                {
                    if (Command.Connection.State == ConnectionState.Open)
                        Command.Connection.Close();
                }
            }
            return DataDetails;
        }
        /// <summary>
        /// return datatable based on connection and transaction passed
        /// </summary>
        /// <param name="Sql"></param>
        /// <param name="conn"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string Sql, SqlConnection conn, SqlTransaction trans)
        {
            SqlCommand Command = new SqlCommand();
            SqlDataAdapter Adapter = new SqlDataAdapter();
            DataTable DataDetails = new DataTable();
            try
            {
                Command.CommandText = Sql;
                Command.CommandType = CommandType.Text;
                Command.Transaction = trans;
                Command.Connection = conn;
                Adapter.SelectCommand = Command;

                Adapter.Fill(DataDetails);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return DataDetails;
        }

        public static DataTable ExecuteDataTable(string Sql, SqlConnection conn)
        {
            SqlCommand Command = new SqlCommand();
            SqlDataAdapter Adapter = new SqlDataAdapter();
            DataTable DataDetails = new DataTable();
            try
            {
                Command.CommandText = Sql;
                Command.CommandType = CommandType.Text;
                Command.Connection = conn;
                Adapter.SelectCommand = Command;

                Adapter.Fill(DataDetails);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return DataDetails;
        }
        #endregion

        #region executereader
        /// <summary>
        /// this method retuns the reader by executing stored procedure
        /// </summary>
        /// <param name="SPName"></param>
        /// <param name="SQLParams"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string SPName,ref SqlParameter[] SQLParams)
        {
            SqlCommand Command = new SqlCommand();
            SqlDataReader Reader = null;
            SqlConnection Conn = new SqlConnection(ConnectionString);
            try
            {
                Conn.Open(); 
                if (Command.Parameters.Count > 0)
                    Command.Parameters.Clear();

                Command.CommandText = SPName;
                Command.Connection = Conn;
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddRange(SQLParams);
                Reader = Command.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Reader;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SPName"></param>
        /// <param name="SQLParams"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string SPName, ref SqlParameter[] SQLParams,string ConnString)
        {
            SqlCommand Command = new SqlCommand();
            SqlDataReader Reader = null;
            try
            {
                if (Command.Parameters.Count > 0)
                    Command.Parameters.Clear();

                Command.CommandText = SPName;
                HelpDeskConnection ac = new HelpDeskConnection();
                Command.Connection = ac.GetConnection(ConnString);
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddRange(SQLParams);
                Reader = Command.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Reader;
        }
        /// <summary>
        /// this method retuns the reader by executing sql statement
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string sql)
        {
            SqlCommand Command = new SqlCommand();
            SqlDataReader Reader=null;
            SqlConnection Conn = new SqlConnection(ConnectionString);
            try
            {
                Conn.Open(); 
                Command.CommandText = sql;
                Command.Connection = Conn;
                Command.CommandType = CommandType.Text;

                Reader = Command.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Reader;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string sql,string ConnString)
        {
            SqlCommand Command = new SqlCommand();
            SqlDataReader Reader = null;
            SqlConnection Conn = new SqlConnection(ConnString);
            try
            {
                Conn.Open();
                Command.CommandText = sql;
                Command.Connection = Conn;
                Command.CommandType = CommandType.Text;

                Reader = Command.ExecuteReader(CommandBehavior.CloseConnection );
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Reader;
        }
        #endregion

        #region executeNonQuery
        
        /// <summary>
        /// this method retuns records effected by executing sql statement
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sql)
        {
            SqlCommand Command = new SqlCommand();
            int cnt=0;
            try
            {
                using (SqlConnection Conn = new SqlConnection(ConnectionString))
                {
                    Conn.Open(); 
                    Command.CommandText = sql;
                    HelpDeskConnection ac = new HelpDeskConnection();
                    Command.Connection = Conn;
                    Command.CommandType = CommandType.Text;

                    cnt = Command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cnt;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sql,string ConnString)
        {
            SqlCommand Command = new SqlCommand();
            int cnt = 0;
            try
            {
                using (SqlConnection Conn = new SqlConnection(ConnString))
                {
                    Conn.Open(); 
                    Command.CommandText = sql;
                    HelpDeskConnection ac = new HelpDeskConnection();
                    Command.Connection = Conn;
                    Command.CommandType = CommandType.Text;

                    cnt = Command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cnt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SPName"></param>
        /// <param name="SQLParams"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string SPName, ref SqlParameter[] SQLParams,string ConnString)
        {
            SqlCommand Command = new SqlCommand();
            int cnt = 0;

            try
            {
                if (Command.Parameters.Count > 0)
                    Command.Parameters.Clear();

                Command.CommandText = SPName;
                HelpDeskConnection ac = new HelpDeskConnection();
                Command.Connection = ac.GetConnection(ConnString);
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddRange(SQLParams);
                Command.CommandTimeout = 1800;
                cnt = Command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (Command.Connection != null)
                {
                    if (Command.Connection.State == ConnectionState.Open)
                        Command.Connection.Close();
                }
            }
            return cnt;
        }

       /// <summary>
       ///  this method retuns records effected by executing stored procedure using the conncetion object passed
       /// </summary>
       /// <param name="SqlCon"></param>
       /// <param name="SPName"></param>
       /// <param name="SQLParams"></param>
       /// <returns></returns>
        public static int ExecuteNonQuery(SqlConnection SqlCon, string SPName,ref SqlParameter[] SQLParams)
        {
            SqlCommand Command = new SqlCommand();
            int cnt=0;

            try
            {
                if (Command.Parameters.Count > 0)
                    Command.Parameters.Clear();

                Command.CommandText = SPName;
                Command.Connection = SqlCon;
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddRange(SQLParams);
                cnt = Command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            return cnt;
        }

        public static int ExecuteNonQuery(SqlConnection SqlCon, SqlTransaction SqlTrans,string SPName, ref SqlParameter[] SQLParams)
        {
            SqlCommand Command = new SqlCommand();
            int cnt = 0;

            try
            {
                if (Command.Parameters.Count > 0)
                    Command.Parameters.Clear();

                Command.CommandText = SPName;
                Command.Connection = SqlCon;
                Command.Transaction = SqlTrans;
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddRange(SQLParams);
                cnt = Command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            return cnt;
        }

        /// <summary>
        /// this method retuns records effected by executing sql statement using the conncetion object passed
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(SqlConnection SqlCon,string sql)
        {
            SqlCommand Command = new SqlCommand();
            int cnt = 0;
            try
            {
                Command.CommandText = sql;
                Command.Connection = SqlCon;
                Command.CommandType = CommandType.Text;

                cnt = Command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            return cnt;
        }

        /// <summary>
        /// this method retuns records effected by executing sql statement using the conncetion object, transaction object passed
        /// </summary>
        /// <param name="SqlCon"></param>
        /// <param name="Trans"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(SqlConnection SqlCon, SqlTransaction SqlTrans, string sql)
        {

            SqlCommand Command = new SqlCommand();

            int cnt = 0;

            try
            {
                Command.CommandText = sql;
                Command.Connection = SqlCon;
                Command.CommandType = CommandType.Text;
                Command.Transaction = SqlTrans;
                cnt = Command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cnt;
        }
        #endregion

        #region Execute Scalar
        /// <summary>
        /// it returns the first column of the first record based on the sql statement passed
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string sql)
        {
            SqlCommand Command = new SqlCommand();
            object obj = null;
            try
            {
                using (SqlConnection Conn = new SqlConnection(ConnectionString))
                {
                    Conn.Open(); 
                    Command.CommandText = sql;
                    Command.Connection = Conn;
                    Command.CommandType = CommandType.Text;
                    obj = Command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }
        public static object ExecuteScaler(string spName,string ConnString)
        {
            SqlCommand Command = new SqlCommand();
            object obj = null;

            try
            {
                using (SqlConnection Conn = new SqlConnection(ConnectionString))
                {
                    Command.CommandText = spName;
                    HelpDeskConnection ac = new HelpDeskConnection();
                    Command.Connection = ac.GetConnection(ConnString);
                    Command.CommandType = CommandType.StoredProcedure;
                    obj = Command.ExecuteScalar();
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            return obj;
        
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string sql,string ConnString)
        {
            SqlCommand Command = new SqlCommand();
            object obj = null;
            try
            {
                using (SqlConnection Conn = new SqlConnection(ConnString))
                {
                    Conn.Open(); 
                    Command.CommandText = sql;
                    Command.Connection = Conn;
                    Command.CommandType = CommandType.Text;
                    obj = Command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }

        public static object ExecuteScalar(string sql,SqlConnection Conn)
        {
            SqlCommand Command = new SqlCommand();
            object obj = null;
            try
            {
                    Command.CommandText = sql;
                    Command.Connection = Conn;
                    Command.CommandType = CommandType.Text;
                    obj = Command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }

        public static object ExecuteScalar(string sql, SqlConnection Conn,SqlTransaction Trans)
        {
            SqlCommand Command = new SqlCommand();
            object obj = null;
            try
            {
                Command.CommandText = sql;
                Command.Connection = Conn;
                Command.Transaction = Trans;
                Command.CommandType = CommandType.Text;
                obj = Command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }

        #endregion

        #region Execute Scalar
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SPName"></param>
        /// <param name="SQLParams"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string SPName, ref SqlParameter[] SQLParams,string ConnString)
        {
            SqlCommand Command = new SqlCommand();
            object obj = null;

            try
            {
                if (Command.Parameters.Count > 0)
                    Command.Parameters.Clear();

                Command.CommandText = SPName;
                HelpDeskConnection ac = new HelpDeskConnection();
                Command.Connection = ac.GetConnection(ConnString);
                Command.CommandType = CommandType.StoredProcedure;
                if(SQLParams!=null)
                Command.Parameters.AddRange(SQLParams);
                obj = Command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (Command.Connection.State == ConnectionState.Open)
                    Command.Connection.Close();
            }
            return obj;

        }
        #endregion

        #region get parameter value
        /// <summary>
        /// this will return value from sql parameter array based on the index.
        /// </summary>
        /// <param name="spOut"></param>
        /// <param name="indexVal"></param>
        /// <returns></returns>
        public static object getParamValue(SqlParameter[] spOut, int indexVal)
        {
            object spObj=null;
            try
            {
                spObj= spOut[indexVal].Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return spObj;
        }
        #endregion

        #region Create SQL parameter
        /// <summary>
        /// return SQL parameter 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static SqlParameter CreateParameter(string name, DbType type, object value)
        {
            SqlParameter param = null;
            try
            {
                param = CreateParameter(name, type, value, ParameterDirection.Input);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return param;
        }
        /// <summary>
        /// return SQL parameter
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static SqlParameter CreateParameter(String name, DbType type, object value, ParameterDirection direction)
        {
            SqlParameter param = new SqlParameter();
            try
            {
                param.ParameterName = name;
                param.DbType = type;
                param.Direction = direction;
                param.Value = value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return param;
        }
        #endregion

        #region BulkCopy
        /// <summary>
        ///  copy the bulk data to the database  using new connection
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="destTableName"></param>
        /// <returns></returns>
        public static bool BulkCopy(DataTable dtSource, string destTableName)
        {
            try
            {
                using (SqlConnection Conn = new SqlConnection(ConnectionString))
                {
                    Conn.Open();
                    SqlBulkCopy bulkcopy = new SqlBulkCopy(Conn);
                    // specify the destination table name to store the data into the database
                    bulkcopy.DestinationTableName = destTableName;
                    //source of the data
                    bulkcopy.WriteToServer(dtSource);
                    bulkcopy.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        ///  copy the bulk data to the database using existing connection and transaction
        /// </summary>
        /// <param name="dtSource"></param>
        /// <param name="destTableName"></param>
        /// <param name="Conn"></param>
        /// <param name="Trans"></param>
        /// <returns></returns>
        public static bool BulkCopy(DataTable dtSource, string destTableName, SqlConnection Conn, SqlTransaction Trans)
        {
            try
            {
                SqlBulkCopy bulkcopy = new SqlBulkCopy(Conn, SqlBulkCopyOptions.Default, Trans);
                // specify the destination table name to store the data into the database
                bulkcopy.DestinationTableName = destTableName;
                //source of the data
                bulkcopy.WriteToServer(dtSource);
                bulkcopy.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        #endregion

        private static string ConnectionString
        {
            get
            {
                return (HttpContext.Current.Session["ConString"] == null) ? string.Empty : HttpContext.Current.Session["ConString"].ToString();
            }
        }

    }
}
