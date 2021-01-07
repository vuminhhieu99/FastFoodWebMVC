using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CnWeb_FastFood.Areas.Admin.Models;
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

        public List<string> GetListCredential(string id_userGroup)
        {
            var data = (from c in db.Credentials
                       join b in db.UserGroups on c.id_userGroup equals b.id_userGroup
                       join r in db.Roles on c.id_role equals r.id_role
                       where b.id_userGroup == id_userGroup
                       select c.id_role).ToList();

            return data;
        }

        public ResultLogin Login(string userName, string passWord, bool isLoginAdmin = true)
        {
            var result = db.Users.SingleOrDefault(x => x.userName == userName);            ;
            if (result == null)
            {
                return new ResultLogin(false, "Account isn't exist");

            }
            if (isLoginAdmin == true)
            {
                if ((result.id_userGroup == AdminCommonConstants.ADMIN_GROUP || result.id_userGroup == AdminCommonConstants.MEMBER_GROUP))
                {
                    if (result.status == false)
                    {
                        return new ResultLogin(false, "Account is locked");
                    }
                    else
                    {
                        if (result.password == passWord)
                            return new ResultLogin(true, "Success");
                        else
                            return new ResultLogin(false, "Password is wronged");
                    }
                }
                else
                {
                    return new ResultLogin(false, "Account does not have permisson to login");
                }
            }
            else
            {
                if (result.status == false)
                {
                    return new ResultLogin(false, "Account is locked");
                }
                else
                {
                    if (result.password == passWord)
                        return new ResultLogin(true, "Success");
                    else
                        return new ResultLogin(false, "Password is wronged");
                }
            }  
           
        }
    }
}