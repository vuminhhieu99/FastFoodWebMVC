namespace CnWeb_FastFood.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            BillDetails = new HashSet<BillDetail>();
            CartDetails = new HashSet<CartDetail>();
            ProductDetails = new HashSet<ProductDetail>();
        }

        [Key]
        public int id_product { get; set; }

        [StringLength(200)]
        public string name { get; set; }

        public string description { get; set; }

        public string information { get; set; }

        public string review { get; set; }

        public int? view { get; set; }

        public bool availability { get; set; }

        public decimal? price { get; set; }

        public int? salePercent { get; set; }

        public decimal? salePrice { get; set; }

        public double? rate { get; set; }

        [StringLength(100)]
        public string mainPhoto { get; set; }

        [StringLength(100)]
        public string photo1 { get; set; }

        [StringLength(100)]
        public string photo2 { get; set; }

        [StringLength(100)]
        public string photo3 { get; set; }

        [StringLength(100)]
        public string photo4 { get; set; }

        [Column(TypeName = "date")]
        public DateTime? updated { get; set; }

        public int? id_category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BillDetail> BillDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CartDetail> CartDetails { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductDetail> ProductDetails { get; set; }
    }
}
