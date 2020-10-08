using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HBRTEST.Domain;
using HBRTEST.BLL;
using HBRTEST.ErrorHandling;

namespace HBRTEST.Controllers
{
    public class UsersController : Controller
    {
        private UsersBLL _usersLogic = new UsersBLL();
        public ActionResult Index()
        {
            HttpContext.Session.Clear();
            return View();
        }

        [HttpPost]
        public ActionResult Index(string UserName, string Password)
        {
            string output = string.Empty;
            try
            {
                if (Request.IsAjaxRequest())
                {
                    var returnedUser = _usersLogic.SignIn(UserName, Password);
                    if (!string.IsNullOrEmpty(returnedUser.UserName) || returnedUser.UserId >= 1)
                    {
                        if(returnedUser.Status == "Activo")
                        {
                            HttpContext.Session.Add("UserID", returnedUser.UserId);
                            HttpContext.Session.Add("UserName", returnedUser.UserName);
                            output = "/Categories/Index";
                        }
                        else
                        {
                            output = "El usuario se ha marcado como inactivo";
                        }
                    }
                    else
                    {
                        output = "Usuario o contraseña incorrecta";
                    }
                }
                else
                {
                    return RedirectToAction("AccessDenied");
                }
            }
            catch(PersonalizedException personalizedException)
            {
                return Json(personalizedException.Message);
            }
            return Json(output);
        }

        public ActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateUser(UserEntity user)
        {
            try
            {
                if (Request.IsAjaxRequest())
                {
                    _usersLogic.CreateUser(user);
                    return Json("Usuario creado correctamente");
                }
                else
                {
                    return RedirectToAction("AccessDenied");
                }
            }
            catch(Exception exception)
            {
                throw new PersonalizedException(exception.Message);
            }
        }

        public ActionResult UpdateProfile()
        {
            if(HttpContext.Session["UserID"] != null)
            {
                int userId = int.Parse(HttpContext.Session["UserID"].ToString());
                UserEntity currentUser = _usersLogic.GetUserById(userId);
                return View(currentUser);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult UpdateProfile(UserEntity user)
        {
            try
            {
                if (Request.IsAjaxRequest())
                {
                    _usersLogic.UpdateProfile(user);
                    return Json("Usuario modificado correctamente");
                }
                else
                {
                    return RedirectToAction("AccessDenied");
                }
            }
            catch (Exception exception)
            {
                throw new PersonalizedException(exception.Message);
            }
        }

        [HttpPost]
        public ActionResult LogOut()
        {
            HttpContext.Session.Abandon();
            return View("Index");
        }

        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}