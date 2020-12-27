using CnWeb_FastFood.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CnWeb_FastFood.Models.Dao.Admin
{
    public class BillDetailDao
    {
        SnackShopDBContext db;

        public BillDetailDao()
        {
            db = new SnackShopDBContext();
        }

        public List<BillDetail> getListBillDetailByIdBill(int id)
        {
            return db.BillDetails.Where(x => x.id_bill == id).ToList();
        }
    }
}