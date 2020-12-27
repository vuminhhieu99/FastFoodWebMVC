using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CnWeb_FastFood.Models.EF;


namespace CnWeb_FastFood.Models.Dao.Client
{
    public class CustomerDao
    {
        SnackShopDBContext db = null;
        public CustomerDao()
        {
            db = new SnackShopDBContext();

        }
        //public long Insert(entity)
        //{
        //    db.Users.Add(entity);
        //    db.SaveChanges();
        //    return entity.ID;
        //}

        public Customer GetById(string userName)
        {
            return db.Customers.SingleOrDefault(x => x.userName == userName);
        }

        public int Login(string userName, string passWord)
        {
            var result = db.Customers.SingleOrDefault(x => x.userName == userName);
            if (result == null)
            {
                return 0;

            }
            else
            {
                if(result.password == passWord)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
        }
    }
}