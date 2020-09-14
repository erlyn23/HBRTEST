using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HBRTEST.DAL;
using HBRTEST.Entities;

namespace HBRTEST.UnitTest
{
    [TestClass]
    public class ProductsTest
    {
        [TestMethod]
        public void GetProductsTestMethod()
        {
            //Arrange
            int productWantedID = 3;
            int productWantedCategoryID = 1004;
            string productWantedName = "Tenis Nike";
            string productWantedCategoryName = "Calzados";
            string productWantedDescription = "Son tenis modernos";
            int productWantedExistence = 20;
            float productWantedPrice = 2000;
            DateTime productWantedCreationDate = DateTime.Parse("13/09/2020");
            DateTime productWantedExpireDate = DateTime.Parse("13/09/2035");

            ProductsDAL _productsRepository = new ProductsDAL();
            //Act
            List<ProductEntity> lstProductsTestResult = _productsRepository.GetProducts();
            //Assert
            Assert.AreEqual(productWantedID, lstProductsTestResult[0].ProductId);
            Assert.AreEqual(productWantedCategoryID, lstProductsTestResult[0].CategoryId);
            Assert.AreEqual(productWantedName, lstProductsTestResult[0].ProductName);
            Assert.AreEqual(productWantedCategoryName, lstProductsTestResult[0].CategoryName);
            Assert.AreEqual(productWantedDescription, lstProductsTestResult[0].Description);
            Assert.AreEqual(productWantedExistence, lstProductsTestResult[0].Existence);
            Assert.AreEqual(productWantedPrice, lstProductsTestResult[0].Price);
            Assert.AreEqual(productWantedCreationDate, lstProductsTestResult[0].Creation_Date);
            Assert.AreEqual(productWantedExpireDate, lstProductsTestResult[0].Expire_Date);
        }

        [TestMethod]
        public void GetProductByIdTestMethod()
        {
            //Arrange
            int productWantedID = 3;
            int productWantedCategoryID = 1004;
            string productWantedName = "Tenis Nike";
            string productWantedCategoryName = "Calzados";
            string productWantedDescription = "Son tenis modernos";
            int productWantedExistence = 20;
            float productWantedPrice = 2000;
            DateTime productWantedCreationDate = DateTime.Parse("13/09/2020");
            DateTime productWantedExpireDate = DateTime.Parse("13/09/2035");

            ProductsDAL _productsRepository = new ProductsDAL();
            //Act
            ProductEntity productTestResult = _productsRepository.GetProductById(3);
            //Assert
            Assert.AreEqual(productWantedID, productTestResult.ProductId);
            Assert.AreEqual(productWantedCategoryID, productTestResult.CategoryId);
            Assert.AreEqual(productWantedName, productTestResult.ProductName);
            Assert.AreEqual(productWantedCategoryName, productTestResult.CategoryName);
            Assert.AreEqual(productWantedDescription, productTestResult.Description);
            Assert.AreEqual(productWantedExistence, productTestResult.Existence);
            Assert.AreEqual(productWantedPrice, productTestResult.Price);
            Assert.AreEqual(productWantedCreationDate, productTestResult.Creation_Date);
            Assert.AreEqual(productWantedExpireDate, productTestResult.Expire_Date);
        }

        [TestMethod]
        public void CreateProductTestMethod()
        {
            //Arrange
            int createResultsWanted = 1;
            ProductEntity product = new ProductEntity();
            product.CategoryId = 1004;
            product.ProductName = "Tenis Nike";
            product.Description = "Son tenis modernos";
            product.Existence = 20;
            product.Price = 2000;
            product.Creation_Date = DateTime.Parse("13/09/2020");
            product.Expire_Date = DateTime.Parse("13/09/2035");
            ProductsDAL _productsRepository = new ProductsDAL();
            //Act
            int createTestResults = _productsRepository.CreateProduct(product);
            //Assert
            Assert.AreEqual(createResultsWanted, createTestResults);
        }

        [TestMethod]
        public void UpdateProductTestMethod()
        {
            //Arrange
            int updateResultsWanted = 1;
            ProductEntity product = new ProductEntity();
            product.ProductId = 3;
            product.CategoryId = 1004;
            product.ProductName = "Tenis Nike";
            product.Description = "Son tenis modernos";
            product.Existence = 20;
            product.Price = 2000;
            product.Creation_Date = DateTime.Parse("13/09/2020");
            product.Expire_Date = DateTime.Parse("13/09/2035");

            ProductsDAL _productsRepository = new ProductsDAL();
            //Act
            int updateTestResults = _productsRepository.UpdateProduct(product);
            //Assert
            Assert.AreEqual(updateResultsWanted, updateTestResults);
        }

        [TestMethod]
        public void DeleteProductTestMethod()
        {
            //Arrange
            bool deleteResultsWanted = true;
            ProductsDAL _productRepository = new ProductsDAL();

            //Act
            bool deleteTestResults = _productRepository.DeleteProduct(3);
            //Assert
            Assert.AreEqual(deleteResultsWanted, deleteTestResults);
        }
    }
}
