using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CnWeb_FastFood.Models.Dao.Admin;
using CnWeb_FastFood.Models;
using CnWeb_FastFood.Areas.Admin.Models;
using System.Security.Cryptography;
using System.Text;

namespace CnWeb_FastFood.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            if (TempData["result"] != null)
            {
                ModelState.AddModelError("result", TempData["result"].ToString());
            }
            return View();
        }
       
        public ActionResult Login(FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(collection["inputUserName"].ToString(), collection["UserPassword"].ToString());
                if (result.status == true)
                {
                    var user = dao.GetById(collection["inputUserName"].ToString());
                    var userSession = new UserLogin();
                    userSession.Name = user.name;
                    userSession.UserID = user.id_user;
                    userSession.CreateDate = user.createDate;
                    var listCredential = dao.GetListCredential(user.id_userGroup);
                    Session.Add(AdminCommonConstants.USER_SESSION, userSession.Name);
                    Session.Add(AdminCommonConstants.ID_SESSION, userSession.UserID);
                    Session.Add(AdminCommonConstants.CREATE_USER_SESSION, userSession.CreateDate);
                    Session.Add(AdminCommonConstants.SESSION_CREDENTIAL, listCredential);
                    return RedirectToAction("Index", "Dashboard");
                }               
               
                else
                {
                    TempData["result"]= result.message;
                }
            }           
            return RedirectToAction("Index");
        }

        public ActionResult Register()
        {

            return View();
        }

        public ActionResult CreateAccount(FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                collection["inputUserName"].ToString();
            }
                return RedirectToAction("Index");
        }
    }
}