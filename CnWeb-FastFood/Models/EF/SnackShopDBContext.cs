namespace CnWeb_FastFood.Models.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SnackShopDBContext : DbContext
    {
        public SnackShopDBContext()
            : base("name=SnackShopDBContext")

        {
           

        }

        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<BillDetail> BillDetails { get; set; }
        public virtual DbSet<BillStatus> BillStatus { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<CartDetail> CartDetails { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<DiscountCode> DiscountCodes { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductDetail> ProductDetails { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserGroup> UserGroups { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bill>()
                .Property(e => e.subtotal)
                .HasPrecision(12, 0);

            modelBuilder.Entity<Bill>()
                .Property(e => e.total)
                .HasPrecision(12, 0);

            modelBuilder.Entity<Bill>()
                .Property(e => e.discountCode)
                .IsUnicode(false);

            modelBuilder.Entity<Bill>()
                .Property(e => e.discount)
                .HasPrecision(12, 0);

            modelBuilder.Entity<Bill>()
                .Property(e => e.phone)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<BillDetail>()
                .Property(e => e.price)
                .HasPrecision(10, 0);

            modelBuilder.Entity<BillDetail>()
                .Property(e => e.intoMoney)
                .HasPrecision(12, 0);

            modelBuilder.Entity<Cart>()
                .Property(e => e.subtotal)
                .HasPrecision(12, 0);

            modelBuilder.Entity<Cart>()
                .Property(e => e.total)
                .HasPrecision(12, 0);

            modelBuilder.Entity<Cart>()
                .Property(e => e.id_discountCode)
                .IsUnicode(false);

            modelBuilder.Entity<Cart>()
                .HasMany(e => e.CartDetails)
                .WithRequired(e => e.Cart)
                .HasForeignKey(e => e.id_cart);

            modelBuilder.Entity<CartDetail>()
                .Property(e => e.price)
                .HasPrecision(10, 0);

            modelBuilder.Entity<CartDetail>()
                .Property(e => e.intoMoney)
                .HasPrecision(12, 0);

            modelBuilder.Entity<Category>()
                .Property(e => e.photo)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Products)
                .WithOptional(e => e.Category)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Customer>()
                .Property(e => e.phone)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.userName)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.subtotalCart)
                .HasPrecision(12, 0);

            modelBuilder.Entity<Customer>()
                .Property(e => e.totalCart)
                .HasPrecision(12, 0);

            modelBuilder.Entity<Customer>()
                .Property(e => e.avatar)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.id_discountCode)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Bills)
                .WithOptional(e => e.Customer)
                .WillCascadeOnDelete();

            modelBuilder.Entity<DiscountCode>()
                .Property(e => e.id_discountCode)
                .IsUnicode(false);

            modelBuilder.Entity<DiscountCode>()
                .Property(e => e.discount)
                .HasPrecision(12, 0);

            modelBuilder.Entity<Product>()
                .Property(e => e.price)
                .HasPrecision(10, 0);

            modelBuilder.Entity<Product>()
                .Property(e => e.salePrice)
                .HasPrecision(10, 0);

            modelBuilder.Entity<Product>()
                .Property(e => e.mainPhoto)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.photo1)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.photo2)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.photo3)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.photo4)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ProductDetails)
                .WithOptional(e => e.Product)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ProductDetail>()
                .Property(e => e.extraPrice)
                .HasPrecision(10, 0);

            modelBuilder.Entity<Role>()
                .Property(e => e.id_role)
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.UserGroups)
                .WithMany(e => e.Roles)
                .Map(m => m.ToTable("Credential").MapLeftKey("id_role").MapRightKey("id_userGroup"));

            modelBuilder.Entity<User>()
                .Property(e => e.userName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.id_userGroup)
                .IsUnicode(false);

            modelBuilder.Entity<UserGroup>()
                .Property(e => e.id_userGroup)
                .IsUnicode(false);
        }
    }
}
