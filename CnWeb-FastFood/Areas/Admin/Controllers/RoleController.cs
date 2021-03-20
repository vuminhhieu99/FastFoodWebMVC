using CnWeb_FastFood.Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CnWeb_FastFood.Areas.Admin.Controllers
{
    public class RoleController : Controller
    {
        private SnackShopDBContext db = new SnackShopDBContext();
        // GET: Admin/Role
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
            var list = db.Roles.Where(r => r.id_role.Contains(searching) || r.name.Contains(searching)).ToList();
            ViewBag.Count = list.Count();
            return View(list.ToPagedList<Role>(pageNumber, pagesize));
        }
        // GET: Admin/Role
        public ActionResult Edit()
        {
            return View();
        }
        // GET: Admin/Role
        public ActionResult Delete()
        {
            return View();
        }
    }
}