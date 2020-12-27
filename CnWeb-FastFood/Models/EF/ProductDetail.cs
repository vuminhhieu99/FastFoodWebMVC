namespace CnWeb_FastFood.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductDetail")]
    public partial class ProductDetail
    {
        [Key]
        public int id_productDetail { get; set; }

        [StringLength(200)]
        public string name { get; set; }

        public int? amount { get; set; }

        public bool availability { get; set; }

        public decimal? extraPrice { get; set; }

        public int? id_product { get; set; }

        public virtual Product Product { get; set; }
    }
}
