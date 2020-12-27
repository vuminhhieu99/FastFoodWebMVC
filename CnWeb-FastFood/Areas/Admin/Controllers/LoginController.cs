using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CnWeb_FastFood.Models.Dao.Admin;
using CnWeb_FastFood.Models;
using CnWeb_FastFood.Areas.Admin.Models;

namespace CnWeb_FastFood.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(collection["inputUserName"].ToString(), collection["UserPassword"].ToString());
                if (result == 1)
                {
                    var user = dao.GetById(collection["inputUserName"].ToString());
                    var userSession = new UserLogin();
                    userSession.Name = user.name;
                    userSession.UserID = user.id_user;
                    userSession.CreateDate = user.createDate;

                    Session.Add(CommonConstants.USER_SESSION, userSession.Name);
                    Session.Add(CommonConstants.ID_SESSION, userSession.UserID);
                    Session.Add(CommonConstants.CREATE_USER_SESSION, userSession.CreateDate);
                    return RedirectToAction("Index", "Dashboard");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Account isn't exist");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Account is locked");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Password is wronged");
                }
                else
                {
                    ModelState.AddModelError("", "Login false");
                }
            }           
            return RedirectToAction("Index");
        }
    }
}