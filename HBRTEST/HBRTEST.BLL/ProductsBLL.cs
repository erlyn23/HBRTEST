using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using HBRTEST.DAL;
using HBRTEST.Entities;

namespace HBRTEST.BLL
{
    public class ProductsBLL
    {
        private ProductsDAL _productsRepository = new ProductsDAL();
        public ProductsBLL()
        {

        }
        public List<ProductEntity> GetProducts()
        {
            List<ProductEntity> lstProducts = _productsRepository.GetProducts();
            return lstProducts;
        }

        public ProductEntity GetProductById(int ProductID)
        {
            ProductEntity product = _productsRepository.GetProductById(ProductID);
            return product; 
        }

        public List<ProductEntity> FilterProductsByCategoryName(string CategoryName)
        {
            List<ProductEntity> filteredProducts = _productsRepository.FilterProductsByCategoryName(CategoryName);
            return filteredProducts;
        }

        public List<ProductEntity> FilterProductsByProductName(string ProductName)
        {
            List<ProductEntity> filteredProducts = _productsRepository.FilterProductByProductName(ProductName);
            return filteredProducts;
        }

        public void CreateProduct(ProductEntity product)
        {
            _productsRepository.CreateProduct(product);
        }

        public void UpdateProduct(ProductEntity product)
        {
           _productsRepository.UpdateProduct(product);
        }

        public void DeleteProduct(int ProductID)
        {
            _productsRepository.DeleteProduct(ProductID);
        }
    }
}
