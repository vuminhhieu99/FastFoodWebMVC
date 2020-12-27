using CnWeb_FastFood.Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CnWeb_FastFood.Models.Dao.Admin
{
    public class BillDao
    {
        SnackShopDBContext db;

        public BillDao()
        {
            db = new SnackShopDBContext();
        }

        public List<Bill> ListBill()
        {
            return db.Bills.ToList();
        }

        public Bill getByID(int id)
        {
            return db.Bills.Find(id);
        }

        public void Add(Bill bill)
        {
            db.Bills.Add(bill);
            db.SaveChanges();
        }

        public void Edit(Bill bill)
        {
            Bill b = getByID(bill.id_bill);
            if (b != null)
            {
                b.id_bill = bill.id_bill;
                b.subtotal = bill.subtotal;
                b.total = bill.total;
                b.creatDate = bill.creatDate;
                b.id_customer = bill.id_customer;
                b.discountCode = bill.discountCode;
                b.discount = bill.discount;
                b.address = bill.address;
                b.phone = bill.phone;
                b.id_status = bill.id_status;

                db.SaveChanges();
            }
        }

        public int Delete(int id)
        {
            Bill bill = db.Bills.Find(id);
            if (bill != null)
            {
                db.Bills.Remove(bill);
                return db.SaveChanges();
            }
            else
            {
                return -1;
            }
        }

        public IEnumerable<BillView> ListSimple(string searching)
        {
            var list = db.Database.SqlQuery<BillView>($"SELECT b.id_bill, c.name AS customerName, b.phone, b.address, b.discountCode, b.discount, b.subtotal, b.total, b.creatDate, bs.status AS statusName " +
                $"FROM dbo.Bill b, dbo.Customer c, dbo.BillStatus bs " +
                $"WHERE b.id_customer = c.id_customer AND b.id_status = bs.id_status " +
                $"AND (b.id_bill LIKE N'%{searching}%' " +
                $"OR c.name LIKE N'%{searching}%' " +
                $"OR b.phone LIKE N'%{searching}%' " +
                $"OR b.address LIKE N'%{searching}%' " +
                $"OR b.discountCode LIKE N'%{searching}%' " +
                $"OR b.discount LIKE N'%{searching}%' " +
                $"OR b.subtotal LIKE N'%{searching}%' " +
                $"OR b.total LIKE N'%{searching}%' " +
                $"OR bs.status LIKE N'%{searching}%')").ToList();

            return list;
        }

        public IEnumerable<BillView> ListSimpleSearch(int PageNum, int PageSize, string searching)
        {
            var list = db.Database.SqlQuery<BillView>($"SELECT b.id_bill, c.name AS customerName, b.phone, b.address, b.discountCode, b.discount, b.subtotal, b.total, b.creatDate, b.id_status, bs.status AS statusName " +
                $"FROM dbo.Bill b, dbo.Customer c, dbo.BillStatus bs " +
                $"WHERE b.id_customer = c.id_customer AND b.id_status = bs.id_status " +
                $"AND (b.id_bill LIKE N'%{searching}%' " +
                $"OR c.name LIKE N'%{searching}%' " +
                $"OR b.phone LIKE N'%{searching}%' " +
                $"OR b.address LIKE N'%{searching}%' " +
                $"OR b.discountCode LIKE N'%{searching}%' " +
                $"OR b.discount LIKE N'%{searching}%' " +
                $"OR b.subtotal LIKE N'%{searching}%' " +
                $"OR b.total LIKE N'%{searching}%' " +
                $"OR bs.status LIKE N'%{searching}%')").ToPagedList<BillView>(PageNum, PageSize);

            return list;
        }

        public IEnumerable<BillView> ListAdvanced(string idBill, string customerName, string phone, string address, string discountCode, string discountFrom, string discountTo, string subtotalFrom, string subtotalTo, string totalFrom, string totalTo, string status)
        {
            string querySearch = $"SELECT b.id_bill, c.name AS customerName, b.phone, b.address, b.discountCode, b.discount, b.subtotal, b.total, b.creatDate, bs.status AS statusName " +
                $"FROM dbo.Bill b, dbo.Customer c, dbo.BillStatus bs " +
                $"WHERE b.id_customer = c.id_customer AND b.id_status = bs.id_status";

            string queryCondition = "";
            if (idBill != "" && idBill != null)
            {
                queryCondition += $" AND b.id_bill LIKE N'%{idBill}%'";
            }
            if (customerName != "" && customerName != null)
            {
                queryCondition += $" AND c.name LIKE N'%{customerName}%'";
            }
            if (phone != "" && phone != null)
            {
                queryCondition += $" AND b.id_category LIKE N'%{phone}%'";
            }
            if (address != "" && address != null)
            {
                queryCondition += $" AND b.address LIKE N'%{address}%'";
            }
            if (discountCode != "" && discountCode != null)
            {
                queryCondition += $" AND b.discountCode LIKE N'%{discountCode}%'";
            }
            if (discountFrom != null && discountTo != null && discountFrom != "" && discountTo != "" && Convert.ToDecimal(discountFrom) <= Convert.ToDecimal(discountTo))
            {
                queryCondition += $" AND p.discount >= {discountFrom} AND p.discount <= {discountTo}";
            }
            if (subtotalFrom != null && subtotalTo != null && subtotalFrom != "" && subtotalTo != "" && Convert.ToDecimal(subtotalFrom) <= Convert.ToDecimal(subtotalTo))
            {
                queryCondition += $" AND p.subtotal >= {subtotalFrom} AND p.subtotal <= {subtotalTo}";
            }
            if (totalFrom != null && totalTo != null && totalFrom != "" && totalTo != "" && Convert.ToDecimal(totalFrom) <= Convert.ToDecimal(totalTo))
            {
                queryCondition += $" AND p.total >= {totalFrom} AND p.total <= {totalTo}";
            }
            if (status != "" && status != null)
            {
                queryCondition += $" AND b.id_status = {status}";
            }

            if (!queryCondition.Equals(""))
            {
                querySearch = querySearch + queryCondition;
            }

            var list = db.Database.SqlQuery<BillView>(querySearch).ToList();

            return list;
        }

        public IEnumerable<BillView> ListAdvancedSearch(int PageNum, int PageSize, string idBill, string customerName, string phone, string address, string discountCode, string discountFrom, string discountTo, string subtotalFrom, string subtotalTo, string totalFrom, string totalTo, string status)
        {
            string querySearch = $"SELECT b.id_bill, c.name AS customerName, b.phone, b.address, b.discountCode, b.discount, b.subtotal, b.total, b.creatDate, bs.status AS statusName " +
                $"FROM dbo.Bill b, dbo.Customer c, dbo.BillStatus bs " +
                $"WHERE b.id_customer = c.id_customer AND b.id_status = bs.id_status";

            string queryCondition = "";
            if (idBill != "" && idBill != null)
            {
                queryCondition += $" AND b.id_bill LIKE N'%{idBill}%'";
            }
            if (customerName != "" && customerName != null)
            {
                queryCondition += $" AND c.name LIKE N'%{customerName}%'";
            }
            if (phone != "" && phone != null)
            {
                queryCondition += $" AND b.id_category LIKE N'%{phone}%'";
            }
            if (address != "" && address != null)
            {
                queryCondition += $" AND b.address LIKE N'%{address}%'";
            }
            if (discountCode != "" && discountCode != null)
            {
                queryCondition += $" AND b.discountCode LIKE N'%{discountCode}%'";
            }
            if (discountFrom != null && discountTo != null && discountFrom != "" && discountTo != "" && Convert.ToDecimal(discountFrom) <= Convert.ToDecimal(discountTo))
            {
                queryCondition += $" AND p.discount >= {discountFrom} AND p.discount <= {discountTo}";
            }
            if (subtotalFrom != null && subtotalTo != null && subtotalFrom != "" && subtotalTo != "" && Convert.ToDecimal(subtotalFrom) <= Convert.ToDecimal(subtotalTo))
            {
                queryCondition += $" AND p.subtotal >= {subtotalFrom} AND p.subtotal <= {subtotalTo}";
            }
            if (totalFrom != null && totalTo != null && totalFrom != "" && totalTo != "" && Convert.ToDecimal(totalFrom) <= Convert.ToDecimal(totalTo))
            {
                queryCondition += $" AND p.total >= {totalFrom} AND p.total <= {totalTo}";
            }
            if (status != "" && status != null)
            {
                queryCondition += $" AND b.id_status = {status}";
            }

            if (!queryCondition.Equals(""))
            {
                querySearch = querySearch + queryCondition;
            }

            var list = db.Database.SqlQuery<BillView>(querySearch).ToPagedList<BillView>(PageNum, PageSize);

            return list;
        }

        internal object BillDetail(int? id)
        {
            throw new NotImplementedException();
        }
    }
}
