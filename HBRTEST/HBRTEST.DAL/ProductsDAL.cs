using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Linq;
using HBRTEST.Entities;
using System.ComponentModel;

namespace HBRTEST.DAL
{
    public class ProductsDAL
    {
        SqlDataReader sqlDataReader;
        DBConnection dbConnection = DBConnection.DbConnectionInstance();
        sqlCommand commandInstance = sqlCommand.InstanceCommand();
        public ProductsDAL()
        {

        }

        private void CloseConnection(SqlConnection connection)
        {
            if (connection.State != System.Data.ConnectionState.Closed)
                connection.Close();
        }
        public List<ProductEntity> GetProducts()
        {
            SqlConnection sqlConnection = dbConnection.GetDbConnection();
            SqlCommand command = commandInstance.GetSqlCommand();
            List<ProductEntity> lstProducts = new List<ProductEntity>();
            try
            {
                sqlConnection.Open();
                command.Connection = sqlConnection;
                command.CommandText = "GetProducts";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                sqlDataReader = command.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    ProductEntity product = new ProductEntity();
                    product.ProductId = sqlDataReader.GetInt32(0);
                    product.CategoryId = sqlDataReader.GetInt32(1);
                    product.ProductName = sqlDataReader.GetString(2);
                    product.CategoryName = sqlDataReader.GetString(3);
                    product.Description = sqlDataReader.GetString(4);
                    product.Existence = sqlDataReader.GetInt32(5);
                    product.Price = float.Parse(sqlDataReader.GetDecimal(6).ToString());
                    product.Creation_Date = sqlDataReader.GetDateTime(7);
                    product.Expire_Date = sqlDataReader.GetDateTime(8);
                    lstProducts.Add(product);
                }
                sqlDataReader.Close();
                CloseConnection(sqlConnection);
                return lstProducts;
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
        public ProductEntity GetProductById(int ProductID)
        {
            SqlConnection sqlConnection = dbConnection.GetDbConnection();
            SqlCommand command = commandInstance.GetSqlCommand();
            ProductEntity product = new ProductEntity();
            try
            {
                sqlConnection.Open();
                command.Connection = sqlConnection;
                command.CommandText = "GetProductById";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Clear();
                command.Parameters.Add(new SqlParameter("@ProductID", ProductID));
                sqlDataReader = command.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    product.ProductId = sqlDataReader.GetInt32(0);
                    product.CategoryId = sqlDataReader.GetInt32(1);
                    product.ProductName = sqlDataReader.GetString(2);
                    product.CategoryName = sqlDataReader.GetString(3);
                    product.Description = sqlDataReader.GetString(4);
                    product.Existence = sqlDataReader.GetInt32(5);
                    product.Price = float.Parse(sqlDataReader.GetDecimal(6).ToString());
                    product.Creation_Date = sqlDataReader.GetDateTime(7);
                    product.Expire_Date = sqlDataReader.GetDateTime(8);
                }
                sqlDataReader.Close();
                CloseConnection(sqlConnection);
                return product;
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

        public List<ProductEntity> FilterProductsByCategoryName(string CategoryName)
        {
            var lstProducts = GetProducts();
            var filteredProducts = from products in lstProducts where products.CategoryName.ToLower().Contains(CategoryName.ToLower()) select products;
            return filteredProducts.ToList();
        }

        public List<ProductEntity> FilterProductByProductName(string ProductName)
        {
            var lstProducts = GetProducts();
            var filteredProducts = from products in lstProducts where products.ProductName.ToLower().Contains(ProductName.ToLower()) select products;
            return filteredProducts.ToList();
        }

        public int CreateProduct(ProductEntity product)
        {
            SqlConnection sqlConnection = dbConnection.GetDbConnection();
            SqlCommand command = commandInstance.GetSqlCommand();
            try
            {
                sqlConnection.Open();
                command.Connection = sqlConnection;
                command.CommandText = "CreateProduct";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Clear();
                command.Parameters.Add(new SqlParameter("@CategoryID",product.CategoryId));
                command.Parameters.Add(new SqlParameter("@ProductName",product.ProductName));
                command.Parameters.Add(new SqlParameter("@Description", product.Description));
                command.Parameters.Add(new SqlParameter("@Existence",product.Existence));
                command.Parameters.Add(new SqlParameter("@Price",product.Price));
                command.Parameters.Add(new SqlParameter("@Creation_Date",product.Creation_Date));
                command.Parameters.Add(new SqlParameter("@Expire_Date",product.Expire_Date));
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

        public int UpdateProduct(ProductEntity product)
        {
            SqlConnection sqlConnection = dbConnection.GetDbConnection();
            SqlCommand command = commandInstance.GetSqlCommand();

            try
            {
                sqlConnection.Open();
                command.Connection = sqlConnection;
                command.CommandText = "UpdateProduct";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Clear();
                command.Parameters.Add(new SqlParameter("@ProductID", product.ProductId));
                command.Parameters.Add(new SqlParameter("@CategoryID", product.CategoryId));
                command.Parameters.Add(new SqlParameter("@ProductName", product.ProductName));
                command.Parameters.Add(new SqlParameter("@Description", product.Description));
                command.Parameters.Add(new SqlParameter("@Existence", product.Existence));
                command.Parameters.Add(new SqlParameter("@Price", product.Price));
                command.Parameters.Add(new SqlParameter("@Creation_Date", product.Creation_Date));
                command.Parameters.Add(new SqlParameter("@Expire_Date", product.Expire_Date));
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
        public bool DeleteProduct(int ProductID)
        {
            SqlConnection sqlConnection = dbConnection.GetDbConnection();
            SqlCommand command = commandInstance.GetSqlCommand();
            try
            {
                sqlConnection.Open();
                command.Connection = sqlConnection;
                command.CommandText = "DeleteProduct";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Clear();
                command.Parameters.Add(new SqlParameter("@ProductID", ProductID));
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
