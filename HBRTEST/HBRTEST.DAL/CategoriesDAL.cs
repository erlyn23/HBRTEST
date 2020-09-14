using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using HBRTEST.Entities;

namespace HBRTEST.DAL
{
    public class CategoriesDAL
    {
        SqlDataReader sqlDataReader;
        DBConnection dbConnection = DBConnection.DbConnectionInstance();
        sqlCommand commandInstance = sqlCommand.InstanceCommand();
        public CategoriesDAL()
        {

        }

        private void CloseConnection(SqlConnection connection)
        {
            if (connection.State != System.Data.ConnectionState.Closed)
                connection.Close();
        }
        
        public List<CategoryEntity> GetCategories()
        {
            SqlConnection sqlConnection = dbConnection.GetDbConnection();
            SqlCommand command = commandInstance.GetSqlCommand();
            List<CategoryEntity> lstCategories = new List<CategoryEntity>();
            
            try
            {
                sqlConnection.Open();
                command.Connection = sqlConnection;
                command.CommandText = "GetCategories";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                sqlDataReader = command.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    CategoryEntity category = new CategoryEntity();
                    category.CategoryId = sqlDataReader.GetInt32(0);
                    category.CategoryName = sqlDataReader.GetString(1);
                    category.Description = sqlDataReader.GetString(2);
                    lstCategories.Add(category);
                }
                sqlDataReader.Close();
                CloseConnection(sqlConnection);
                return lstCategories;
            }
            catch
            {
                throw;
            }
            finally
            {
                CloseConnection(sqlConnection);
            }
        }

        public CategoryEntity GetCategoryById(int CategoryID)
        {
            SqlConnection sqlConnection = dbConnection.GetDbConnection();
            SqlCommand command = commandInstance.GetSqlCommand();
            CategoryEntity category = new CategoryEntity();
            
            try
            {
                sqlConnection.Open();
                command.Connection = sqlConnection;
                command.CommandText = "GetCategoryById";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Clear();
                command.Parameters.Add(new SqlParameter("@CategoryID", CategoryID));
                sqlDataReader = command.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    category.CategoryId = sqlDataReader.GetInt32(0);
                    category.CategoryName = sqlDataReader.GetString(1);
                    category.Description = sqlDataReader.GetString(2);
                }
                sqlDataReader.Close();
                CloseConnection(sqlConnection);
                return category;
            }
            catch
            {
                throw;
            }
        }
        public int CreateCategory(CategoryEntity category)
        {
            SqlConnection sqlConnection = dbConnection.GetDbConnection();
            SqlCommand command = commandInstance.GetSqlCommand();
            try
            {
                bool isCategoryNameExists = ValidateIfCategoryNameExists(category.CategoryName);
                if (isCategoryNameExists)
                {
                    return 0;
                }
                sqlConnection.Open();
                command.Connection = sqlConnection;
                command.CommandText = "CreateCategory";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Clear();
                command.Parameters.Add(new SqlParameter("@CategoryName", category.CategoryName));
                command.Parameters.Add(new SqlParameter("@Description", category.Description));
                command.ExecuteNonQuery();
                CloseConnection(sqlConnection);
                return 1;
            }
            catch
            {
                throw;
            }
            finally
            {
                CloseConnection(sqlConnection);
            }
        }
        private bool ValidateIfCategoryNameExists(string CategoryName)
        {

            SqlConnection sqlConnection = dbConnection.GetDbConnection();
            SqlCommand command = commandInstance.GetSqlCommand();
            try
            {
                sqlConnection.Open();
                command.Connection = sqlConnection;
                command.CommandText = "ValidateIfCategoryNameExists";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Clear();
                command.Parameters.Add(new SqlParameter("@CategoryName", CategoryName));
                sqlDataReader = command.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    if (!string.IsNullOrEmpty(sqlDataReader.GetString(0)))
                    {
                        sqlDataReader.Close();
                        CloseConnection(sqlConnection);
                        return true;
                    }
                }
                sqlDataReader.Close();
                CloseConnection(sqlConnection);
                return false;
            }
            catch
            {
                throw;
            }
            finally
            {
                CloseConnection(sqlConnection);
            }
        }
        public int UpdateCategory(CategoryEntity category)
        {
            SqlConnection sqlConnection = dbConnection.GetDbConnection();
            SqlCommand command = commandInstance.GetSqlCommand();
            try
            {
                sqlConnection.Open();
                command.Connection = sqlConnection;
                command.CommandText = "UpdateCategory";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Clear();
                command.Parameters.Add(new SqlParameter("@CategoryID", category.CategoryId));
                command.Parameters.Add(new SqlParameter("@CategoryName", category.CategoryName));
                command.Parameters.Add(new SqlParameter("@Description", category.Description));
                command.ExecuteNonQuery(); 
                CloseConnection(sqlConnection);
                return 1;
            }
            catch
            {
                CloseConnection(sqlConnection);
                throw;
            }
        }

        public bool DeleteCategory(int CategoryID)
        {
            SqlConnection sqlConnection = dbConnection.GetDbConnection();
            SqlCommand command = commandInstance.GetSqlCommand();
            try
            {
                sqlConnection.Open();
                command.Connection = sqlConnection;
                command.CommandText = "DeleteCategory";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Clear();
                command.Parameters.Add(new SqlParameter("@CategoryID", CategoryID));
                command.ExecuteNonQuery();
                CloseConnection(sqlConnection);
                return true;
            }
            catch
            {
                throw;
            }
            finally
            {
                CloseConnection(sqlConnection);
            }

        }
    }
}
