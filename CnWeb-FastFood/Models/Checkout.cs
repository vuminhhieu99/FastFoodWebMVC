using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CnWeb_FastFood.Models
{
    [Serializable]
    public class Checkout
    {
        public List<CartItem> Item { set; get; }
        public int? Id_customer { get; set; }

        public decimal Subtotal { get; set; }        
        public decimal Total { get; set; }
        public string DiscountCode { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public Checkout()
        {
            Subtotal = 0;
            Total = 0;
        }
    }
}