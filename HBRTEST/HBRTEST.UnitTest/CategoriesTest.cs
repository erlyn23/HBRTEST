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
    public class CategoriesTest
    {

        [TestMethod]
        public void GetCategoriesTestMethod()
        {
            //Arrange
            int categoryWantedID = 2;
            string categoryWantedName = "Calzados";
            string categoryWantedDescription = "Son accesorios para los pies";

            CategoriesDAL _categoriesRepository = new CategoriesDAL();
            //Act
            List<CategoryEntity> categoryResultsTest = _categoriesRepository.GetCategories();

            //Assert
            Assert.AreEqual(categoryWantedID, categoryResultsTest[0].CategoryId);
            Assert.AreEqual(categoryWantedName, categoryResultsTest[0].CategoryName);
            Assert.AreEqual(categoryWantedDescription, categoryResultsTest[0].Description);
        }

        [TestMethod]
        public void GetCategoryByIdTestMethod()
        {
            //Arrange
            int categoryWantedID = 2;
            string categoryWantedName = "Calzados";
            string categoryWantedDescription = "Son accesorios para los pies";

            CategoriesDAL _categoriesRepository = new CategoriesDAL();
            //Act
            CategoryEntity categoryResultsTest = _categoriesRepository.GetCategoryById(2);

            //Assert
            Assert.AreEqual(categoryWantedID, categoryResultsTest.CategoryId);
            Assert.AreEqual(categoryWantedName, categoryResultsTest.CategoryName);
            Assert.AreEqual(categoryWantedDescription, categoryResultsTest.Description);
        }

        [TestMethod]
        public void CreateCategoryTestMethod()
        {
            //Arrange
            int createCategoryResultWanted = 1;

            CategoryEntity category = new CategoryEntity();
            category.CategoryName = "Calzados";
            category.Description = "Son accesorios para los pies";

            CategoriesDAL _categoriesRepository = new CategoriesDAL();
            //Act
            _categoriesRepository.CreateCategory(category);

            //Assert
            //Assert.AreEqual(createCategoryResultWanted, createCategoryResultsTest);
        }
        [TestMethod]
        public void UpdateCategoryTestMethod()
        {
            //Arrange
            int updateCategoryResultsWanted = 1; 
            CategoryEntity category = new CategoryEntity();
            category.CategoryId = 2;
            category.CategoryName = "Frutas";
            category.Description = "Producción de los árboles";

            CategoriesDAL _categoriesRepository = new CategoriesDAL();
            //Act
            int categoryResultsTest = _categoriesRepository.UpdateCategory(category);

            //Assert
            Assert.AreEqual(updateCategoryResultsWanted, categoryResultsTest);
        }
        [TestMethod]
        public void DeleteCategoryTestMethod()
        {
            //Arrange
            bool deleteCategoryResultsWanted = true;
            CategoriesDAL _categoriesRepository = new CategoriesDAL();
            //Act
            bool deleteCategoryTestResults = _categoriesRepository.DeleteCategory(2);
            //Assert
            Assert.AreEqual(deleteCategoryResultsWanted, deleteCategoryTestResults);
        }
    }
}
