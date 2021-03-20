using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CnWeb_FastFood.Models
{
    public class StatisticView
    {
        public int productTotal;
        public float saleByDay;
        public float saleByMonth;
        public float saleByYear;
        public List<int?> mapStatictis = new List<int?>();
        public int totalCustomer;
        public int totalBill;
    }
    

}