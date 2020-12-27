using CnWeb_FastFood.Models;
using CnWeb_FastFood.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CnWeb_FastFood.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        private SnackShopDBContext db = new SnackShopDBContext();
        public ActionResult Index(string keyword, string catelogyString)
        {
            ViewBag.Keyword = keyword;
            ViewBag.catelogyString = catelogyString;
            IEnumerable<ProductView> list;
            if (catelogyString == "All Categories")
            {
                catelogyString = "";
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                list = db.Database.SqlQuery<ProductView>($"SELECT p.id_product, p.name as productName, p.id_category, c.name as categoryName, p.availability, p.price, p.salePercent, p.salePrice, p.rate, p.[view], p.mainPhoto, p.updated " +
                $"FROM dbo.Product p LEFT JOIN dbo.Category c ON c.id_category = p.id_category where c.[name] LIKE N'%{catelogyString}%'").ToList();
            }

            list = db.Database.SqlQuery<ProductView>($"SELECT p.id_product, p.name as productName, p.id_category, c.name as categoryName, p.availability, p.price, p.salePercent, p.salePrice, p.rate, p.[view], p.mainPhoto, p.updated " +
            $"FROM dbo.Product p LEFT JOIN dbo.Category c ON c.id_category = p.id_category " +
            $"WHERE c.[name] LIKE N'%{catelogyString}%' AND p.name LIKE N'%{keyword}%'").ToList();

            ViewBag.ProductSearchList = list;

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


        public ActionResult ListPriceShow()
        {
            return PartialView(db.Products.ToList());
        }

        public ActionResult SaleOff()
        {
            return PartialView(db.Products.ToList());
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