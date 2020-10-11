using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Linq;
using HBRTEST.Domain;
using System.ComponentModel;
using HBRTEST.ErrorHandling;
using HBRTEST.Core.DBUtilities;

namespace HBRTEST.DAL
{
    public class ProductsDAL
    {
        SqlDataReader sqlDataReader;
        DBConnection dbConnection = DBConnection.DbConnectionInstance();
        Command commandInstance = Command.InstanceCommand();
        public ProductsDAL()
        {

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
                command.Parameters.Clear();
                sqlDataReader = command.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    ProductEntity product = new ProductEntity
                    {
                        ProductId = sqlDataReader.GetInt32(0),
                        CategoryId = sqlDataReader.GetInt32(1),
                        ProductName = sqlDataReader.GetString(2),
                        CategoryName = sqlDataReader.GetString(3),
                        ProductImage = sqlDataReader.GetString(4),
                        Description = sqlDataReader.GetString(5),
                        Existence = sqlDataReader.GetInt32(6),
                        Price = float.Parse(sqlDataReader.GetDecimal(7).ToString()),
                        CreationDate = sqlDataReader.GetDateTime(8),
                        LastModificationDate = sqlDataReader.GetDateTime(9),
                        Active = sqlDataReader.GetBoolean(10)
                    };
                    lstProducts.Add(product);
                }
                sqlDataReader.Close();
                DBConnection.CloseConnection(sqlConnection);
                return lstProducts;
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
        public ProductEntity GetProductById(int ProductID)
        {
            SqlConnection sqlConnection = dbConnection.GetDbConnection();
            SqlCommand command = commandInstance.GetSqlCommand();
            ProductEntity product = null;
            try
            {
                if(ProductID <= 0)
                {
                    throw new PersonalizedException("El id del producto debe ser mayor o igual a 1");
                }
                else
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
                        product = new ProductEntity
                        {
                            ProductId = sqlDataReader.GetInt32(0),
                            CategoryId = sqlDataReader.GetInt32(1),
                            ProductName = sqlDataReader.GetString(2),
                            CategoryName = sqlDataReader.GetString(3),
                            ProductImage = sqlDataReader.GetString(4),
                            Description = sqlDataReader.GetString(5),
                            Existence = sqlDataReader.GetInt32(6),
                            Price = float.Parse(sqlDataReader.GetDecimal(7).ToString()),
                            CreationDate = sqlDataReader.GetDateTime(8),
                            LastModificationDate = sqlDataReader.GetDateTime(9),
                            Active = sqlDataReader.GetBoolean(10)
                        };
                    }
                    sqlDataReader.Close();
                    DBConnection.CloseConnection(sqlConnection);
                    if (product == null)
                    {
                        throw new PersonalizedException("No se pudo encontrar el producto");
                    }
                    return product;
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

        public List<ProductEntity> FilterProducts(int categoryId, string productName)
        {
            var lstProducts = GetProducts();
            if (!string.IsNullOrEmpty(productName) && categoryId > 0)
            {
                var filteredProducts = from products in lstProducts where products.CategoryId == categoryId 
                                       && products.ProductName.ToLower().Contains(productName.ToLower().Trim())
                                       && products.Active
                                       select products;
                
                return filteredProducts.ToList();
            }
            else if(categoryId > 0 && string.IsNullOrEmpty(productName))
            {
                var filteredProducts = from products in lstProducts where
                                       products.CategoryId == categoryId && products.Active 
                                       select products;
                return filteredProducts.ToList();
            }
            else if(categoryId <= 0 && !string.IsNullOrEmpty(productName))
            {
                var filteredProducts = from products in lstProducts where 
                                       products.ProductName.ToLower().Contains(productName.ToLower().Trim())
                                       select products;
                return filteredProducts.ToList();
            }
            return lstProducts.Where(products => products.Active).ToList();
        }

        public void CreateProduct(ProductEntity product)
        {
            SqlConnection sqlConnection = dbConnection.GetDbConnection();
            SqlCommand command = commandInstance.GetSqlCommand();
            try
            {
                if(product == null)
                {
                    throw new PersonalizedException("El producto no puede ser nulo o vacío");
                }
                else if(product.CategoryId <= 0 || string.IsNullOrEmpty(product.ProductName))
                {
                    throw new PersonalizedException("Debes escoger una categoría y escribir el nombre del producto");
                }
                else
                {
                    sqlConnection.Open();
                    command.Connection = sqlConnection;
                    command.CommandText = "CreateProduct";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Clear();
                    command.Parameters.Add(new SqlParameter("@CategoryID", product.CategoryId));
                    command.Parameters.Add(new SqlParameter("@ProductName", product.ProductName));
                    if (string.IsNullOrEmpty(product.ProductImage))
                    {
                        command.Parameters.Add(new SqlParameter("@ProductImage", ""));
                    }
                    else
                    {
                        command.Parameters.Add(new SqlParameter("@ProductImage", product.ProductImage));
                    }
                    if (string.IsNullOrEmpty(product.Description))
                    {
                        command.Parameters.Add(new SqlParameter("@Description", ""));
                    }
                    else
                    {
                        command.Parameters.Add(new SqlParameter("@Description", product.Description));
                    }
                    command.Parameters.Add(new SqlParameter("@Existence", product.Existence));
                    command.Parameters.Add(new SqlParameter("@Price", product.Price));
                    command.Parameters.Add(new SqlParameter("@CreationDate", DateTime.Today));
                    command.Parameters.Add(new SqlParameter("@LastModificationDate", DateTime.Today));
                    command.Parameters.Add(new SqlParameter("@Active", product.Active));
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

        public void UpdateProduct(ProductEntity product)
        {
            SqlConnection sqlConnection = dbConnection.GetDbConnection();
            SqlCommand command = commandInstance.GetSqlCommand();

            try
            {
                if(product.ProductId <= 0)
                {
                    throw new PersonalizedException("El id del producto debe ser mayor a 0");
                }
                else if(product == null)
                {
                    throw new PersonalizedException("El producto no puede ser nulo o vacío");
                }
                else if(product.CategoryId <= 0 || string.IsNullOrEmpty(product.ProductName))
                {
                    throw new PersonalizedException("Debes escoger una categoría y escribir el nombre del producto");
                }
                else
                {
                    sqlConnection.Open();
                    command.Connection = sqlConnection;
                    command.CommandText = "UpdateProduct";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Clear();
                    command.Parameters.Add(new SqlParameter("@ProductID", product.ProductId));
                    command.Parameters.Add(new SqlParameter("@CategoryID", product.CategoryId));
                    command.Parameters.Add(new SqlParameter("@ProductName", product.ProductName));
                    if (string.IsNullOrEmpty(product.Description))
                    {
                        command.Parameters.Add(new SqlParameter("@Description", ""));
                    }
                    else
                    {
                        command.Parameters.Add(new SqlParameter("@Description", product.Description));
                    }
                    command.Parameters.Add(new SqlParameter("@Existence", product.Existence));
                    command.Parameters.Add(new SqlParameter("@Price", product.Price));
                    command.Parameters.Add(new SqlParameter("@LastModificationDate", DateTime.Today));
                    command.Parameters.Add(new SqlParameter("@Active", product.Active));
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
        public void DeleteProduct(int ProductID)
        {
            SqlConnection sqlConnection = dbConnection.GetDbConnection();
            SqlCommand command = commandInstance.GetSqlCommand();
            try
            {
                if(ProductID <= 0)
                {
                    throw new PersonalizedException("El id del producto debe ser mayor o igual a 1");
                }
                else
                {
                    sqlConnection.Open();
                    command.Connection = sqlConnection;
                    command.CommandText = "DeleteProduct";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Clear();
                    command.Parameters.Add(new SqlParameter("@ProductID", ProductID));
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
