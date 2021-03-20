using CnWeb_FastFood.Models.Dao.Admin;
using CnWeb_FastFood.Models.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;


namespace CnWeb_FastFood.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        SnackShopDBContext db = new SnackShopDBContext();
        UserDao Cdao = new UserDao();

        // GET: Admin/User
        public ActionResult Index(int? page, int? PageSize, string searching = "")
        {
            ViewBag.SearchString = searching;
            ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Value="10", Text= "10" },
                new SelectListItem() { Value="15", Text= "15" },
                new SelectListItem() { Value="20", Text= "20" },
                new SelectListItem() { Value="25", Text= "25" },
                new SelectListItem() { Value="50", Text= "50" }
            };
            int pageNumber = (page ?? 1);
            int pagesize = (PageSize ?? 10);
            ViewBag.psize = pagesize;
            ViewBag.Count = Cdao.ListSimple(searching).Count();
            ViewBag.UserGroup = new SelectList(db.UserGroups, "id_userGroup", "name");
            return View(Cdao.ListSimpleSearch(pageNumber, pagesize, searching));
        }

        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }
        public ActionResult Filter()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            User User = Cdao.getByID(id);
            return View(User);
        }

        public ActionResult Edit(int id)
        {
            return View(Cdao.getByID(id));
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "id_user, name, email, address, userName, password,status")] User User)
        {
            if (ModelState.IsValid)
            {
                db.Users.Attach(User);
                db.Entry(User).Property("id_user").IsModified = true;
                db.Entry(User).Property("name").IsModified = true;
                db.Entry(User).Property("email").IsModified = true;
                db.Entry(User).Property("address").IsModified = true;
                db.Entry(User).Property("userName").IsModified = true;
                db.Entry(User).Property("password").IsModified = true;
                db.Entry(User).Property("status").IsModified = true;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(User);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Cdao.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult CreateUserJson(User User)
        { 

            if (ModelState.IsValid)
            {
                bool t = Cdao.CheckUserName(User.userName);
                if (Cdao.CheckUserName(User.userName) == false)
                {
                    return Json(new { status = false }, JsonRequestBehavior.AllowGet);
                }
                User.password = CreateMD5(User.password);
                db.Users.Add(User);
                db.SaveChanges();

                return Json(new { status = true, id_user = User.id_user }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = false }, JsonRequestBehavior.AllowGet);
        }


        //[HttpPost]
        //public JsonResult postdata([Bind(Include = "name, phone,address,userName,password")] User User, IEnumerable<HttpPostedFileBase> files)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        User.subtotalCart = 0;
        //        User.totalCart = 0;
        //        db.Users.Add(User);
        //        db.SaveChanges();

        //        string id = db.Users.Where(c => c.userName == User.userName && c.password == User.password).First().id_User.ToString();
        //        if (files != null)
        //        {
        //            string[] arrListStr = files.ElementAt(0).FileName.Split('.');

        //            string formatFile = arrListStr.ElementAt(arrListStr.Length - 1);


        //            string createDate = DateTime.Now.ToShortDateString();
        //            createDate = createDate.Replace("/", "");
        //            string filename = $"cus_{id}_{createDate}" + "." + formatFile;
        //            var path = Server.MapPath("~/Areas/Admin/Content/Photos/") + filename;
        //            files.ElementAt(0).SaveAs(path);
        //            User currentCus = db.Users.Find(Convert.ToInt32(id));
        //            currentCus.avatar = filename;
        //            db.SaveChanges();
        //        }
        //        return Json(new { status = true, id_User = id }, JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(new { status = false }, JsonRequestBehavior.AllowGet);
        //}

        [HttpGet]
        public JsonResult CheckUserName(string userName)
        {

            if (userName != null)
            {
                userName = userName.Trim();
                if (db.Users.Where(c => c.userName == userName).ToList().Count > 0)
                {
                    return Json(new { status = true }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new { status = false }, JsonRequestBehavior.AllowGet);
        }

        //// test
        //public JsonResult SaveUploadedFile(string name, string phone, string address, string userName, string password, IEnumerable<HttpPostedFileBase> files)
        //{
        //    bool SavedSuccessfully = true;
        //    string fName = "";

        //    // test file
        //    if (files ==null)
        //        return Json(new { status = false }, JsonRequestBehavior.AllowGet);
        //    try
        //    {
        //        //loop through all the files
        //        foreach (var file in files)
        //        {

        //            //Save file content goes here
        //            fName = file.FileName;
        //            if (file != null && file.ContentLength > 0)
        //            {

        //                var originalDirectory = new DirectoryInfo(string.Format("{0}Images\\", Server.MapPath(@"\")));

        //                string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "imagepath");

        //                var fileName1 = Path.GetFileName(file.FileName);

        //                bool isExists = System.IO.Directory.Exists(pathString);

        //                if (!isExists)
        //                    System.IO.Directory.CreateDirectory(pathString);

        //                var path = "C:/User/BlueMan/Desktop/" +  file.FileName;
        //                file.SaveAs(path);

        //            }

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        SavedSuccessfully = false;
        //    }


        //    if (SavedSuccessfully)
        //    {
        //        return Json(new { status = false }, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(new { status = false }, JsonRequestBehavior.AllowGet);
        //    }
        //}
    }
}
