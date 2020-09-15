using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using HBRTEST.DAL;
using HBRTEST.Entities;

namespace HBRTEST.BLL
{
    public class CategoriesBLL
    {
        CategoriesDAL _categoriesRepository = new CategoriesDAL();
        public CategoriesBLL()
        {

        }
        
        public List<CategoryEntity> GetCategories()
        {
            List<CategoryEntity> lstCategories = _categoriesRepository.GetCategories();
            return lstCategories;
        }

        public CategoryEntity GetCategoryById(int CategoryID)
        {
            CategoryEntity category = _categoriesRepository.GetCategoryById(CategoryID);
            return category;
        }

        public void CreateCategory(CategoryEntity category)
        {
            _categoriesRepository.CreateCategory(category);
        }

        public void UpdateCategory(CategoryEntity category)
        {
            _categoriesRepository.UpdateCategory(category);
        }

        public void DeleteCategory(int CategoryID)
        {
           _categoriesRepository.DeleteCategory(CategoryID);
        }
    }
}
