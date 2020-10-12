using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HBRTEST.Models;
using HBRTEST.BLL;
using HBRTEST.ErrorHandling;
using HBRTEST.Domain;
using HBRTEST.Services;
using System.IO;

namespace HBRTEST.Controllers
{
    public class ProductsController : Controller
    {
        private ProductsBLL _productLogic = new ProductsBLL();
        private UploadImageService _uploadImageService = new UploadImageService();
        HttpCookie sessionUserIdCookie;
        [HttpPost]
        public ActionResult GetProductById(int ProductId)
        {
            sessionUserIdCookie = Request.Cookies["UserId"];
            if (Request.IsAjaxRequest() && sessionUserIdCookie != null)
            {
                try
                {
                    ProductEntity product = _productLogic.GetEntityById(ProductId);
                    return Json(product);
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
                ProductsModel productModel = new ProductsModel();
                productModel.LstProducts = _productLogic.GetAll();
                return View(productModel);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Users");
            }
        }

        [HttpPost]
        public ActionResult Index(ProductsModel productModel)
        {
            sessionUserIdCookie = Request.Cookies["UserId"];
            if (Request.IsAjaxRequest() && sessionUserIdCookie != null)
            {
                try
                {
                    if (productModel.ProductId > 0)
                    {
                        _productLogic.Update(productModel);
                        return Json("Producto modificado correctamente");
                    }
                    else
                    {
                        _productLogic.Add(productModel);
                        return Json("Producto creado correctamente");
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
        public ActionResult UploadImage()
        {
            string result = string.Empty;
            try
            {
                result = _uploadImageService.UploadImage(Request);
            }
            catch(Exception exception)
            {
                throw new PersonalizedException(exception.ToString());
            }
            return Json(result);
        }

        public ActionResult DetailsProduct(int ProductId)
        {
            ProductEntity product = new ProductEntity();
            try
            {
                product = _productLogic.GetEntityById(ProductId);
                return View(product);
            }
            catch (PersonalizedException personalizedException)
            {
                ViewBag.Error = personalizedException.Message;
                return View(product);
            }
        }

        [HttpPost]
        public ActionResult DeleteProduct(int ProductId)
        {
            sessionUserIdCookie = Request.Cookies["UserId"];
            if (Request.IsAjaxRequest() && sessionUserIdCookie != null)
            {
                try
                {
                    _productLogic.Delete(ProductId);
                    return Json("Producto eliminado correctamente");
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