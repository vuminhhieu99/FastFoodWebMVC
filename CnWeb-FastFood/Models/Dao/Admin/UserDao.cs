using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CnWeb_FastFood.Models.EF;

namespace CnWeb_FastFood.Models.Dao.Admin
{
    public class UserDao
    {
        SnackShopDBContext db = null;
        public UserDao()
        {
            db = new SnackShopDBContext();

        }
        //public long Insert(entity)
        //{
        //    db.Users.Add(entity);
        //    db.SaveChanges();
        //    return entity.ID;
        //}

        public User GetById(string userName)
        {
            return db.Users.SingleOrDefault(x => x.userName == userName);
        }

        public int Login(string userName, string passWord)
        {
            var result = db.Users.SingleOrDefault(x => x.userName == userName);
            if (result == null)
            {
                return 0;

            }
            else
            {
                if (result.status== false)
                {
                    return -1;
                }
                else
                {
                    if (result.password == passWord)
                        return 1;
                    else
                        return -2;

                }

            }
        }
    }
}