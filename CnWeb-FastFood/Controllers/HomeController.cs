using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CnWeb_FastFood.Models;
using CnWeb_FastFood.Models.Dao.Client;
using CnWeb_FastFood.Models.EF;
using PagedList;

namespace CnWeb_FastFood.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        private SnackShopDBContext db = new SnackShopDBContext();
        public ActionResult Index(string keyword, string catelogyString, int? page, int? PageSize)
        {
            ViewBag.Keyword = keyword;
            ViewBag.catelogyString = catelogyString;
            int pageNumber = (page ?? 1);
            int pagesize = (PageSize ?? 10);
            IEnumerable<ProductView> list;
            if (catelogyString == "All Categories")
            {
                catelogyString = "";
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                list = db.Database.SqlQuery<ProductView>($"SELECT p.id_product, p.name as productName, p.id_category, c.name as categoryName, p.availability, p.price, p.salePercent, p.salePrice, p.rate, p.[view] , p.mainPhoto, p.updated " +
                $"FROM dbo.Product p LEFT JOIN dbo.Category c ON c.id_category = p.id_category where c.[name] LIKE N'%{catelogyString}%'").ToPagedList<ProductView>(pageNumber, pagesize); 
            }
           
            list = db.Database.SqlQuery<ProductView>($"SELECT p.id_product, p.name as productName, p.id_category, c.name as categoryName, p.availability, p.price, p.salePercent, p.salePrice, p.rate, p.[view], p.mainPhoto, p.updated " +
            $"FROM dbo.Product p LEFT JOIN dbo.Category c ON c.id_category = p.id_category " +
            $"WHERE c.[name] LIKE N'%{catelogyString}%' AND p.name LIKE N'%{keyword}%'").ToPagedList<ProductView>(pageNumber, pagesize);

            ViewBag.psize = pagesize;
            ViewBag.ProductSearchList = list;
            ViewBag.ProductAllList = db.Products.ToList();
            ViewBag.Count = db.Products.Count();
            return View(list);
        }
        public ActionResult CategoryShow()
        {
            return PartialView(db.Categories.ToList());
        }
        public ActionResult CategoryShowImage()
        {
            return PartialView(db.Categories.ToList());
        }
        public ActionResult ListCategoryShow()
        {
            return PartialView(db.Categories.ToList());
        }
        
        public ActionResult LatestProducts()
        {
            return PartialView(db.Products.OrderByDescending(p => p.updated).Take(9));
        }

        public ActionResult TopRatedProducts()
        {
            return PartialView(db.Products.OrderByDescending(p => p.rate).Take(9));
        }

        public ActionResult TopViewProducts()
        {
            return PartialView(db.Products.OrderByDescending(p => p.view).Take(9));
        }

        public JsonResult ListName(string q)
        {
            var data = new HomeProductDao().ListName(q);
            return Json(new
            {
                data = data,
                status = true
            }, JsonRequestBehavior.AllowGet);
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