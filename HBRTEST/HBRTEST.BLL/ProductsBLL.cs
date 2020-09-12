using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using HBRTEST.DAL;
using HBRTEST.Entities;

namespace HBRTEST.BLL
{
    public class ProductsBLL: IDisposable
    {
        #region Definiciones
        private Component components = new Component();
        private bool _disposed = false;
        #endregion
        #region Constructor
        public ProductsBLL()
        {

        }
        #endregion

        #region Métodos
        #region Métodos públicos
        public List<ProductEntity> GetProducts()
        {
            try
            {
                using(ProductsDAL _productsRepository = new ProductsDAL())
                {
                    List<ProductEntity> lstProducts = _productsRepository.GetProducts();
                    if(lstProducts != null)
                    {
                        return lstProducts;
                    }
                    return null;
                }
            }
            catch
            {
                throw new Exception();
            }
        }

        public List<ProductEntity> FilterProductsByCategoryName(string CategoryName)
        {
            try
            {
                using (ProductsDAL _productsRepository = new ProductsDAL())
                {
                    List<ProductEntity> filteredProducts = _productsRepository.FilterProductsByCategoryName(CategoryName);
                    if(filteredProducts != null)
                    {
                        return filteredProducts;
                    }
                    return null;
                }
            }
            catch
            {
                throw new Exception();
            }
        }

        public List<ProductEntity> FilterProductsByProductName(string ProductName)
        {
            try
            {
                using (ProductsDAL _productsRepository = new ProductsDAL())
                {
                    List<ProductEntity> filteredProducts = _productsRepository.FilterProductByProductName(ProductName);
                    if(filteredProducts != null)
                    {
                        return filteredProducts;
                    }
                    return null;
                }
            }
            catch
            {
                throw new Exception();
            }
        }

        public bool CreateProduct(ProductEntity product)
        {
            try
            {
                using (ProductsDAL _productsRepository = new ProductsDAL())
                {
                    bool isProductCreated = _productsRepository.CreateProduct(product);
                    return isProductCreated;
                }
            }
            catch
            {
                throw new Exception();
            }
        }

        public bool UpdateProduct(ProductEntity product)
        {
            try
            {
                using (ProductsDAL _productsRepository = new ProductsDAL())
                {
                    bool isProductUpdated = _productsRepository.UpdateProduct(product);
                    return isProductUpdated;
                }
            }
            catch
            {
                throw new Exception();
            }
        }

        public bool DeleteProduct(int ProductID)
        {
            try
            {
                using (ProductsDAL _productsRepository = new ProductsDAL())
                {
                    bool isProductDeleted = _productsRepository.DeleteProduct(ProductID);
                    return isProductDeleted;
                }
            }
            catch
            {
                throw new Exception();
            }
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
        ~ProductsBLL() => Dispose(false);
        #endregion
    }
}
