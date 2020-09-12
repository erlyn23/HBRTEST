using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using HBRTEST.Entities;
using System.ComponentModel;

namespace HBRTEST.DAL
{
    public class CategoriesDAL: IDisposable
    {
        #region Definiciones
        SqlDataReader sqlDataReader;
        private Component components = new Component();
        private bool _disposed = false;
        #endregion
        #region Constructor
        public CategoriesDAL()
        {

        }
        #endregion
        #region Métodos
        #region Métodos Públicos
        public List<CategoryEntity> GetCategories()
        {
            #region Definiciones
            SqlConnection sqlConnection = DBConnection.DbInstance();
            SqlCommand command = sqlCommand.InstanceCommand();
            List<CategoryEntity> lstCategories = new List<CategoryEntity>();
            #endregion
            #region Proceso
            try
            {
                sqlConnection.Open();
                command.CommandText = "exec GetCategories";
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
                if(lstCategories != null)
                {
                    sqlDataReader.Close();
                    sqlConnection.Close();
                    return lstCategories;
                }
                sqlDataReader.Close();
                sqlConnection.Close();
                return null;
            }
            catch
            {
                sqlConnection.Close();
                throw new Exception();
            }
            #endregion
        }

        public CategoryEntity GetCategoryById(int CategoryID)
        {
            #region Definiciones
            SqlConnection sqlConnection = DBConnection.DbInstance();
            SqlCommand command = sqlCommand.InstanceCommand();
            CategoryEntity category = new CategoryEntity();
            #endregion
            #region Proceso
            try
            {
                sqlConnection.Open();
                command.CommandText = "exec GetCategoryById";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@CategoryID", CategoryID));
                sqlDataReader = command.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    category.CategoryId = sqlDataReader.GetInt32(0);
                    category.CategoryName = sqlDataReader.GetString(1);
                    category.Description = sqlDataReader.GetString(2);
                }
                if (category != null)
                {
                    sqlDataReader.Close();
                    sqlConnection.Close();
                    return category;
                }
                sqlDataReader.Close();
                sqlConnection.Close();
                return null;
            }
            catch
            {
                sqlConnection.Close();
                throw new Exception();
            }
            #endregion
        }
        public string CreateCategory(CategoryEntity category)
        {
            #region Definiciones
            SqlConnection sqlConnection = DBConnection.DbInstance();
            SqlCommand command = sqlCommand.InstanceCommand();
            #endregion
            #region Proceso
            try
            {
                bool isCategoryNameExists = ValidateIfCategoryNameExists(category.CategoryName);
                if (isCategoryNameExists)
                {
                    return "La categoría ya existe, intente con una nueva";
                }
                sqlConnection.Open();
                command.Connection = sqlConnection;
                command.CommandText = "exec CreateCategory";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@CategoryName", category.CategoryName));
                command.Parameters.Add(new SqlParameter("@Description", category.Description));
                command.ExecuteNonQuery();
                sqlConnection.Close();
                return "Categoría creada correctamente";
            }
            catch
            {
                sqlConnection.Close();
                throw new Exception();
            }
            #endregion
        }
        public bool UpdateCategory(CategoryEntity category)
        {
            #region Definiciones
            SqlConnection sqlConnection = DBConnection.DbInstance();
            SqlCommand command = sqlCommand.InstanceCommand();
            #endregion
            #region Proceso
            try
            {
                sqlConnection.Open();
                command.Connection = sqlConnection;
                command.CommandText = "exec UpdateCategory";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@CategoryID", category.CategoryId));
                command.Parameters.Add(new SqlParameter("@CategoryName", category.CategoryName));
                command.Parameters.Add(new SqlParameter("@Description", category.Description));
                command.ExecuteNonQuery();
                sqlConnection.Close();
                return true;
            }
            catch
            {
                sqlConnection.Close();
                throw new Exception();
            }
            #endregion
        }

        public bool DeleteCategory(int CategoryID)
        {
            #region Definiciones
            SqlConnection sqlConnection = DBConnection.DbInstance();
            SqlCommand command = sqlCommand.InstanceCommand();
            #endregion
            #region Proceso
            try
            {
                sqlConnection.Open();
                command.Connection = sqlConnection;
                command.CommandText = "exec DeleteCategory";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@CategoryID", CategoryID));
                command.ExecuteNonQuery();
                sqlConnection.Close();
                return true;
            }
            catch
            {
                sqlConnection.Close();
                throw new Exception();
            }
            #endregion

        }
        #endregion
        #region Métodos Privados
        private bool ValidateIfCategoryNameExists(string CategoryName)
        {

            #region Definiciones
            SqlConnection sqlConnection = DBConnection.DbInstance();
            SqlCommand command = sqlCommand.InstanceCommand();
            #endregion
            #region Proceso
            try 
            {
                sqlConnection.Open();
                command.Connection = sqlConnection;
                command.CommandText = "exec ValidateIfCategoryNameExists";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@CategoryName", CategoryName));
                sqlDataReader = command.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    if (!string.IsNullOrEmpty(sqlDataReader.GetString(0)))
                    {
                        sqlDataReader.Close();
                        sqlConnection.Close();
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                sqlDataReader.Close();
                sqlConnection.Close();
                throw new Exception();
            }
            #endregion
        }
        #endregion
        #endregion
        #region Destructor
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    this.components.Dispose();
                    this.components = null;
                }
            }
            this._disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~CategoriesDAL() => Dispose(false);
        #endregion
    }
}
