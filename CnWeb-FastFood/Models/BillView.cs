using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CnWeb_FastFood.Models
{
    public class BillView
    {
        public int? id_bill { get; set; }

        public decimal? subtotal { get; set; }

        public decimal? total { get; set; }

        public DateTime? creatDate { get; set; }

        public int? id_customer { get; set; }

        public string customerName { get; set; }

        public string discountCode { get; set; }

        public decimal? discount { get; set; }

        public string address { get; set; }

        public string phone { get; set; }

        public int? id_status { get; set; }

        public string statusName { get; set; }
    }
}