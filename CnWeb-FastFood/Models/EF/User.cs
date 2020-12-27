namespace CnWeb_FastFood.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        [Key]
        public int id_user { get; set; }

        [Required]
        [StringLength(50)]
        public string userName { get; set; }

        [Required]
        [StringLength(50)]
        public string password { get; set; }

        [StringLength(50)]
        public string name { get; set; }

        [StringLength(50)]
        public string email { get; set; }

        public bool? status { get; set; }

        public DateTime? createDate { get; set; }

        [StringLength(20)]
        public string id_userGroup { get; set; }

        public virtual UserGroup UserGroup { get; set; }
    }
}
