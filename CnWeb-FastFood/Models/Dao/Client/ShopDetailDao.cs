using CnWeb_FastFood.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CnWeb_FastFood.Models.Dao.Client
{
    public class ShopDetailDao : Controller
    {
        SnackShopDBContext db = null;
        public ShopDetailDao()
        {
            db = new SnackShopDBContext();

        }
        public Product GetProduct(int? id)
        {
            return db.Products.Find(id);
        }

        public IEnumerable<ProductDetail> GetProductDetail(int? id)
        {
            return db.ProductDetails.Where(x => x.id_product == id).ToList();
        }


        
        public IEnumerable<Product> GetRelatedProduct(int? idCatelogy)
        {            
            return db.Products.Where(x => x.id_category == idCatelogy).ToList();
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