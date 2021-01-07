using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CnWeb_FastFood.Areas.Admin.Models
{
    public class ResultLogin
    {
        public ResultLogin(bool status, string message)
        {
            this.status = status;
            this.message = message;
        }
        public bool status;
        public string message;
    }
}