using CnWeb_FastFood.Models.Dao.Admin;
using CnWeb_FastFood.Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Text;
using System.Net;
using Newtonsoft.Json;

namespace CnWeb_FastFood.Areas.Admin.Controllers
{
    public class BillController : Controller
    {
        SnackShopDBContext db = new SnackShopDBContext();
        BillDao Bdao = new BillDao();
        BillStatusDao BSdao = new BillStatusDao();
        BillDetailDao BDdao = new BillDetailDao();

        // GET: Admin/Product

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
            ViewBag.Count = Bdao.ListSimple(searching).Count();
            return View(Bdao.ListSimpleSearch(pageNumber, pagesize, searching));
        }

        public ActionResult Index2(int? page, int? PageSize, string idBill, string customerName, string phone, string address, string discountCode, string discountFrom, string discountTo, string subtotalFrom, string subtotalTo, string totalFrom, string totalTo, string status)
        {
            ViewBag.IdBill = idBill;
            ViewBag.CustomerName = customerName;
            ViewBag.CustomerPhone = phone;
            ViewBag.CustomerAddress = address;
            ViewBag.DiscountCode = discountCode;
            ViewBag.DiscountFrom = discountFrom;
            ViewBag.DiscountTo = discountTo;
            ViewBag.SubtotalFrom = subtotalFrom;
            ViewBag.SubtotalTo = subtotalTo;
            ViewBag.TotalFrom = totalFrom;
            ViewBag.TotalTo = totalTo;
            ViewBag.IdBillStatus = status;
            if (status != "" && status != null)
                ViewBag.Status = BSdao.getByID(Convert.ToInt32(status)).status;

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
            ViewBag.Count = Bdao.ListAdvanced(idBill, customerName, phone, address, discountCode, discountFrom, discountTo, subtotalFrom, subtotalTo, totalFrom, totalTo, status).Count();
            return View(Bdao.ListAdvancedSearch(pageNumber, pagesize, idBill, customerName, phone, address, discountCode, discountFrom, discountTo, subtotalFrom, subtotalTo, totalFrom, totalTo, status));
        }

      

        [HttpGet]
        public JsonResult Delete(int id)
        {
            Bill bill = db.Bills.Find(id);
            if(bill == null)
            {
                return Json(new { status = false }, JsonRequestBehavior.AllowGet);
            }
            db.Bills.Remove(bill);
            db.SaveChanges();
            return Json(new { status = true },  JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Details(int id)
        {
            using (var context = new SnackShopDBContext()) // set Configuration.ProxyCreationEnabled local in this function
            {
                context.Configuration.ProxyCreationEnabled = false;
                Bill bill = context.Bills.Where(b => b.id_bill == id).Include(b => b.BillStatus).Include(b => b.Customer).Include(b => b.BillDetails).SingleOrDefault();
                foreach (BillDetail bd in bill.BillDetails)
                {
                    bd.Product = context.Products.Find(bd.id_product);
                }
                var data = JsonConvert.SerializeObject(bill, Formatting.None,
                            new JsonSerializerSettings()
                            {
                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                            });
                return Json( data , JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult UpdateStatusBillJson([Bind(Include= "id_bill, id_status")] Bill bill)
        {
            db.Bills.Attach(bill);           
            db.Entry(bill).Property("id_status").IsModified = true; // không cần set id_bill vì nó là khóa
            db.SaveChanges();
            string nameStatusBill = db.BillStatus.Find(bill.id_status).status.ToString();
            return Json(new { status = true, id_status = bill.id_status, nameStatusBill = nameStatusBill}, JsonRequestBehavior.AllowGet);

        }


        public ActionResult IndexBillDetail(int id)
        {
            var listBillDetail = BDdao.getListBillDetailByIdBill(id);

            if (listBillDetail == null)
                return HttpNotFound();

            ViewBag.id_bill = id;

            return PartialView(listBillDetail);
        }

       
    }
}
