using System.Web;
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
        private HttpCookie sessionUserIdCookie;

        [HttpPost]
        public ActionResult GetCategoryById(int CategoryID)
        {
            sessionUserIdCookie = Request.Cookies["UserId"];
            if (Request.IsAjaxRequest() && sessionUserIdCookie != null)
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
                return RedirectToAction("AccessDenied", "Users");
            }
        }
        public ActionResult Index()
        {
            sessionUserIdCookie = Request.Cookies["UserId"];
            if (sessionUserIdCookie != null)
            {
                CategoryModel categoryModel = new CategoryModel();
                categoryModel.LstCategories = _categoryLogic.GetAll();
                return View(categoryModel);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Users");
            }
        }
        [HttpPost]
        public ActionResult Index(CategoryModel categoryModel)
        {
            sessionUserIdCookie = Request.Cookies["UserId"];
            if (Request.IsAjaxRequest() && sessionUserIdCookie != null)
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
                return RedirectToAction("AccessDenied", "Users");
            }
        }

        [HttpPost]
        public ActionResult DeleteCategory(int CategoryID)
        {
            sessionUserIdCookie = Request.Cookies["UserId"];
            if (Request.IsAjaxRequest() && sessionUserIdCookie != null)
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
                return RedirectToAction("AccessDenied", "Users");
            }
        }
    }
}