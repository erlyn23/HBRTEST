using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Linq;
using HBRTEST.Entities;

namespace HBRTEST.DAL
{
    class ProductsDAL
    {
        #region Definiciones
        SqlDataReader sqlDataReader;
        #endregion
        #region Constructor
        public ProductsDAL()
        {

        }
        #endregion
        #region Métodos
        #region Métodos públicos
        public List<ProductEntity> GetProducts()
        {
            #region Definiciones
            SqlConnection sqlConnection = DBConnection.DbInstance();
            SqlCommand command = sqlCommand.InstanceCommand();
            List<ProductEntity> lstProducts = new List<ProductEntity>();
            #endregion
            #region Proceso
            try
            {
                sqlConnection.Open();
                command.CommandText = "exec GetProducts";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                sqlDataReader = command.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    ProductEntity product = new ProductEntity();
                    product.ProductId = sqlDataReader.GetInt32(0);
                    product.CategoryId = sqlDataReader.GetInt32(1);
                    product.ProductName = sqlDataReader.GetString(3);
                    product.CategoryName = sqlDataReader.GetString(4);
                    product.Description = sqlDataReader.GetString(5);
                    product.Existence = sqlDataReader.GetInt32(6);
                    product.Price = sqlDataReader.GetFloat(7);
                    product.Creation_Date = sqlDataReader.GetDateTime(8);
                    product.Expire_Date = sqlDataReader.GetDateTime(9);
                    lstProducts.Add(product);
                }
                if(lstProducts.Count > 0)
                {
                    sqlDataReader.Close();
                    sqlConnection.Close();
                    return lstProducts;
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

        public ProductEntity GetProductById(int ProductID)
        {
            #region Definiciones
            SqlConnection sqlConnection = DBConnection.DbInstance();
            SqlCommand command = sqlCommand.InstanceCommand();
            ProductEntity product = new ProductEntity();
            #endregion
            #region Proceso
            try
            {
                sqlConnection.Open();
                command.CommandText = "exec GetProductById";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@ProductID", ProductID));
                sqlDataReader = command.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    product.ProductId = sqlDataReader.GetInt32(0);
                    product.CategoryId = sqlDataReader.GetInt32(1);
                    product.ProductName = sqlDataReader.GetString(3);
                    product.CategoryName = sqlDataReader.GetString(4);
                    product.Description = sqlDataReader.GetString(5);
                    product.Existence = sqlDataReader.GetInt32(6);
                    product.Price = sqlDataReader.GetFloat(7);
                    product.Creation_Date = sqlDataReader.GetDateTime(8);
                    product.Expire_Date = sqlDataReader.GetDateTime(9);
                }
                if (product != null)
                {
                    sqlDataReader.Close();
                    sqlConnection.Close();
                    return product;
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

        public bool CreateProduct(ProductEntity product)
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
                command.CommandText = "exec CreateProduct";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@ProductID",product.ProductId));
                command.Parameters.Add(new SqlParameter("@CategoryID",product.CategoryId));
                command.Parameters.Add(new SqlParameter("@ProductName",product.ProductName));
                command.Parameters.Add(new SqlParameter("@Description", product.Description));
                command.Parameters.Add(new SqlParameter("@Existence",product.Existence));
                command.Parameters.Add(new SqlParameter("@Price",product.Price));
                command.Parameters.Add(new SqlParameter("@Creation_Date",product.Creation_Date));
                command.Parameters.Add(new SqlParameter("@Expire_Date",product.Expire_Date));
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

        public bool UpdateProduct(ProductEntity product)
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
                command.CommandText = "exec UpdateProduct";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@ProductID", product.ProductId));
                command.Parameters.Add(new SqlParameter("@CategoryID", product.CategoryId));
                command.Parameters.Add(new SqlParameter("@ProductName", product.ProductName));
                command.Parameters.Add(new SqlParameter("@Description", product.Description));
                command.Parameters.Add(new SqlParameter("@Existence", product.Existence));
                command.Parameters.Add(new SqlParameter("@Price", product.Price));
                command.Parameters.Add(new SqlParameter("@Creation_Date", product.Creation_Date));
                command.Parameters.Add(new SqlParameter("@Expire_Date", product.Expire_Date));
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
        public bool DeleteProduct(int ProductID)
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
                command.CommandText = "exec DeleteProduct";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@ProductID", ProductID));
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
        #endregion
    }
}
