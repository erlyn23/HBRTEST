using System.Web.Mvc;
using HBRTEST.BLL;
using HBRTEST.Domain;
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
            if (Request.IsAjaxRequest() && HttpContext.Session["UserID"] != null)
            {
                try
                {
                    CategoryEntity category = _categoryLogic.GetEntityById(CategoryID);
                    return Json(category);
                }
                catch (PersonalizedException personalizedException)
                {
                    return Json(personalizedException.Message);
                }
            }
            else
            {
                return RedirectToAction("/Users/AccessDenied");
            }
        }
        public ActionResult Index()
        {
           if(HttpContext.Session["UserID"] != null)
            {
                CategoryModel categoryModel = new CategoryModel();
                categoryModel.LstCategories = _categoryLogic.GetAll();
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
            if (Request.IsAjaxRequest() && HttpContext.Session["UserID"] != null)
            {
                try
                {
                    if (categoryModel.CategoryId > 0)
                    {
                        _categoryLogic.Update(categoryModel);
                        return Json("Categoría modificada correctamente");
                    }
                    else
                    {
                        _categoryLogic.Add(categoryModel);
                        return Json("Categoría creada correctamente");
                    }
                }
                catch (PersonalizedException personalizedException)
                {
                    return Json(personalizedException.Message);
                }
            }
            else
            {
                return RedirectToAction("/Users/AccessDenied");
            }
        }

        [HttpPost]
        public ActionResult DeleteCategory(int CategoryID)
        {
            if (Request.IsAjaxRequest() && HttpContext.Session["UserID"] != null)
            {
                try
                {
                    _categoryLogic.Delete(CategoryID);
                    return Json("Categoría eliminada correctamente");
                }
                catch (PersonalizedException personalizedException)
                {
                    return Json(personalizedException.Message);
                }
            }
            else
            {
                return RedirectToAction("/Users/AccessDenied");
            }
        }
    }
}