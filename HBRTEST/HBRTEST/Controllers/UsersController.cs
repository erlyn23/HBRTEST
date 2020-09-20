using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HBRTEST.Entities;
using HBRTEST.BLL;
using HBRTEST.ErrorHandling;

namespace HBRTEST.Controllers
{
    public class UsersController : Controller
    {
        private UsersBLL _usersLogic = new UsersBLL();
        public ActionResult Index()
        {
            Session["UserId"] = null;
            Session["UserName"] = null;
            return View();
        }

        [HttpPost]
        public ActionResult Index(string UserName, string Password)
        {
            try
            {
                if (Request.IsAjaxRequest())
                {
                    var returnedUser = _usersLogic.SignIn(UserName, Password);
                    if (!string.IsNullOrEmpty(returnedUser.UserName) || returnedUser.UserId >= 1)
                    {
                        Session["UserId"] = returnedUser.UserId;
                        Session["UserName"] = returnedUser.UserName;
                        return Json("/Categories/Index");
                    }
                    return Json("Usuario o contraseña incorrecta");
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
            if(Session["UserId"] != null)
            {
                UserEntity currentUser = _usersLogic.GetUserById(int.Parse(Session["UserId"].ToString()));
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

        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}