using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using CnWeb_FastFood.Areas.Admin.Models;
using CnWeb_FastFood.Models.EF;
using PagedList;

namespace CnWeb_FastFood.Models.Dao.Admin
{
    public class UserDao
    {
        SnackShopDBContext db = null;
        public UserDao()
        {
            db = new SnackShopDBContext();

        }
        public bool CheckUserName(String name)
        {
            var check = db.Users.Where(x => x.userName == name).ToList();
            if(check.Count == 0)
            {
                return true;
            }
            return false;
        }

        public User GetById(string userName)
        {
            return db.Users.SingleOrDefault(x => x.userName == userName);
        }
        public IEnumerable<User> ListSimple(string searching)
        {
            var list = db.Database.SqlQuery<User>($"SELECT * FROM dbo.[User] c " +
                $"WHERE c.id_user LIKE N'%{searching}%' " +
                $"OR c.name LIKE N'%{searching}%' " +
                $"OR c.email LIKE N'%{searching}%' " +
                $"OR c.userName LIKE N'%{searching}%'").ToList();


            return list;
        }

        public IEnumerable<User> ListSimpleSearch(int PageNum, int PageSize, string searching)
        {
            
            var list = db.Users.Where(x => x.name.Contains(searching) || x.email.Contains(searching)).ToList().ToPagedList<User>(PageNum, PageSize);

            return list;
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
        public User getByID(int id)
        {
            return db.Users.Find(id);
        }
        public void Add(User User)
        {
            db.Users.Add(User);
            db.SaveChanges();
        }

        public void Edit(User User)
        {
            User ctm = getByID(User.id_user);
            if (ctm != null)
            {
                ctm.name = User.name;
                ctm.email = User.email;
                ctm.password = User.password;
                ctm.userName = User.userName;
                ctm.status = User.status;

                db.Entry(ctm).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public int Delete(int id)
        {
            User User = db.Users.Find(id);
            if (User != null)
            {
                db.Users.Remove(User);
                return db.SaveChanges();
            }
            else
            {
                return -1;
            }
        }
        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
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
                        if (result.password == CreateMD5(passWord))
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