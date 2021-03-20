using CnWeb_FastFood.Models;
using CnWeb_FastFood.Models.EF;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CnWeb_FastFood.Areas.Admin.Controllers
{
    public class DashboardController : BaseController
    {
        // GET: Admin/Dashboard
        SnackShopDBContext db = new SnackShopDBContext();
        public ActionResult Index()
        {
            StatisticView statistic = new StatisticView();
            statistic.productTotal = db.Products.Count();
            DateTime dateNow = DateTime.Now;
            decimal? saleDay = db.Bills.Where(b => b.creatDate.Value.Day == dateNow.Day && b.creatDate.Value.Month == dateNow.Month && b.creatDate.Value.Year == dateNow.Year).Sum(b => b.total);
            decimal? saleMonth = db.Bills.Where(b => b.creatDate.Value.Month == dateNow.Month && b.creatDate.Value.Year == dateNow.Year).Sum(b => b.total);
            decimal? saleYear = db.Bills.Where(b => b.creatDate.Value.Year == dateNow.Year).Sum(b => b.total);
            statistic.saleByDay = (saleDay != null) ? (float)saleDay : 0;
            statistic.saleByMonth = (saleMonth != null) ? (float)saleMonth : 0;
            statistic.saleByYear = (saleYear != null) ? (float)saleYear : 0;
            statistic.totalCustomer = db.Customers.Count();
            statistic.totalBill = db.Bills.Where(x => x.id_status == 4).Count();
            var mapStatictis = db.Database.SqlQuery<int?>($"EXEC Statictis").ToList();
            statistic.mapStatictis = mapStatictis;
            return View(statistic);
        }

        public JsonResult MapStatictis()
        {
            var mapStatictis = db.Database.SqlQuery<int?>($"EXEC Statictis").ToList();

            return Json(new { data = mapStatictis }, JsonRequestBehavior.AllowGet);

        }
    }
}