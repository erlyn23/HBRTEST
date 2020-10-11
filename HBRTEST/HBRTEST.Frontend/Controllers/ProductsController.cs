using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HBRTEST.BLL;
using HBRTEST.Domain;
using HBRTEST.ErrorHandling;
using HBRTEST.Frontend.Models;

namespace HBRTEST.Frontend.Controllers
{
    public class ProductsController : Controller
    {
        ProductsBLL _productsLogic = new ProductsBLL();

        public ActionResult Index()
        {
            ProductsModel productsModel = new ProductsModel();
            try 
            {
                productsModel.LstProducts = _productsLogic.GetAll();
                return View(productsModel);
            }
            catch(Exception exception)
            {
                throw new PersonalizedException(exception.ToString());
            }
        }

        public ActionResult ProductsDetail(int productId)
        {
            try
            {
                ProductEntity product = _productsLogic.GetEntityById(productId);
                return View(product);
            }
            catch(Exception exception)
            {
                throw new PersonalizedException(exception.ToString());
            }
        }

        [HttpPost]
        public ActionResult FilterProducts(int categoryId, string productName)
        {
            ProductsModel productsModel = new ProductsModel();
            try
            {
                productsModel.LstProducts = _productsLogic.FilterProducts(categoryId, productName);
                return Json(productsModel.LstProducts);
            }
            catch (Exception exception)
            {
                throw new PersonalizedException(exception.ToString());
            }
        }
    }
}