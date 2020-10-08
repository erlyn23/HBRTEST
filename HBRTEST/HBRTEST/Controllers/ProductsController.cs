using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HBRTEST.Models;
using HBRTEST.BLL;
using HBRTEST.ErrorHandling;
using HBRTEST.Domain;

namespace HBRTEST.Controllers
{
    public class ProductsController : Controller
    {
        private ProductsBLL _productLogic = new ProductsBLL();

        [HttpPost]
        public ActionResult GetProductById(int ProductId)
        {
            if (Request.IsAjaxRequest())
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
                return RedirectToAction("/Users/AccessDenied");
            }
        }

        [HttpPost]
        public ActionResult FilterProducts(string filter, string search)
        {
            if (Request.IsAjaxRequest())
            {
                ProductsModel productModel = new ProductsModel();
                try
                {
                    if (filter.Equals("Category"))
                    {

                        productModel.LstProducts = _productLogic.FilterProductsByCategoryName(search);
                        return Json(productModel.LstProducts);
                    }
                    else
                    {
                        productModel.LstProducts = _productLogic.FilterProductsByProductName(search);
                        return Json(productModel.LstProducts);
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

        public ActionResult Index()
        {
            ProductsModel productModel = new ProductsModel();
            productModel.LstProducts = _productLogic.GetAll();
            return View(productModel);
        }

        [HttpPost]
        public ActionResult Index(ProductsModel productModel)
        {
            if (Request.IsAjaxRequest())
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
                return RedirectToAction("/Users/AccessDenied");
            }
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
            if (Request.IsAjaxRequest())
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
                return RedirectToAction("/Users/AccessDenied");             
            }
        }
    }
}