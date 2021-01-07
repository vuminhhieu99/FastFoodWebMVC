using CnWeb_FastFood.Models.Dao.Admin;
using CnWeb_FastFood.Models.EF;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;


namespace CnWeb_FastFood.Areas.Admin.Controllers
{
    public class DiscountCodeController : BaseController
    {
        SnackShopDBContext db = new SnackShopDBContext();
        DiscountCodeDao dao = new DiscountCodeDao();

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
            ViewBag.Count = dao.ListSimple(searching).Count();
            return View(dao.ListSimpleSearch(pageNumber, pagesize, searching));
        }

        

        public ActionResult Edit(string id)
        {
            return View(dao.getByID(id));
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "id_discountCode, discount")] DiscountCode discountCode)
        {
            
            if (ModelState.IsValid)
            {
                db.Entry(discountCode).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }          
             return View(discountCode);
            
        }

        [HttpDelete]
        public ActionResult Delete(string id)
        {
            dao.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult CreateDiscountCodeJson([Bind(Include ="id_discountCode, discount")] DiscountCode discountCode)
        {

            if (ModelState.IsValid)
            {
                discountCode.discount = discountCode.discount * 1000;


                db.DiscountCodes.Add(discountCode);
                db.SaveChanges();                
              
                return Json(new { status = true, id_discountCode = discountCode.id_discountCode }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = false }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CheckDiscountCode(string id_discountCode)
        {

            if (id_discountCode != null)
            {
                id_discountCode = id_discountCode.Trim();
                if (db.DiscountCodes.Find(id_discountCode) != null)
                {
                    return Json(new { status = true });
                }
            }

            return Json(new { status = false });
        }
    }
}