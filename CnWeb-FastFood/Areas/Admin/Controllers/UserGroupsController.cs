using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using CnWeb_FastFood.Models.Dao.Admin;
using CnWeb_FastFood.Models.EF;

namespace CnWeb_FastFood.Areas.Admin.Controllers
{
    public class UserGroupsController : Controller
    {
        private SnackShopDBContext db = new SnackShopDBContext();
        private UserGroupDao dao = new UserGroupDao();
        // GET: Admin/UserGroups
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
            ViewBag.Count = dao.ListAll().Count();
            ViewBag.listRole = dao.GetAllRole();
            return View(dao.ListUserGroup(pageNumber, pagesize, searching));
        }

        // GET: Admin/UserGroups/Create
        public JsonResult CreateUserGroupsJson(string userGroup, string listRole)
        {
            var jsonUserGroup = new JavaScriptSerializer().Deserialize<UserGroup>(userGroup);
            List<string> jsonListRole = new JavaScriptSerializer().Deserialize<List<string>>(listRole);

            db.UserGroups.Add(jsonUserGroup);
            db.SaveChanges();
            foreach(var role in jsonListRole)
            {
                db.Credentials.Add(new Credential() { id_userGroup = jsonUserGroup.id_userGroup, id_role= role });
            }
            db.SaveChanges();

            return Json(new
            {
                status = true                
            });

           
        }

        // POST: Admin/UserGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_userGroup,name")] UserGroup userGroup)
        {
            if (ModelState.IsValid)
            {
                db.UserGroups.Add(userGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userGroup);
        }

        // GET: Admin/UserGroups/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserGroup userGroup = db.UserGroups.Find(id);
            if (userGroup == null)
            {
                return HttpNotFound();
            }
             ViewBag.listRole = dao.GetAllRole();
            return View(userGroup);
        }

        // POST: Admin/UserGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        
        public JsonResult editUserGroupsJson(string userGroup, string listRole)
        {
            var jsonUserGroup = new JavaScriptSerializer().Deserialize<UserGroup>(userGroup);
            List<string> jsonListRole = new JavaScriptSerializer().Deserialize<List<string>>(listRole);

            db.Entry(jsonUserGroup).State = EntityState.Modified;
            db.SaveChanges();
            var oldListRole = db.Credentials.Where(c => c.id_userGroup == jsonUserGroup.id_userGroup).ToList();
            foreach (var role in oldListRole)
            {               
                db.Credentials.Remove(role);
                db.SaveChanges();
            }
            foreach (var role in jsonListRole)
            {
                db.Credentials.Add(new Credential() {id_role = role, id_userGroup = jsonUserGroup.id_userGroup });
                db.SaveChanges();
            }

            return Json(new
            {
                status = true
            });

        }

        // GET: Admin/UserGroups/Delete/5

        [HttpDelete]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserGroup userGroup = db.UserGroups.Find(id);
            if (userGroup == null)
            {
                return HttpNotFound();
            }
            db.UserGroups.Remove(userGroup);
            db.SaveChanges();
            //var oldListRole = db.Credentials.Where(c => c.id_userGroup == userGroup.id_userGroup).ToList();
            //foreach (var role in oldListRole)
            //{
            //    db.Credentials.Remove(role);
            //    db.SaveChanges();
            //}


            return RedirectToAction("Index");
        }


       
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
