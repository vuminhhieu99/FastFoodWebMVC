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
using System.Windows.Documents;
using CnWeb_FastFood.Areas.Admin.Models;

namespace CnWeb_FastFood.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        SnackShopDBContext db = new SnackShopDBContext();
        ProductDao Pdao = new ProductDao();
        CategoryDao Cdao = new CategoryDao();

        // GET: Admin/Product
        [HasCredential(id_role ="VIEW_USER")]
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
            ViewBag.Count = Pdao.ListSimple(searching).Count();
            ViewBag.CategoryList = new SelectList(db.Categories, "id_category", "name");            
            return View(Pdao.ListSimpleSearch(pageNumber, pagesize, searching));
        }

        public ActionResult Index2(int? page, int? PageSize, string idProduct, string name, string category, string availability, string priceFrom, string priceTo, string salePercentFrom, string salePercentTo, string salePriceFrom, string salePriceTo, string rateFrom, string rateTo)
        {
            ViewBag.IdProduct = idProduct;
            ViewBag.ProductName = name;
            ViewBag.IdCategory = category;
            if (category != "" && category != null)
                ViewBag.CategoryName = Cdao.getByID(Convert.ToInt32(category)).name;
            ViewBag.Availability = availability;
            ViewBag.PriceFrom = priceFrom;
            ViewBag.PriceTo = priceTo;
            ViewBag.SalePercentFrom = salePercentFrom;
            ViewBag.SalePercentTo = salePercentTo;
            ViewBag.SalePriceForm = salePriceFrom;
            ViewBag.SalePriceTo = salePriceTo;
            ViewBag.RateFrom = rateFrom;
            ViewBag.RateTo = rateTo;

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
            ViewBag.Count = Pdao.ListAdvanced(idProduct, name, category, availability, priceFrom, priceTo, salePercentFrom, salePercentTo, salePriceFrom, salePriceTo, rateFrom, rateTo).Count();
            return View(Pdao.ListAdvancedSearch(pageNumber, pagesize, idProduct, name, category, availability, priceFrom, priceTo, salePercentFrom, salePercentTo, salePriceFrom, salePriceTo, rateFrom, rateTo));
        }

        public ActionResult Filter()
        {
            List<Category> ctgr = Cdao.ListCategory();
            SelectList categoryList = new SelectList(ctgr, "id_category", "name", "id_category");
            ViewBag.CategoryList = categoryList;

            return View();
        }

        public ActionResult Details(int id)
        {
            Product product = Pdao.getByID(id);
            return View(product);
        }

       

        public ActionResult Edit(int id)
        {
            List<Category> category = Cdao.ListCategory();
            SelectList categoryList = new SelectList(category, "id_category", "name", "id_category");
            ViewBag.CategoryList = categoryList;

            return View(Pdao.getByID(id));
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "id_product, id_category, name, description, information, review, availability, price, salePercent, salePrice", Exclude = "mainPhoto, photo1, photo2, photo3, photo4")] Product product, HttpPostedFileBase mainPhoto, HttpPostedFileBase photo1, HttpPostedFileBase photo2, HttpPostedFileBase photo3, HttpPostedFileBase photo4, bool? mainPhotoChanged, bool? photo1Changed, bool? photo2Changed, bool? photo3Changed, bool? photo4Changed)
        {            

            if (ModelState.IsValid)
            {
                db.Products.Attach(product);
                db.Entry(product).Property("id_product").IsModified = true;
                db.Entry(product).Property("id_category").IsModified = true;
                db.Entry(product).Property("name").IsModified = true;
                db.Entry(product).Property("description").IsModified = true;
                db.Entry(product).Property("information").IsModified = true;
                db.Entry(product).Property("review").IsModified = true;
                db.Entry(product).Property("availability").IsModified = true;
                db.Entry(product).Property("price").IsModified = true;
                db.Entry(product).Property("salePercent").IsModified = true;
                db.Entry(product).Property("salePrice").IsModified = true;

                if (mainPhotoChanged == true)
                {
                    string oldName = db.Database.SqlQuery<string>($"select mainPhoto from dbo.Product where id_product = {product.id_product}").FirstOrDefault();
                    if(oldName!=null && oldName != "")
                    {
                        deleteImage(oldName);
                    }
                    string newName = addImage(mainPhoto, "mainPhoto");                   
                    product.mainPhoto = mainPhoto.FileName;
                }

                if (photo1Changed == true)
                {
                    string oldName = db.Database.SqlQuery<string>($"select photo1 from dbo.Product where id_product = {product.id_product}").FirstOrDefault();
                    if (oldName != null && oldName != "")
                    {
                        deleteImage(oldName);
                    }
                    string newName = addImage(photo1, "photo1");                   
                    product.photo1 = newName;
                }

                if (photo2Changed == true)
                {
                    string oldName = db.Database.SqlQuery<string>($"select photo2 from dbo.Product where id_product = {product.id_product}").FirstOrDefault();
                    if (oldName != null && oldName != "")
                    {
                        deleteImage(oldName);
                    }
                    string newName = addImage(photo2, "photo2");
                    product.photo2 = newName;
                }
                if (photo3Changed == true)
                {
                    string oldName = db.Database.SqlQuery<string>($"select photo3 from dbo.Product where id_product = {product.id_product}").FirstOrDefault();
                    if (oldName != null && oldName != "")
                    {
                        deleteImage(oldName);
                    }
                    string newName = addImage(photo3, "photo3");
                    product.photo3 = newName;
                }
                if (photo4Changed == true)
                {
                    string oldName = db.Database.SqlQuery<string>($"select photo4 from dbo.Product where id_product = {product.id_product}").FirstOrDefault();
                    if (oldName != null && oldName != "")
                    {
                        deleteImage(oldName);
                    }
                    string newName = addImage(photo4, "photo4");
                    product.photo4 = newName;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Pdao.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult CreateProductJson( [Bind(Include ="id_category, availability, name, price, salePercent, salePrice, description, information, review")] Product product, List<HttpPostedFileBase> files, string setMainPhoto)
        {
            if (ModelState.IsValid)
            {
                product.updated = DateTime.Now;
                product.view = 0;             

                if (files != null)
                {                    
                    if (setMainPhoto == null)
                    {
                        setMainPhoto = files.ElementAt(0).FileName;
                    }

                    //save mainPhoto
                    int index;
                    for(index =0; index < files.Count(); index++)
                    {
                        if(setMainPhoto == files.ElementAt(index).FileName)
                        {
                            string filename = addImage(files.ElementAt(index), "mainPhoto");
                            product.mainPhoto = filename;                           
                            files.RemoveAt(index);
                            break;
                        }
                    }
                    //save photo 1
                    if (files.Count > 0)
                    {
                        string filename = addImage(files.ElementAt(0), "Photo_1");                       
                        product.photo1 = filename;                        
                        files.RemoveAt(0);
                        
                    }
                    //save photo 2
                    if (files.Count > 0)
                    {
                        string filename = addImage(files.ElementAt(0), "Photo_2");
                        product.photo2 = filename;                       
                        files.RemoveAt(0);

                    }
                    //save photo 3
                    if (files.Count > 0)
                    {
                        string filename = addImage(files.ElementAt(0), "Photo_3");
                        product.photo3 = filename;                       
                        files.RemoveAt(0);

                    }
                    //save photo 4
                    if (files.Count > 0)
                    {
                        string filename = addImage(files.ElementAt(0), "Photo_4");
                        product.photo4 = filename;   
                    }
                }
                db.Products.Add(product);
                db.SaveChanges();
                return Json(new { status = true }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = false }, JsonRequestBehavior.AllowGet);
        }

        // id: id của product        
        public string addImage(HttpPostedFileBase files, string extraName )
        {
            if(files != null)
            {
                string[] arrListStr = files.FileName.Split('.');
                string formatFile = arrListStr.ElementAt(arrListStr.Length - 1);
                string createDate = DateTime.Now.ToShortDateString();
                createDate = createDate.Replace("/", "");
                string filename = $"prd_{createDate}_{extraName}" + "." + formatFile;
                var path = Server.MapPath("~/Areas/Admin/Content/Photos/") + filename;
                files.SaveAs(path);
                return filename;
            }
            return "";

                
         
            
        }
        private bool deleteImage(string oldName)
        {
            var filePath = Server.MapPath("~/Areas/Admin/Content/Photos/") + oldName;
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
                return true;
            }
            return false;
        }
    }
}