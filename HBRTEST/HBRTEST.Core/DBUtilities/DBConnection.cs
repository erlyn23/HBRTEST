using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using HBRTEST.ErrorHandling;

namespace HBRTEST.Core.DBUtilities
{
    public class DBConnection
    {
        private static DBConnection dbConnection;
        private static readonly string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HBRTEST;Integrated Security=True;";
        private static SqlConnection sqlConnection = new SqlConnection(connectionString);
        private DBConnection()
        {

        }

        public static DBConnection DbConnectionInstance()
        {
            if (dbConnection == null)
            {
                dbConnection = new DBConnection();
                return dbConnection;
            }
            return dbConnection;
        }
        public SqlConnection GetDbConnection()
        {
            return sqlConnection;
        }

        public static void CloseConnection(SqlConnection connection)
        {
            try
            {
                if (sqlConnection != null && sqlConnection.State == System.Data.ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
            catch (Exception exception)
            {
                throw new PersonalizedException(exception.Message);
            }
        }
    }
}
