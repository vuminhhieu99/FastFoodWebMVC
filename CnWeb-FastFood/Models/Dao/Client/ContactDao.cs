using CnWeb_FastFood.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CnWeb_FastFood.Models.Dao.Client
{
    public class ContactDao
    {
        SnackShopDBContext db = null;
        public ContactDao()
        {
            db = new SnackShopDBContext();

        }
    }
}