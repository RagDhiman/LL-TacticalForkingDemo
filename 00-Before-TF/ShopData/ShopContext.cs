using Microsoft.EntityFrameworkCore;
using ShopDomain;

namespace ShopData
{
    public class ShopContext: DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Account> Address { get; set; }
        public DbSet<Account> BillingAddress { get; set; }
        public DbSet<Account> CreditCard { get; set; }
        public DbSet<Account> Delivery { get; set; }
        public DbSet<Account> Inventory { get; set; }
        public DbSet<Account> Order { get; set; }
        public DbSet<Account> OrderHistory { get; set; }
        public DbSet<Account> Product { get; set; }
        public DbSet<Account> Return { get; set; }
        public DbSet<Account> Stock { get; set; }
        public DbSet<Account> Supplier { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = ShopDatabase"
            );
        }
    }
}