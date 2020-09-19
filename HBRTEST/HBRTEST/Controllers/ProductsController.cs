using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HBRTEST.Models;
using HBRTEST.BLL;
using HBRTEST.ErrorHandling;
using HBRTEST.Entities;

namespace HBRTEST.Controllers
{
    public class ProductsController : Controller
    {
        private ProductsBLL _productLogic = new ProductsBLL();

        [HttpPost]
        public ActionResult GetProductById(int ProductId)
        {
            try
            {
                ProductEntity product = _productLogic.GetProductById(ProductId);
                return Json(product);
            }
            catch(PersonalizedException personalizedException)
            {
                return Json(personalizedException.Message);
            }
        }

        [HttpPost]
        public ActionResult FilterProducts(string filter, string search)
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

        public ActionResult Index()
        {
            ProductsModel productModel = new ProductsModel();
            productModel.LstProducts = _productLogic.GetProducts();
            return View(productModel);
        }

        [HttpPost]
        public ActionResult Index(ProductsModel productModel)
        {
            try
            {
                if(productModel.ProductId > 0)
                {
                    _productLogic.UpdateProduct(productModel);
                    return Json("Producto modificado correctamente");
                }
                else
                {
                    _productLogic.CreateProduct(productModel);
                    return Json("Producto creado correctamente");
                }
            }
            catch(PersonalizedException personalizedException)
            {
                return Json(personalizedException.Message);
            }
        }

        public ActionResult DetailsProduct(int ProductId)
        {
            ProductEntity product = new ProductEntity();
            try
            {
                
                product = _productLogic.GetProductById(ProductId);
                return View(product);
            }
            catch(PersonalizedException personalizedException)
            {
                ViewBag.Error = personalizedException.Message;
                return View(product);
            }
        }

        [HttpPost]
        public ActionResult DeleteProduct(int ProductId)
        {
            try
            {
                _productLogic.DeleteProduct(ProductId);
                return Json("Producto eliminado correctamente");
            }
            catch (PersonalizedException personalizedException)
            {
                return Json(personalizedException.Message);
            }
        }
    }
}