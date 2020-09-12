using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace HBRTEST.DAL
{
    class DBConnection
    {
        #region Definiciones
        private static SqlConnection sqlConnection = null;
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        #endregion

        #region Constructor
        private DBConnection()
        {

        }
        #endregion

        #region Métodos
        #region Métodos Públicos
        public static SqlConnection DbInstance()
        {
            if(sqlConnection == null && string.IsNullOrEmpty(sqlConnection.ConnectionString))
            {
                sqlConnection = new SqlConnection();
                sqlConnection.ConnectionString = connectionString;
                return sqlConnection;
            }
            return sqlConnection;
        }
        #endregion
        #endregion
    }
}
