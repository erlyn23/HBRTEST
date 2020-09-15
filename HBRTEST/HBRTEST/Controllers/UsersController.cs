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
        public ActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignIn(string UserName, string Password)
        {
            return View();
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
                _usersLogic.CreateUser(user);
                return Json("Usuario creado correctamente");
            }
            catch(Exception exception)
            {
                throw new PersonalizedException(exception.Message);
            }
        }

        public ActionResult UpdateProfile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UpdateProfile(UserEntity user)
        {
            try
            {
                _usersLogic.UpdateProfile(user);
                return Json("Usuario modificado correctamente");
            }
            catch (Exception exception)
            {
                throw new PersonalizedException(exception.Message);
            }
        }

    }
}