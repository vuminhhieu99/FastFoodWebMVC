using CnWeb_FastFood.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CnWeb_FastFood.Models
{
    [Serializable]
    public class CartItem
    {
        public Product Products { set; get; }
        public int Amount { set; get; }
        public decimal Price { set; get; }
        public decimal IntoMoney { set; get; }
        public string discriptionProductDetail { get; set; }

        public CartItem()
        {
            Amount = 0;
            Price = 0;
            IntoMoney = 0;
        }
    }
}