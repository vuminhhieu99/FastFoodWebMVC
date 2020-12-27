using CnWeb_FastFood.Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace CnWeb_FastFood.Models.Dao.Admin
{
    public class ProductDao
    {
        SnackShopDBContext db;

        public ProductDao()
        {
            db = new SnackShopDBContext();
        }

        public List<Product> ListProduct()
        {
            return db.Products.ToList();
        }

        public Product getByID(int id)
        {
            return db.Products.Find(id);
        }

        public void Add(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
        }

        public void Edit(Product product)
        {
            Product prd = getByID(product.id_product);
            if (prd != null)
            {
                prd.name = product.name;
                prd.description = product.description;
                prd.information = product.information;
                prd.view = product.view;
                prd.availability = product.availability;
                prd.price = product.price;
                prd.salePercent = product.salePercent;
                prd.salePrice = product.salePrice;
                prd.rate = product.rate;
                prd.mainPhoto = product.mainPhoto;
                prd.photo1 = product.photo1;
                prd.photo2 = product.photo2;
                prd.photo3 = product.photo3;
                prd.photo4 = product.photo4;
                prd.updated = product.updated;
                prd.id_category = product.id_category;

                db.SaveChanges();
            }
        }

        public int Delete(int id)
        {
            Product product = db.Products.Find(id);
            if (product != null)
            {
                db.Products.Remove(product);
                return db.SaveChanges();
            }
            else
            {
                return -1;
            }
        }

        public bool ChangeAvailability( long id)
        {
            var user = db.Products.Find(id);
            user.availability = !user.availability;
            db.SaveChanges();
            return user.availability;
        }
       
        public IEnumerable<Product> ListProductPage(int PageNum, int PageSize)
        {
            return db.Products.OrderBy(p => p.id_product).ToPagedList(PageNum, PageSize);
        }

        public IEnumerable<ProductView> ListSimple(string searching)
        {
            var list = db.Database.SqlQuery<ProductView>($"SELECT p.id_product, p.name as productName, c.name as categoryName, p.availability, p.price, p.salePercent, p.salePrice, p.rate, p.[view], p.mainPhoto, p.updated " +
                $"FROM dbo.Product p LEFT JOIN dbo.Category c ON c.id_category = p.id_category " +
                $"WHERE p.id_product LIKE N'%{searching}%' " +
                $"OR p.name LIKE N'%{searching}%' " +
                $"OR c.name LIKE N'%{searching}%' " +
                $"OR p.availability LIKE N'%{searching}%' " +
                $"OR p.price LIKE N'%{searching}%' " +
                $"OR p.salePercent LIKE N'%{searching}%' " +
                $"OR p.salePrice LIKE N'%{searching}%' " +
                $"OR p.rate LIKE N'%{searching}%' " +
                $"OR p.updated LIKE N'%{searching}%'").ToList();

            return list;
        }

        public IEnumerable<ProductView> ListSimpleSearch(int PageNum, int PageSize, string searching)
        {
            var list = db.Database.SqlQuery<ProductView>($"SELECT p.id_product, p.name as productName, c.name as categoryName, p.availability, p.price, p.salePercent, p.salePrice, p.rate, p.[view], p.mainPhoto, p.updated " +
                $"FROM dbo.Product p LEFT JOIN dbo.Category c ON c.id_category = p.id_category " +
                $"WHERE p.id_product LIKE N'%{searching}%' " +
                $"OR p.name LIKE N'%{searching}%' " +
                $"OR c.name LIKE N'%{searching}%' " +
                $"OR p.availability LIKE N'%{searching}%' " +
                $"OR p.price LIKE N'%{searching}%' " +
                $"OR p.salePercent LIKE N'%{searching}%' " +
                $"OR p.salePrice LIKE N'%{searching}%' " +
                $"OR p.rate LIKE N'%{searching}%' " +
                $"OR p.updated LIKE N'%{searching}%'").ToPagedList<ProductView>(PageNum, PageSize);

            return list;
        }

        public IEnumerable<ProductView> ListAdvanced(string idProduct, string name, string category, string availability, string priceFrom, string priceTo, string salePercentFrom, string salePercentTo, string salePriceFrom, string salePriceTo, string rateFrom, string rateTo)
        {
            string querySearch = "SELECT p.id_product, p.name as productName, c.name as categoryName, p.availability, p.price, p.salePercent, p.salePrice, p.rate, p.[view], p.mainPhoto, p.updated " +
                 $"FROM dbo.Product p LEFT JOIN dbo.Category c ON c.id_category = p.id_category ";

            string queryCondition = "";
            if (idProduct != "" && idProduct != null)
            {
                queryCondition += $" AND p.id_product LIKE N'%{idProduct}%'";
            }
            if (name != "" && name != null)
            {
                queryCondition += $" AND p.name LIKE N'%{name}%'";
            }
            if (category != "" && category != null)
            {
                queryCondition += $" AND p.id_category = {category}";
            }
            if (availability != "" && availability != null)
            {
                if (availability == "True")
                    queryCondition += $" AND p.availability = 1";
                else
                    queryCondition += $" AND p.availability = 0";
            }
            if (priceFrom != null && priceTo != null && priceFrom != "" && priceTo != "" && Convert.ToDecimal(priceFrom) <= Convert.ToDecimal(priceTo))
            {
                queryCondition += $" AND p.price >= {priceFrom} AND p.price <= {priceTo}";
            }
            if (salePercentFrom != null && salePercentTo != null && salePercentFrom != "" && salePercentTo != "" && Convert.ToInt32(salePercentFrom) <= Convert.ToInt32(salePercentTo))
            {
                queryCondition += $" AND p.salePercent >= {salePercentFrom} AND p.salePercent <= {salePercentTo}";
            }
            if (salePriceFrom != null && salePriceTo != null && salePriceFrom != "" && salePriceTo != "" && Convert.ToDecimal(salePriceFrom) <= Convert.ToDecimal(salePriceTo))
            {
                queryCondition += $" AND p.salePrice >= {salePriceFrom} AND p.salePrice <= {salePriceTo}";
            }
            if (rateFrom != null && rateTo != null && rateFrom != "" && rateTo != null && Convert.ToDouble(rateFrom) <= Convert.ToDouble(rateTo))
            {
                queryCondition += $" AND p.rate >= {rateFrom} AND p.rate<={rateTo}";
            }

            if (!queryCondition.Equals(""))
            {
                querySearch = querySearch + " WHERE" + queryCondition.Remove(0, 4);
            }

            var list = db.Database.SqlQuery<ProductView>(querySearch).ToList();

            return list;
        }

        public IEnumerable<ProductView> ListAdvancedSearch(int PageNum, int PageSize, string idProduct, string name, string category, string availability, string priceFrom, string priceTo, string salePercentFrom, string salePercentTo, string salePriceFrom, string salePriceTo, string rateFrom, string rateTo)
        {
            string querySearch = "SELECT p.id_product, p.name as productName, c.name as categoryName, p.availability, p.price, p.salePercent, p.salePrice, p.rate , p.[view], p.mainPhoto, p.updated " +
                 $"FROM dbo.Product p LEFT JOIN dbo.Category c ON c.id_category = p.id_category ";

            string queryCondition = "";
            if (idProduct != "" && idProduct != null)
            {
                queryCondition += $" AND p.id_product LIKE N'%{idProduct}%'";
            }
            if (name != "" && name != null)
            {
                queryCondition += $" AND p.name LIKE N'%{name}%'";
            }
            if (category != "" && category != null)
            {
                queryCondition += $" AND p.id_category = {category}";
            }
            if (availability != "" && availability != null)
            {
                if (availability == "True")
                    queryCondition += $" AND p.availability = 1";
                else
                    queryCondition += $" AND p.availability = 0";
            }
            if (priceFrom != null && priceTo != null && priceFrom != "" && priceTo != "" && Convert.ToDecimal(priceFrom) <= Convert.ToDecimal(priceTo))
            {
                queryCondition += $" AND p.price >= {priceFrom} AND p.price <= {priceTo}";
            }
            if (salePercentFrom != null && salePercentTo != null && salePercentFrom != "" && salePercentTo != "" && Convert.ToInt32(salePercentFrom) <= Convert.ToInt32(salePercentTo))
            {
                queryCondition += $" AND p.salePercent >= {salePercentFrom} AND p.salePercent <= {salePercentTo}";
            }
            if (salePriceFrom != null && salePriceTo != null && salePriceFrom != "" && salePriceTo != "" && Convert.ToDecimal(salePriceFrom) <= Convert.ToDecimal(salePriceTo))
            {
                queryCondition += $" AND p.salePrice >= {salePriceFrom} AND p.salePrice <= {salePriceTo}";
            }
            if (rateFrom != null && rateTo != null && rateFrom != "" && rateTo != "" && Convert.ToDouble(rateFrom) <= Convert.ToDouble(rateTo))
            {
                queryCondition += $" AND p.rate >= {rateFrom} AND p.rate<={rateTo}";
            }

            if (!queryCondition.Equals(""))
            {
                querySearch = querySearch + " WHERE" + queryCondition.Remove(0, 4);
            }

            var list = db.Database.SqlQuery<ProductView>(querySearch).ToPagedList<ProductView>(PageNum, PageSize);

            return list;
        }

       
    }
}