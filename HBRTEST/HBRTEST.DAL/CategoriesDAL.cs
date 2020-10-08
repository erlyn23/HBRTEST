using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using HBRTEST.Domain;
using HBRTEST.ErrorHandling;
using HBRTEST.Core.DBUtilities;

namespace HBRTEST.DAL
{
    public class CategoriesDAL
    {
        SqlDataReader sqlDataReader;
        DBConnection dbConnection = DBConnection.DbConnectionInstance();
        Command commandInstance = Command.InstanceCommand();
        public CategoriesDAL()
        {

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
                command.Parameters.Clear();
                sqlDataReader = command.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    CategoryEntity category = new CategoryEntity 
                    {
                        CategoryId = sqlDataReader.GetInt32(0),
                        CategoryName = sqlDataReader.GetString(1),
                        Description = sqlDataReader.GetString(2),
                        CreationDate = DateTime.Parse(sqlDataReader.GetString(3)),
                        LastModificationeDate = DateTime.Parse(sqlDataReader.GetString(4)),
                        Status = sqlDataReader.GetString(5)
                    };
                    lstCategories.Add(category);
                }
                sqlDataReader.Close();
                DBConnection.CloseConnection(sqlConnection);
                return lstCategories;
            }
            catch(Exception exception)
            {
                throw new PersonalizedException(exception.Message);
            }
            finally
            {
                DBConnection.CloseConnection(sqlConnection);
            }
        }

        public CategoryEntity GetCategoryById(int CategoryID)
        {
            SqlConnection sqlConnection = dbConnection.GetDbConnection();
            SqlCommand command = commandInstance.GetSqlCommand();
            CategoryEntity category = null;

            try
            {
                if (CategoryID <= 0)
                {
                    throw new PersonalizedException("El id de la categoría debe ser mayor o igual a 1");
                }
                else
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
                        category = new CategoryEntity
                        {
                            CategoryId = sqlDataReader.GetInt32(0),
                            CategoryName = sqlDataReader.GetString(1),
                            Description = sqlDataReader.GetString(2),
                            CreationDate = DateTime.Parse(sqlDataReader.GetString(3)),
                            LastModificationeDate = DateTime.Parse(sqlDataReader.GetString(4)),
                            Status = sqlDataReader.GetString(5)
                        };
                    }
                    sqlDataReader.Close();
                    DBConnection.CloseConnection(sqlConnection);
                    if (category == null)
                    {
                        throw new PersonalizedException("Categoría no encontrada");
                    }
                    return category;
                }
            }
            catch(Exception exception)
            {
                throw new PersonalizedException(exception.Message);
            }
            finally
            {
                DBConnection.CloseConnection(sqlConnection);
            }
        }
        public void CreateCategory(CategoryEntity category)
        {
            SqlConnection sqlConnection = dbConnection.GetDbConnection();
            SqlCommand command = commandInstance.GetSqlCommand();
            try
            {
                bool isCategoryNameExists = ValidateIfCategoryNameExists(category.CategoryName);

                if (category == null)
                {
                    throw new PersonalizedException("La categoría no puede ser nula o vacía");
                }
                else if (string.IsNullOrEmpty(category.CategoryName))
                {
                    throw new PersonalizedException("Debes insertar una categoría");
                }
                else if (isCategoryNameExists)
                {
                    throw new PersonalizedException("La categoría ya se encuentra registrada en la base de datos");
                }
                else
                {
                    sqlConnection.Open();
                    command.Connection = sqlConnection;
                    command.CommandText = "CreateCategory";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Clear();
                    command.Parameters.Add(new SqlParameter("@CategoryName", category.CategoryName));
                    command.Parameters.Add(new SqlParameter("@Description", category.Description));
                    command.Parameters.Add(new SqlParameter("@CreationDate", DateTime.Today.ToString()));
                    command.Parameters.Add(new SqlParameter("@LastModificationDate", DateTime.Today.ToString()));
                    command.Parameters.Add(new SqlParameter("@Status", "Activo"));
                    command.ExecuteNonQuery();
                    DBConnection.CloseConnection(sqlConnection);
                }
            }
            catch(Exception exception)
            {
                throw new PersonalizedException(exception.Message);
            }
            finally
            {
                DBConnection.CloseConnection(sqlConnection);
            }
        }
       
        private bool ValidateIfCategoryNameExists(string CategoryName)
        {

            SqlConnection sqlConnection = dbConnection.GetDbConnection();
            SqlCommand command = commandInstance.GetSqlCommand();
            try
            {
                if (string.IsNullOrEmpty(CategoryName))
                {
                    throw new PersonalizedException("No se especificó un nombre de categoría");
                }
                else
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
                            DBConnection.CloseConnection(sqlConnection);
                            return true;
                        }
                    }
                    sqlDataReader.Close();
                    DBConnection.CloseConnection(sqlConnection);
                    return false;
                }
            }
            catch(Exception exception)
            {
                throw new PersonalizedException(exception.Message);
            }
            finally
            {
                DBConnection.CloseConnection(sqlConnection);
            }
        }
        public void UpdateCategory(CategoryEntity category)
        {
            SqlConnection sqlConnection = dbConnection.GetDbConnection();
            SqlCommand command = commandInstance.GetSqlCommand();
            try
            {
                if (category == null)
                {
                    throw new PersonalizedException("La categoría no puede ser nula o vacía");
                }
                else if (category.CategoryId <= 0)
                {
                    throw new PersonalizedException("El Id de la categoría debe ser mayor o igual a 1");
                }
                else if (string.IsNullOrEmpty(category.CategoryName))
                {
                    throw new PersonalizedException("Debes ingresar una categoría");
                }
                else
                {
                    sqlConnection.Open();
                    command.Connection = sqlConnection;
                    command.CommandText = "UpdateCategory";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Clear();
                    command.Parameters.Add(new SqlParameter("@CategoryID", category.CategoryId));
                    command.Parameters.Add(new SqlParameter("@CategoryName", category.CategoryName));
                    command.Parameters.Add(new SqlParameter("@Description", category.Description));
                    command.Parameters.Add(new SqlParameter("@LastModificationDate", DateTime.Today.ToString()));
                    command.Parameters.Add(new SqlParameter("@Status", "Activo"));
                    command.ExecuteNonQuery();
                    DBConnection.CloseConnection(sqlConnection);
                }
            }
            catch (Exception exception)
            {
                throw new PersonalizedException(exception.Message);
            }
            finally
            {
                DBConnection.CloseConnection(sqlConnection);
            }
        }

        public void DeleteCategory(int CategoryID)
        {
            SqlConnection sqlConnection = dbConnection.GetDbConnection();
            SqlCommand command = commandInstance.GetSqlCommand();
            try
            {
                if(CategoryID <= 0)
                {
                    throw new PersonalizedException("El id de la categoría debe ser mayor o igual a 1");
                }
                else
                {
                    sqlConnection.Open();
                    command.Connection = sqlConnection;
                    command.CommandText = "DeleteCategory";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Clear();
                    command.Parameters.Add(new SqlParameter("@CategoryID", CategoryID));
                    command.ExecuteNonQuery();
                    DBConnection.CloseConnection(sqlConnection);
                }                    
            }
            catch(Exception exception)
            {
                throw new PersonalizedException(exception.Message);
            }
            finally
            {
                DBConnection.CloseConnection(sqlConnection);
            }
        }
    }
}
