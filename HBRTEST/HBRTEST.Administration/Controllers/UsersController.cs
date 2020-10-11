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
            Request.Cookies.Clear();
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
                        if(returnedUser.Active)
                        {
                            HttpCookie sessionUserIdCookie = new HttpCookie("UserId");
                            sessionUserIdCookie.Expires.AddMinutes(20);
                            sessionUserIdCookie.Value = returnedUser.UserId.ToString();
                            
                            HttpCookie sessionUserNameCookie = new HttpCookie("UserName");
                            sessionUserNameCookie.Expires.AddMinutes(20);
                            sessionUserNameCookie.Value = returnedUser.UserName;

                            Response.Cookies.Add(sessionUserIdCookie);
                            Response.Cookies.Add(sessionUserNameCookie);
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
            HttpCookie sessionUserIdCookie = Request.Cookies["UserId"];
            if(sessionUserIdCookie != null)
            {
                int userId = int.Parse(sessionUserIdCookie.Value.ToString());
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
                HttpCookie sessionUserIdCookie = Request.Cookies["UserId"];
                if (Request.IsAjaxRequest() && sessionUserIdCookie != null)
                {
                    _usersLogic.UpdateProfile(user);
                    return Json("Usuario modificado correctamente");
                }
                else
                {
                    return RedirectToAction("AccessDenied", "Users");
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
            Request.Cookies.Clear();
            return View("Index");
        }

        public ActionResult AccessDenied()
        {
            Request.Cookies.Clear();
            return View();
        }
    }
}