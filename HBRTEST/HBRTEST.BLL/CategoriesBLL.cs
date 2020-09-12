using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using HBRTEST.DAL;
using HBRTEST.Entities;

namespace HBRTEST.BLL
{
    public class CategoriesBLL: IDisposable
    {
        #region Definiciones
        private Component components = new Component();
        private bool _disposed = false;
        #endregion

        #region Constructor
        public CategoriesBLL()
        {

        }
        #endregion

        #region Métodos
        #region Métodos públicos
        public List<CategoryEntity> GetCategories()
        {
            try
            {
                using(CategoriesDAL _categoriesRepository = new CategoriesDAL())
                {
                    List<CategoryEntity> lstCategories = _categoriesRepository.GetCategories();
                    if(lstCategories != null)
                    {
                        return lstCategories;
                    }
                    return null;
                }
            }
            catch
            {
                throw new Exception();
            }
        }

        public CategoryEntity GetCategoryById(int CategoryID)
        {
            try
            {
                using(CategoriesDAL _categoriesRepostiroy = new CategoriesDAL())
                {
                    CategoryEntity category = _categoriesRepostiroy.GetCategoryById(CategoryID);
                    if(category != null)
                    {
                        return category;
                    }
                    return null;
                }
            }
            catch
            {
                throw new Exception();
            }
        }

        public string CreateCategory(CategoryEntity category)
        {
            try
            {
                using(CategoriesDAL _categoriesRepository = new CategoriesDAL())
                {
                    string categoryResult = _categoriesRepository.CreateCategory(category);
                    return categoryResult;
                }
            }
            catch
            {
                throw new Exception();
            }
        }

        public bool UpdateCategory(CategoryEntity category)
        {
            try
            {
                using(CategoriesDAL _categoriesRepository = new CategoriesDAL())
                {
                    bool isCategoryUpdated = _categoriesRepository.UpdateCategory(category);
                    return isCategoryUpdated;
                }
            }
            catch
            {
                throw new Exception();
            }
        }

        public bool DeleteCategory(int CategoryID)
        {
            try
            {
                using(CategoriesDAL _categoriesRepository = new CategoriesDAL())
                {
                    bool isCategoryDeleted = _categoriesRepository.DeleteCategory(CategoryID);
                    return isCategoryDeleted;
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
        ~CategoriesBLL() => Dispose(false);
        #endregion
    }
}
