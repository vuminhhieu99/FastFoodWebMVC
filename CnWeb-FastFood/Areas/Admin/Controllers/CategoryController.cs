using CnWeb_FastFood.Models.Dao.Admin;
using CnWeb_FastFood.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CnWeb_FastFood.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        CategoryDao dao = new CategoryDao();
        SnackShopDBContext db = new SnackShopDBContext();
        // GET: Admin/Category
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
            ViewBag.Count = dao.ListSimple(searching).Count();
            return View(dao.ListSimpleSearch(pageNumber, pagesize, searching));
        }

        public ActionResult Details(int id)
        {
            Category category = dao.getByID(id);
            return View(category);
        }

        public ActionResult Edit(int id)
        {
            return View(dao.getByID(id));
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "id_category, name", Exclude = "photo")] Category category, HttpPostedFileBase photo, bool? photoChanged)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Attach(category);
                db.Entry(category).Property("id_category").IsModified = true;
                db.Entry(category).Property("photo").IsModified = true;

                if (photoChanged == true)
                {
                    string oldName = db.Database.SqlQuery<string>($"select avatar from dbo.Customer where id_customer = {category.id_category}").FirstOrDefault();
                    if (oldName != null && oldName != "")
                    {
                        var filePath = Server.MapPath("~/Areas/Admin/Content/Photos/") + oldName;
                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }
                    }
                    string filename = "";
                    if (photo != null)
                    {
                        string[] arrListStr = photo.FileName.Split('.');
                        string formatFile = arrListStr.ElementAt(arrListStr.Length - 1);
                        string createDate = DateTime.Now.ToShortDateString();
                        createDate = createDate.Replace("/", "");
                        filename = $"ctgr_{createDate}_photo" + "." + formatFile;
                        var path = Server.MapPath("~/Areas/Admin/Content/Photos/") + filename;
                        photo.SaveAs(path);
                    }
                    category.photo = filename;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            dao.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult CreateCategoryJson(string name, HttpPostedFileBase files)
        {
            if (ModelState.IsValid)
            {

                Category category = new Category();
                category.name = name;
                dao.Add(category);                

                string id = db.Categories.Where(c => c.name == name).First().id_category.ToString();
                if (files != null)
                {
                    string[] arrListStr = files.FileName.Split('.');

                    string formatFile = arrListStr.ElementAt(arrListStr.Length - 1);


                    string createDate = DateTime.Now.ToShortDateString();
                    createDate = createDate.Replace("/", "");
                    string filename = $"Ctgr_{id}_{createDate}" + "." + formatFile;
                    var path = Server.MapPath("~/Areas/Admin/Content/Photos/") + filename;
                    files.SaveAs(path);
                    Category currentCus = db.Categories.Find(Convert.ToInt32(id));
                    currentCus.photo = filename;
                    db.SaveChanges();
                }
                return Json(new { status = true, id_Category = id }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = false }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CheckName(string name)
        {

            if (name != null)
            {
                name = name.Trim();
                if (db.Categories.Where(c => c.name == name).ToList().Count > 0)
                {
                    return Json(new { status = true });
                }
            }

            return Json(new { status = false });
        }
    }
}