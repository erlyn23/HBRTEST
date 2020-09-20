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
        public ActionResult Index()
        {
            if(Session["UserId"] != null)
            {
                CategoryModel categoryModel = new CategoryModel();
                categoryModel.LstCategories = _categoryLogic.GetCategories();
                return View(categoryModel);
            }
            else
            {
                return RedirectToAction("/Users/Index");
            }
        }
        [HttpPost]
        public ActionResult Index(CategoryModel categoryModel)
        {
            try
            {
                if(categoryModel.CategoryId > 0)
                {
                    _categoryLogic.UpdateCategory(categoryModel);
                    return Json("Categoría modificada correctamente");
                }
                else
                {
                    _categoryLogic.CreateCategory(categoryModel);
                    return Json("Categoría creada correctamente");
                }
            }
            catch (PersonalizedException personalizedException)
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