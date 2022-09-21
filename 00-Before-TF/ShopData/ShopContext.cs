using Microsoft.EntityFrameworkCore;
using ShopDomain;

namespace ShopData
{
    public class ShopContext : DbContext
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var myAccounts = new List<Account>() {
                new Account() { AccountId=1, Name = "Mr Smith", EmailAddress = "Smith@tacticalforking.com", CreatedDate = new DateTime(2023,12,01) },
                new Account() { AccountId=2, Name = "Mr Jones", EmailAddress = "Jones@tacticalforking.com", CreatedDate = new DateTime(2023,12,02) }
            };

            modelBuilder.Entity<Account>().HasData(myAccounts);

            var myAddresses = new List<Address>() {
                new Address() { AddressId=1, AccountId=1,  AddressLine1 = "403 Florence Road", AddressLine2 = "Smethwick",  AddressLine3="West Midlands", CityTown = "Birmingham", PostCode = "B12345"},
                new Address() { AddressId=2, AccountId=2,  AddressLine1 = "501 Parkhill Road", AddressLine2 = "Blue Gates",  AddressLine3="West Midlands", CityTown = "Birmingham", PostCode = "B67890"},
            };

            modelBuilder.Entity<Address>().HasData(myAddresses);

            var myCreditCard = new List<CreditCard>() {
                new CreditCard() { CreditCardId=1, AccountId=1, CreditCardName = "Mr A Smith", CreditCardNumber = 123456789, CreditDate =new DateTime(2023,12,01)},
                new CreditCard() { CreditCardId=2, AccountId=2, CreditCardName = "Mr B Jones", CreditCardNumber = 987654321, CreditDate=new DateTime(2023,12,01)}
            };

            modelBuilder.Entity<CreditCard>().HasData(myCreditCard);

            var myBillingAddress = new List<BillingAddress>() {
                new BillingAddress() { BillingAddressId=1, CreditCardId=1,  AddressLine1 = "403 Florence Road", AddressLine2 = "Smethwick",  AddressLine3="West Midlands", CityTown = "Birmingham", PostCode = "B12345"},
                new BillingAddress() { BillingAddressId=2, CreditCardId=2,  AddressLine1 = "501 Parkhill Road", AddressLine2 = "Blue Gates",  AddressLine3="West Midlands", CityTown = "Birmingham", PostCode = "B67890"},
            };

            modelBuilder.Entity<BillingAddress>().HasData(myBillingAddress);

        }
    }
}