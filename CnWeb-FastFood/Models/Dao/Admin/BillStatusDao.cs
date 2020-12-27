using CnWeb_FastFood.Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CnWeb_FastFood.Models.Dao.Admin
{
    public class BillStatusDao
    {
        SnackShopDBContext db;

        public BillStatusDao()
        {
            db = new SnackShopDBContext();
        }

        public List<BillStatus> ListBillStatus()
        {
            return db.BillStatus.ToList();
        }

        public BillStatus getByID(int id)
        {
            return db.BillStatus.Find(id);
        }
    }
}