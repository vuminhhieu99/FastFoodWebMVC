using CnWeb_FastFood.Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CnWeb_FastFood.Models.Dao.Admin
{
    public class CustomerDao
    {
        SnackShopDBContext db;

        public CustomerDao()
        {
            db = new SnackShopDBContext();
        }

        public List<Customer> ListCustomer()
        {
            return db.Customers.ToList();
        }

        public Customer getByID(int id)
        {
            return db.Customers.Find(id);
        }

        public void Add(Customer customer)
        {
            db.Customers.Add(customer);
            db.SaveChanges();
        }

        public void Edit(Customer customer)
        {
            Customer ctm = getByID(customer.id_customer);
            if (ctm != null)
            {
                ctm.name = customer.name;
                ctm.phone = customer.phone;
                ctm.address = customer.address;
                ctm.userName = customer.userName;
                ctm.password = customer.password;
                ctm.subtotalCart = customer.subtotalCart;
                ctm.totalCart = customer.totalCart;
                ctm.id_discountCode = customer.id_discountCode;

                db.Entry(ctm).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public int Delete(int id)
        {
            Customer customer = db.Customers.Find(id);
            if (customer != null)
            {
                db.Customers.Remove(customer);
                return db.SaveChanges();
            }
            else
            {
                return -1;
            }
        }

        public IEnumerable<Customer> ListSimple(string searching)
        {
            var list = db.Database.SqlQuery<Customer>($"SELECT * FROM dbo.Customer c " +
                $"WHERE c.id_customer LIKE N'%{searching}%' " +
                $"OR c.name LIKE N'%{searching}%' " +
                $"OR c.phone LIKE N'%{searching}%' " +
                $"OR c.address LIKE N'%{searching}%' " +
                $"OR c.userName LIKE N'%{searching}%'").ToList();


            return list;
        }

        public IEnumerable<Customer> ListSimpleSearch(int PageNum, int PageSize, string searching)
        {
            var list = db.Database.SqlQuery<Customer>($"SELECT * FROM dbo.Customer c " +
               $"WHERE c.id_customer LIKE N'%{searching}%' " +
               $"OR c.name LIKE N'%{searching}%' " +
               $"OR c.phone LIKE N'%{searching}%' " +
               $"OR c.address LIKE N'%{searching}%' " +
               $"OR c.userName LIKE N'%{searching}%'").ToPagedList<Customer>(PageNum, PageSize);

            return list;
        }

        public IEnumerable<Customer> ListAdvanced(string idCustomer, string name, string phone, string address, string username)
        {
            string querySearch = "SELECT * FROM dbo.Customer c ";

            string queryCondition = "";
            if (idCustomer != "" && idCustomer != null)
            {
                queryCondition += $" AND c.id_customer LIKE N'%{idCustomer}%'";
            }
            if (name != "" && name != null)
            {
                queryCondition += $" AND c.name LIKE N'%{name}%'";
            }
            if (phone != "" && phone != null)
            {
                queryCondition += $" AND c.phone LIKE N'%{phone}%'";
            }
            if (address != "" && address != null)
            {
                queryCondition += $" AND c.address LIKE N'%{address}%'";
            }
            if (username != "" && username != null)
            {
                queryCondition += $" AND c.userName LIKE N'%{username}%'";
            }

            if (!queryCondition.Equals(""))
            {
                querySearch = querySearch + " WHERE" + queryCondition.Remove(0, 4);
            }

            var list = db.Database.SqlQuery<Customer>(querySearch).ToList();

            return list;
        }

        public IEnumerable<Customer> ListAdvancedSearch(int PageNum, int PageSize, string idCustomer, string name, string phone, string address, string username)
        {
            string querySearch = "SELECT * FROM dbo.Customer c ";

            string queryCondition = "";
            if (idCustomer != "" && idCustomer != null)
            {
                queryCondition += $" AND c.id_customer LIKE N'%{idCustomer}%'";
            }
            if (name != "" && name != null)
            {
                queryCondition += $" AND c.name LIKE N'%{name}%'";
            }
            if (phone != "" && phone != null)
            {
                queryCondition += $" AND c.phone LIKE N'%{phone}%'";
            }
            if (address != "" && address != null)
            {
                queryCondition += $" AND c.address LIKE N'%{address}%'";
            }
            if (username != "" && username != null)
            {
                queryCondition += $" AND c.userName LIKE N'%{username}%'";
            }

            if (!queryCondition.Equals(""))
            {
                querySearch = querySearch + " WHERE" + queryCondition.Remove(0, 4);
            }

            var list = db.Database.SqlQuery<Customer>(querySearch).ToPagedList<Customer>(PageNum, PageSize);

            return list;
        }
    }
}