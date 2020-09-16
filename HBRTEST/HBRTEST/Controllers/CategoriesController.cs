using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HBRTEST.BLL;
using HBRTEST.Entities;
using HBRTEST.ErrorHandling;
using HBRTEST.Models;

namespace HBRTEST.Controllers
{
    public class CategoriesController : Controller
    {
        private CategoriesBLL _categoryLogic = new CategoriesBLL();


        [HttpPost]
        public ActionResult GetCategoryById(int CategoryID)
        {
            try
            {
                CategoryEntity category = _categoryLogic.GetCategoryById(CategoryID);
                return Json(category);
            }
            catch(PersonalizedException personalizedException)
            {
                return Json(personalizedException.Message);
            }
        }
        public ActionResult CategoryHome()
        {
            CategoryModel categoryModel = new CategoryModel();
            categoryModel.lstCategories = _categoryLogic.GetCategories();
            return View(categoryModel);
        }
        [HttpPost]
        public ActionResult CategoryHome(CategoryModel categoryModel)
        {
            try
            {
                _categoryLogic.CreateCategory(categoryModel);
                return Json("Categoría creada correctamente");
            }
            catch(PersonalizedException personalizedException)
            {
                return Json(personalizedException.Message);
            }
        }

        [HttpPost]
        public ActionResult UpdateCategory(CategoryModel categoryModel)
        {
            try
            {
                _categoryLogic.UpdateCategory(categoryModel);
                return Json("Categoría actualizada correctamente");
            }
            catch(PersonalizedException personalizedException)
            {
                return Json(personalizedException.Message);
            }
        }

        [HttpPost]
        public ActionResult DeleteCategory(int CategoryID)
        {
            try
            {
                _categoryLogic.DeleteCategory(CategoryID);
                return Json("Categoría eliminada correctamente");
            }
            catch(PersonalizedException personalizedException)
            {
                return Json(personalizedException.Message);
            }
        }
    }
}