using CnWeb_FastFood.Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CnWeb_FastFood.Models.Dao.Admin
{
    public class DiscountCodeDao
    {
        SnackShopDBContext db;

        public DiscountCodeDao()
        {
            db = new SnackShopDBContext();
        }

        public List<DiscountCode> ListDiscountCode()
        {
            return db.DiscountCodes.ToList();
        }

        public DiscountCode getByID(string id)
        {
            return db.DiscountCodes.Find(id);
        }

        public void Add(DiscountCode discount)
        {
            db.DiscountCodes.Add(discount);
            db.SaveChanges();
        }

        public void Edit(DiscountCode discountCode)
        {
            DiscountCode dcc = getByID(discountCode.id_discountCode);
            if (dcc != null)
            {
                dcc.discount = discountCode.discount;

                db.SaveChanges();
            }
        }

        public int Delete(string id)
        {
            DiscountCode dcc = db.DiscountCodes.Find(id);
            if (dcc != null)
            {
                db.DiscountCodes.Remove(dcc);
                return db.SaveChanges();
            }
            else
            {
                return -1;
            }
        }

        public IEnumerable<DiscountCode> ListSimple(string searching)
        {
            var list = db.Database.SqlQuery<DiscountCode>($"SELECT * FROM dbo.DiscountCode d " +
                $"WHERE d.id_discountCode LIKE N'%{searching}%' OR d.discount LIKE N'%{searching}%'").ToList();


            return list;
        }

        public IEnumerable<DiscountCode> ListSimpleSearch(int PageNum, int PageSize, string searching)
        {
            var list = db.Database.SqlQuery<DiscountCode>($"SELECT * FROM dbo.DiscountCode d " +
                $"WHERE d.id_discountCode LIKE N'%{searching}%' OR d.discount LIKE N'%{searching}%'").ToPagedList<DiscountCode>(PageNum, PageSize);

            return list;
        }

    }
}