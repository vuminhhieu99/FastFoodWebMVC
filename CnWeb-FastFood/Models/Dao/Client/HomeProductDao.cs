using CnWeb_FastFood.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CnWeb_FastFood.Models.Dao.Client
{
    public class HomeProductDao
    {
        SnackShopDBContext db;

        public HomeProductDao()
        {
            db = new SnackShopDBContext();
        }

        public List<string> ListName(string keyword)
        {
            return db.Products.Where(x => x.name.Contains(keyword)).Select(x => x.name).ToList();
        }

        public List<ProductView> Search(string keyword)
        {
            var list = db.Database.SqlQuery<ProductView>($"SELECT p.id_product, p.name as productName, p.id_category, c.name as categoryName, p.availability, p.price, p.salePercent, p.salePrice, p.rate, p.mainPhoto, p.updated " +
                $"FROM dbo.Product p LEFT JOIN dbo.Category c ON c.id_category = p.id_category " +
                $"WHERE c.[name] LIKE N'%{keyword}%' AND p.id_product LIKE N'%{keyword}%' " +
                $"OR p.name LIKE N'%{keyword}%' " +
                $"OR c.name LIKE N'%{keyword}%' " +
                $"OR p.availability LIKE N'%{keyword}%' " +
                $"OR p.price LIKE N'%{keyword}%' " +
                $"OR p.salePercent LIKE N'%{keyword}%' " +
                $"OR p.salePrice LIKE N'%{keyword}%' " +
                $"OR p.rate LIKE N'%{keyword}%' " +
                $"OR p.updated LIKE N'%{keyword}%'").ToList();

            return list;
        }
    }
}