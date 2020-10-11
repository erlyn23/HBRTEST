using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using HBRTEST.DAL;
using HBRTEST.Domain;
using HBRTEST.Core.Interfaces;

namespace HBRTEST.BLL
{
    public class CategoriesBLL: IRepository<CategoryEntity>
    {
        CategoriesDAL _categoriesRepository = new CategoriesDAL();
        public CategoriesBLL()
        {

        }
        
        public List<CategoryEntity> GetAll()
        {
            List<CategoryEntity> lstCategories = _categoriesRepository.GetCategories();
            return lstCategories;
        }

        public CategoryEntity GetEntityById(int CategoryID)
        {
            CategoryEntity category = _categoriesRepository.GetCategoryById(CategoryID);
            return category;
        }

        public void Add(CategoryEntity category)
        {
            _categoriesRepository.CreateCategory(category);
        }

        public void Update(CategoryEntity category)
        {
            _categoriesRepository.UpdateCategory(category);
        }

        public void Delete(int CategoryID)
        {
           _categoriesRepository.DeleteCategory(CategoryID);
        }
    }
}
