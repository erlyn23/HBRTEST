using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using HBRTEST.DAL;
using HBRTEST.Domain;
using HBRTEST.Core.Interfaces;

namespace HBRTEST.BLL
{
    public class ProductsBLL: IRepository<ProductEntity>
    {
        private ProductsDAL _productsRepository = new ProductsDAL();
        public ProductsBLL()
        {

        }
        public List<ProductEntity> GetAll()
        {
            List<ProductEntity> lstProducts = _productsRepository.GetProducts();
            return lstProducts;
        }

        public ProductEntity GetEntityById(int ProductID)
        {
            ProductEntity product = _productsRepository.GetProductById(ProductID);
            return product; 
        }

        public List<ProductEntity> FilterProductsByCategoryName(int categoryId)
        {
            List<ProductEntity> filteredProducts = _productsRepository.FilterProductsByCategoryName(categoryId);
            return filteredProducts;
        }

        public List<ProductEntity> FilterProductsByProductName(string ProductName)
        {
            List<ProductEntity> filteredProducts = _productsRepository.FilterProductByProductName(ProductName);
            return filteredProducts;
        }

        public void Add(ProductEntity product)
        {
            _productsRepository.CreateProduct(product);
        }


        public void Update(ProductEntity product)
        {
           _productsRepository.UpdateProduct(product);
        }

        public void Delete(int ProductID)
        {
            _productsRepository.DeleteProduct(ProductID);
        }
    }
}
