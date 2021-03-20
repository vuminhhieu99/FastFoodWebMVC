using CnWeb_FastFood.Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CnWeb_FastFood.Models.Dao.Admin
{
    public class UserGroupDao
    {
        SnackShopDBContext db = null;
        public UserGroupDao()
        {
            db = new SnackShopDBContext();

        }
        public IEnumerable<UserGroup> ListUserGroup(int PageNum, int PageSize, string searching)
        {
            var list = db.UserGroups.Where(x => x.id_userGroup.Contains(searching) || x.name.Contains(searching)).ToList().ToPagedList<UserGroup>(PageNum, PageSize);
            return list;
        }

        public IEnumerable<UserGroup> ListAll()
        {

            return db.UserGroups.ToList();
        }


        public UserGroup GetByID(string id)
        {
            return db.UserGroups.Find(id);
        }
        public IEnumerable<Role> GetAllRole()
        {
            return db.Roles.ToList();
        }

        public IEnumerable<Credential> GetListCredentialBy(string id_userGroup){
            var data = (from c in db.Credentials
                        join b in db.UserGroups on c.id_userGroup equals b.id_userGroup
                        join r in db.Roles on c.id_role equals r.id_role
                        where b.id_userGroup == id_userGroup
                        select c).ToList();
            return data;
            }
              
    }
}