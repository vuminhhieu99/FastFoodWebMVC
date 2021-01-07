using CnWeb_FastFood.Models.Dao.Admin;
using CnWeb_FastFood.Models.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace CnWeb_FastFood.Areas.Admin.Controllers
{
    public class CustomerController : BaseController
    {
        SnackShopDBContext db = new SnackShopDBContext();
        CustomerDao Cdao = new CustomerDao();

        // GET: Admin/Customer
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
            return View(Cdao.ListSimpleSearch(pageNumber, pagesize, searching));
        }

        public ActionResult Index2(int? page, int? PageSize, string idCustomer, string name, string phone, string address, string username)
        {
            ViewBag.IdCustomer = idCustomer;
            ViewBag.CustomerName = name;
            ViewBag.CustomerPhone = phone;
            ViewBag.CustomerAddress = address;
            ViewBag.CustomerUserName = username;

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
            ViewBag.Count = Cdao.ListAdvanced(idCustomer, name, phone, address, username).Count();
            return View(Cdao.ListAdvancedSearch(pageNumber, pagesize, idCustomer, name, phone, address, username));
        }

        public ActionResult Filter()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            Customer customer = Cdao.getByID(id);
            return View(customer);
        }

        public ActionResult Edit(int id)
        {           
            return View(Cdao.getByID(id));
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "id_customer, name, phone, address, userName, password", Exclude = "avatar")] Customer customer, HttpPostedFileBase avatar, bool? avatarChanged)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Attach(customer);
                db.Entry(customer).Property("id_customer").IsModified = true;
                db.Entry(customer).Property("name").IsModified = true;
                db.Entry(customer).Property("phone").IsModified = true;
                db.Entry(customer).Property("address").IsModified = true;
                db.Entry(customer).Property("userName").IsModified = true;
                db.Entry(customer).Property("password").IsModified = true;
                if (avatarChanged == true)
                {
                    string oldName = db.Database.SqlQuery<string>($"select avatar from dbo.Customer where id_customer = {customer.id_customer}").FirstOrDefault();
                    if (oldName != null && oldName != "")
                    {
                        var filePath = Server.MapPath("~/Areas/Admin/Content/Photos/") + oldName;
                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);                            
                        }
                    }
                    string filename = "";
                    if (avatar != null)
                    {
                        string[] arrListStr = avatar.FileName.Split('.');
                        string formatFile = arrListStr.ElementAt(arrListStr.Length - 1);
                        string createDate = DateTime.Now.ToShortDateString();
                        createDate = createDate.Replace("/", "");
                        filename = $"ctm_{createDate}_avatar" + "." + formatFile;
                        var path = Server.MapPath("~/Areas/Admin/Content/Photos/") + filename;
                        avatar.SaveAs(path);                        
                    }
                    customer.avatar = filename;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Cdao.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult postdata([Bind(Include = "name, phone,address,userName,password")] Customer customer, IEnumerable<HttpPostedFileBase> files)
        {
            
            if (ModelState.IsValid)
            {
                customer.subtotalCart = 0;
                customer.totalCart = 0;
                db.Customers.Add(customer);
                db.SaveChanges();

                string id = db.Customers.Where(c => c.userName == customer.userName && c.password == customer.password).First().id_customer.ToString();
                if (files != null)
                {
                    string[] arrListStr = files.ElementAt(0).FileName.Split('.');

                    string formatFile = arrListStr.ElementAt(arrListStr.Length - 1);


                    string createDate = DateTime.Now.ToShortDateString();
                    createDate = createDate.Replace("/", "");
                    string filename = $"cus_{id}_{createDate}" + "." + formatFile;
                    var path = Server.MapPath("~/Areas/Admin/Content/Photos/") + filename ;
                    files.ElementAt(0).SaveAs(path);
                    Customer currentCus = db.Customers.Find(Convert.ToInt32(id));
                    currentCus.avatar = filename;
                    db.SaveChanges();
                }      
                return Json(new {status = true, id_customer = id }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = false }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult CheckUserName(string userName)
        {
       
            if (userName != null)
            {
                userName = userName.Trim();
                if (db.Customers.Where(c => c.userName == userName).ToList().Count > 0)
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

        //                var path = "C:/Users/BlueMan/Desktop/" +  file.FileName;
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
