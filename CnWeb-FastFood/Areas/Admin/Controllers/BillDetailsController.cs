using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CnWeb_FastFood.Models.EF;

namespace CnWeb_FastFood.Areas.Admin.Controllers
{
    public class BillDetailsController : BaseController
    {
        private SnackShopDBContext db = new SnackShopDBContext();

        // GET: Admin/BillDetails


        // GET: Admin/BillDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillDetail billDetail = db.BillDetails.Find(id);
            if (billDetail == null)
            {
                return HttpNotFound();
            }
            return View(billDetail);
        }

        // GET: Admin/BillDetails/Create
        public ActionResult Create(int? idBill)
        {
            
            ViewBag.id_product = new SelectList(db.Products, "id_product", "name");
            if (idBill == null)
            {
                ViewBag.id_bill = new SelectList(db.Bills, "id_bill", "discountCode");
            }
            else
            {
                ViewBag.Bill = db.Bills.Where(p => p.id_bill == idBill).Include(b => b.BillStatus).Include(b => b.Customer).First();
            }
            return View();
        }

        // POST: Admin/BillDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_bill,id_product,price,amount,intoMoney,discriptionProductDetail")] BillDetail billDetail)
        {
            if (ModelState.IsValid)
            {
                db.BillDetails.Add(billDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_bill = new SelectList(db.Bills, "id_bill", "discountCode", billDetail.id_bill);
            ViewBag.id_product = new SelectList(db.Products, "id_product", "name", billDetail.id_product);
            return View(billDetail);
        }

        // GET: Admin/BillDetails/Edit/5
        public ActionResult Edit(int? idBill, int? idProduct)
        {
            if (idBill == null || idProduct == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillDetail billDetail = db.BillDetails.Find(idBill, idProduct);
            if (billDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.bill = db.Bills.Find(idBill);
            ViewBag.id_product = new SelectList(db.Products, "id_product", "name", billDetail.id_product);
            return View(billDetail);
        }

        // POST: Admin/BillDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_bill,id_product,price,amount,intoMoney,discriptionProductDetail")] BillDetail billDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(billDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_bill = new SelectList(db.Bills, "id_bill", "discountCode", billDetail.id_bill);
            ViewBag.id_product = new SelectList(db.Products, "id_product", "name", billDetail.id_product);
            return View(billDetail);
        }

        // GET: Admin/BillDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillDetail billDetail = db.BillDetails.Find(id);
            if (billDetail == null)
            {
                return HttpNotFound();
            }
            return View(billDetail);
        }

        // POST: Admin/BillDetails/Delete/5
        [HttpDelete]
        public ActionResult DeleteBillDetail(int? idBill, int idProduct)
        {
            BillDetail billDetail = db.BillDetails.Find(idBill, idProduct);
            db.BillDetails.Remove(billDetail);
            db.SaveChanges();
            return RedirectToAction("Edit", "Bills", new { @id = idBill });
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
