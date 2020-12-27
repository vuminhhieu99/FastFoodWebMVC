using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CnWeb_FastFood.Models.Dao.Client;
using CnWeb_FastFood.Models.EF;
using CnWeb_FastFood.Models;
using CnWeb_FastFood.Areas.Admin.Models;

namespace CnWeb_FastFood.Controllers
{
    public class CustomerLoginController : Controller
    {
        // GET: CustomerLogin
        SnackShopDBContext db = new SnackShopDBContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                var dao = new CustomerDao();
                var result = dao.Login(collection["inputUserName"].ToString(), collection["UserPassword"].ToString());
                if (result == 1)
                {
                    var user = dao.GetById(collection["inputUserName"].ToString());
                    var userSession = new UserLogin();
                    userSession.Name = user.name;
                    userSession.UserID = user.id_customer;
                    userSession.Avatar = user.avatar;

                    Session.Add(CommonConstants.USER_SESSION, userSession.Name);
                    Session.Add(CommonConstants.ID_SESSION, userSession.UserID);
                    Session.Add(CommonConstants.AVATAR_SESSION, userSession.Avatar);
                    
                    return RedirectToAction("Index", "Home");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Account isn't exist");
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