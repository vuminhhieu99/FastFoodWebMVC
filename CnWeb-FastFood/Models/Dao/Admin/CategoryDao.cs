using CnWeb_FastFood.Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CnWeb_FastFood.Models.Dao.Admin
{
    public class CategoryDao
    {
        SnackShopDBContext db;

        public CategoryDao()
        {
            db = new SnackShopDBContext();
        }

        public List<Category> ListCategory()
        {
            return db.Categories.ToList();
        }

        public Category getByID(int id)
        {
            return db.Categories.Find(id);
        }

        public void Add(Category category)
        {
            db.Categories.Add(category);
            db.SaveChanges();
        }

        public void Edit(Category category)
        {
            Category ctgr = getByID(category.id_category);
            if(ctgr!=null)
            {
                ctgr.name = category.name;
                db.SaveChanges();
            }
        }

        public int Delete(int id)
        {
            Category category = db.Categories.Find(id);
            if(category!=null)
            {
                db.Categories.Remove(category);
                return db.SaveChanges();
            }
            else
            {
                return -1;
            }
        }

        public IEnumerable<Category> ListSimple(string searching)
        {
            var list = db.Database.SqlQuery<Category>($"SELECT * FROM dbo.Category c " +
                $"WHERE c.id_category LIKE N'%{searching}%' OR c.name LIKE N'%{searching}%'").ToList();
          

            return list;
        }

        public IEnumerable<Category> ListSimpleSearch(int PageNum, int PageSize, string searching)
        {
            var list = db.Database.SqlQuery<Category>($"SELECT * FROM dbo.Category c " +
                $"WHERE c.id_category LIKE N'%{searching}%' OR c.name LIKE N'%{searching}%'").ToPagedList<Category>(PageNum, PageSize);

            return list;
        }

        
    }
}