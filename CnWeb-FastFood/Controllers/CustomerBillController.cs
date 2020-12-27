using CnWeb_FastFood.Models;
using CnWeb_FastFood.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CnWeb_FastFood.Controllers
{
    public class CustomerBillController : BaseController
    {
        private SnackShopDBContext db = new SnackShopDBContext();
        // GET: Bill
        public ActionResult Index()
        {
            var id_current= Convert.ToInt32(Session[CommonConstants.ID_SESSION]);
            var list = db.Bills.Where(b => b.id_customer == id_current).OrderByDescending(b => b.creatDate);
            
            return View(list);
        }

        public ActionResult ListStatusShow()
        {
            var id_current = Convert.ToInt32(Session[CommonConstants.ID_SESSION]);
            ViewBag.Count_1 = db.Bills.Where(b => b.id_customer == id_current && b.id_status == 1).Count();
            ViewBag.Count_2 = db.Bills.Where(b => b.id_customer == id_current && b.id_status == 2).Count();
            ViewBag.Count_3 = db.Bills.Where(b => b.id_customer == id_current && b.id_status == 3).Count();
            return PartialView(db.BillStatus.ToList());
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