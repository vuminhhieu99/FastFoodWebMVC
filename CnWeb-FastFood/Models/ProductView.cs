using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CnWeb_FastFood.Models
{
    public class ProductView
    {
        public int? id_product { get; set; }

        public string productName { get; set; }

        public int? id_category { get; set; }

        public string categoryName { get; set; }

        public bool availability { get; set; }

        public decimal? price { get; set; }

        public int? salePercent { get; set; }

        public decimal? salePrice { get; set; }

        public double? rate { get; set; }

        public int? view { get; set; }

        public string mainPhoto { get; set; }

        public DateTime? updated { get; set; }

        public decimal? priceFrom { get; set; }

        public decimal? priceTo { get; set; }

        public int? salePercentFrom { get; set; }

        public int? salePercentTo { get; set; }

        public decimal? salePriceFrom { get; set; }

        public decimal? salePriceTo { get; set; }

        public double? rateFrom { get; set; }

        public double? rateTo { get; set; }
    }
}