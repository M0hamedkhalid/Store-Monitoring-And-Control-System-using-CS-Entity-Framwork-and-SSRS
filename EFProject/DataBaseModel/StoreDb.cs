using System.Data.Entity;

namespace EFProject.DataBaseModel
{
    public partial class StoreDb : DbContext
    {
        public StoreDb()
            : base("name=StoreDb")
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerBill> CustomerBills { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductUnit> ProductUnits { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<SupplierBill> SupplierBills { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Warehouse> Warehouses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .Property(e => e.Name)
                .IsFixedLength();

            modelBuilder.Entity<Customer>()
                .Property(e => e.Address)
                .IsFixedLength();

            modelBuilder.Entity<Customer>()
                .Property(e => e.Mail)
                .IsFixedLength();

            modelBuilder.Entity<Customer>()
                .Property(e => e.Website)
                .IsFixedLength();

            modelBuilder.Entity<Product>()
                .Property(e => e.Name)
                .IsFixedLength();

            modelBuilder.Entity<ProductUnit>()
                .Property(e => e.Unit)
                .IsFixedLength();

            modelBuilder.Entity<Supplier>()
                .Property(e => e.Name)
                .IsFixedLength();

            modelBuilder.Entity<Supplier>()
                .Property(e => e.Address)
                .IsFixedLength();

            modelBuilder.Entity<Supplier>()
                .Property(e => e.Mail)
                .IsFixedLength();

            modelBuilder.Entity<Supplier>()
                .Property(e => e.Website)
                .IsFixedLength();

            modelBuilder.Entity<Warehouse>()
                .Property(e => e.Name)
                .IsFixedLength();

            modelBuilder.Entity<Warehouse>()
                .Property(e => e.Address)
                .IsFixedLength();

            modelBuilder.Entity<Warehouse>()
                .Property(e => e.MgrName)
                .IsFixedLength();
        }
    }
}