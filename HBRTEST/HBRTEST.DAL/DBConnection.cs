using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace HBRTEST.DAL
{
    class DBConnection
    {
        
        private static DBConnection dbConnection;
        private static readonly string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HBRTEST;Integrated Security=True;";
        private static SqlConnection sqlConnection = new SqlConnection(connectionString);
        private DBConnection()
        {

        }

        public static DBConnection DbConnectionInstance()
        {
            if(dbConnection == null)
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
    }
}
