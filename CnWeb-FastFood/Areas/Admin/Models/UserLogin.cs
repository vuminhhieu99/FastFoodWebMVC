using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CnWeb_FastFood.Areas.Admin.Models
{
    [Serializable]
    public class UserLogin
    {
        public long UserID { set; get; }
        public string UserName { set; get; }
        public string Name { set; get; }
        public DateTime? CreateDate { set; get; }
        public string Avatar { set; get; }
    }
}