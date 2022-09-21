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

            var myProducts = new List<Product>() {
                new Product() { ProductId=1, ProductCategoryId = 10, ProductName = "Phone Case",   ProductPrice= (decimal)12.5,  SupplierId = 1,  CreatedDate = new DateTime(2020,12,01)},
                new Product() { ProductId=2, ProductCategoryId = 11, ProductName = "Phone Charger",   ProductPrice= (decimal)22.5,  SupplierId = 1,  CreatedDate = new DateTime(2020,11,01)},
                new Product() { ProductId=3, ProductCategoryId = 12, ProductName = "Phone Lead",   ProductPrice= (decimal)2.5,  SupplierId = 1,  CreatedDate = new DateTime(2020,10,01)},
                new Product() { ProductId=4, ProductCategoryId = 13, ProductName = "Phone Mount",   ProductPrice= (decimal)17.5,  SupplierId = 1,  CreatedDate = new DateTime(2020,09,01)},
                new Product() { ProductId=5, ProductCategoryId = 14, ProductName = "Phone Car Mount",   ProductPrice= (decimal)20.5,  SupplierId = 2,  CreatedDate = new DateTime(2020,08,01)},
                new Product() { ProductId=6, ProductCategoryId = 15, ProductName = "Phone Battery Bank",   ProductPrice= (decimal)30.5,  SupplierId = 2,  CreatedDate = new DateTime(2020,07,01)},
                new Product() { ProductId=7, ProductCategoryId = 16, ProductName = "Phone Screen Protector",   ProductPrice= (decimal)9.5,  SupplierId = 2,  CreatedDate = new DateTime(2020,06,01)},
                new Product() { ProductId=8, ProductCategoryId = 17, ProductName = "Phone Wallet",   ProductPrice= (decimal)15.5,  SupplierId = 2,  CreatedDate = new DateTime(2020,05,01)},
            };

            modelBuilder.Entity<Product>().HasData(myProducts);

            var myOrders = new List<Order>() {
                new Order() { OrderId=1, AccountId=1,  ProductId = 1,  Quantity = 3,  Price  = (decimal)15.5},
                new Order() { OrderId=2, AccountId=1,  ProductId = 2,  Quantity = 1,  Price  = (decimal)10},
                new Order() { OrderId=3, AccountId=1,  ProductId = 3,  Quantity = 1,  Price  = (decimal)10},
                new Order() { OrderId=4, AccountId=1,  ProductId = 4,  Quantity = 1,  Price  = (decimal)10},
                new Order() { OrderId=5, AccountId=2,  ProductId = 5,  Quantity = 3,  Price  = (decimal)15.5},
                new Order() { OrderId=6, AccountId=2,  ProductId = 6,  Quantity = 1,  Price  = (decimal)10},
                new Order() { OrderId=7, AccountId=2,  ProductId = 7,  Quantity = 1,  Price  = (decimal)10},
                new Order() { OrderId=8, AccountId=2,  ProductId = 8,  Quantity = 1,  Price  = (decimal)10}
            };

            modelBuilder.Entity<Order>().HasData(myOrders);

            var myDelivery = new List<Delivery>() {
                new Delivery() { OrderId=1, DeliveryId=1,  AddressLine1 = "403 Florence Road", AddressLine2 = "Smethwick",  AddressLine3="West Midlands", CityTown = "Birmingham", PostCode = "B12345"},
                new Delivery() { OrderId=2, DeliveryId=2,  AddressLine1 = "403 Florence Road", AddressLine2 = "Smethwick",  AddressLine3="West Midlands", CityTown = "Birmingham", PostCode = "B12345"},
                new Delivery() { OrderId=3, DeliveryId=3,  AddressLine1 = "403 Florence Road", AddressLine2 = "Smethwick",  AddressLine3="West Midlands", CityTown = "Birmingham", PostCode = "B12345"},
                new Delivery() { OrderId=4, DeliveryId=4,  AddressLine1 = "403 Florence Road", AddressLine2 = "Smethwick",  AddressLine3="West Midlands", CityTown = "Birmingham", PostCode = "B12345"},
                new Delivery() { OrderId=5, DeliveryId=5,  AddressLine1 = "501 Parkhill Road", AddressLine2 = "Blue Gates",  AddressLine3="West Midlands", CityTown = "Birmingham", PostCode = "B67890"},
                new Delivery() { OrderId=6, DeliveryId=6,  AddressLine1 = "501 Parkhill Road", AddressLine2 = "Blue Gates",  AddressLine3="West Midlands", CityTown = "Birmingham", PostCode = "B67890"},
                new Delivery() { OrderId=7, DeliveryId=7,  AddressLine1 = "501 Parkhill Road", AddressLine2 = "Blue Gates",  AddressLine3="West Midlands", CityTown = "Birmingham", PostCode = "B67890"},
                new Delivery() { OrderId=8, DeliveryId=8,  AddressLine1 = "501 Parkhill Road", AddressLine2 = "Blue Gates",  AddressLine3="West Midlands", CityTown = "Birmingham", PostCode = "B67890"}
            };

            modelBuilder.Entity<Delivery>().HasData(myDelivery);

            var myOrderHistories = new List<OrderHistory>() {
                new OrderHistory() { OrderId=1, OrderHistoryId=1, OrderDate = new DateTime(2020,12,01)},
                new OrderHistory() { OrderId=2, OrderHistoryId=2, OrderDate = new DateTime(2020,12,01)},
                new OrderHistory() { OrderId=3, OrderHistoryId=3, OrderDate = new DateTime(2020,12,01)},
                new OrderHistory() { OrderId=4, OrderHistoryId=4, OrderDate = new DateTime(2020,12,01)},
                new OrderHistory() { OrderId=5, OrderHistoryId=5, OrderDate = new DateTime(2020,12,01)},
                new OrderHistory() { OrderId=6, OrderHistoryId=6, OrderDate = new DateTime(2020,12,01)},
                new OrderHistory() { OrderId=7, OrderHistoryId=7, OrderDate = new DateTime(2020,12,01)},
                new OrderHistory() { OrderId=8, OrderHistoryId=8, OrderDate = new DateTime(2020,12,01)}
            };

            modelBuilder.Entity<OrderHistory>().HasData(myOrderHistories);

            var mySuppliers = new List<Supplier>() {
                new Supplier() { SupplierId=1,  SupplierName="Bell",  AddressLine1 = "304 Coventory Road", AddressLine2 = "Smethwick",  AddressLine3="West Midlands", CityTown = "Birmingham", PostCode = "B12345"},
                new Supplier() { SupplierId=2,  SupplierName="Sapple",  AddressLine1 = "504 Wolverhampton Road", AddressLine2 = "Smethwick",  AddressLine3="West Midlands", CityTown = "Birmingham", PostCode = "B12345"},
                new Supplier() { SupplierId=3,  SupplierName="Hamsung",  AddressLine1 = "873 Beachway Road", AddressLine2 = "Smethwick",  AddressLine3="West Midlands", CityTown = "Birmingham", PostCode = "B12345"},
                new Supplier() { SupplierId=4,  SupplierName="Rony",  AddressLine1 = "490 Lowson Road", AddressLine2 = "Smethwick",  AddressLine3="West Midlands", CityTown = "Birmingham", PostCode = "B12345"},
                new Supplier() { SupplierId=5,  SupplierName="Vega",  AddressLine1 = "222 Rawlings Road", AddressLine2 = "Blue Gates",  AddressLine3="West Midlands", CityTown = "Birmingham", PostCode = "B67890"},
                new Supplier() { SupplierId=6,  SupplierName="Jeto",  AddressLine1 = "321 Gillot Road", AddressLine2 = "Blue Gates",  AddressLine3="West Midlands", CityTown = "Birmingham", PostCode = "B67890"},
                new Supplier() { SupplierId=7,  SupplierName="Ketol",  AddressLine1 = "403 Hagley Road", AddressLine2 = "Blue Gates",  AddressLine3="West Midlands", CityTown = "Birmingham", PostCode = "B67890"},
                new Supplier() { SupplierId=8,  SupplierName="Airr",  AddressLine1 = "302 Broad Street", AddressLine2 = "Blue Gates",  AddressLine3="West Midlands", CityTown = "Birmingham", PostCode = "B67890"}
            };

            modelBuilder.Entity<Supplier>().HasData(mySuppliers);

            var myInventoryChecks = new List<InventoryCheck>() {
                new InventoryCheck() { InventoryCheckId=1, StockId=1,  CheckDateTime = new DateTime(2020,12,01), InspectorName = "B Bob"},
                new InventoryCheck() { InventoryCheckId=2, StockId=2,  CheckDateTime = new DateTime(2021,01,02), InspectorName = "J Singh"},
                new InventoryCheck() { InventoryCheckId=3, StockId=3,  CheckDateTime = new DateTime(2021,12,04), InspectorName = "R Khan"},
                new InventoryCheck() { InventoryCheckId=4, StockId=4,  CheckDateTime = new DateTime(2019,03,06), InspectorName = "E Kumar"},
                new InventoryCheck() { InventoryCheckId=5, StockId=5,  CheckDateTime = new DateTime(2018,04,01), InspectorName = "E Jones"},
                new InventoryCheck() { InventoryCheckId=6, StockId=6,  CheckDateTime = new DateTime(2017,09,23), InspectorName = "A Albert"},
                new InventoryCheck() { InventoryCheckId=7, StockId=7,  CheckDateTime = new DateTime(2019,10,13), InspectorName = "D Montford"},
                new InventoryCheck() { InventoryCheckId=8, StockId=8,  CheckDateTime = new DateTime(2018,11,05), InspectorName = "S Watson"}
            };

            modelBuilder.Entity<InventoryCheck>().HasData(myInventoryChecks);

            var myStocks = new List<Stock>() {
                new Stock() { InventoryCheckId=1, StockId=1,  ProductId = 1, Quantity = 23423},
                new Stock() { InventoryCheckId=2, StockId=2,  ProductId = 2, Quantity = 23423},
                new Stock() { InventoryCheckId=3, StockId=3,  ProductId = 3, Quantity = 23423},
                new Stock() { InventoryCheckId=4, StockId=4,  ProductId = 4, Quantity = 23423},
                new Stock() { InventoryCheckId=5, StockId=5,  ProductId = 5, Quantity = 23423},
                new Stock() { InventoryCheckId=6, StockId=6,  ProductId = 6, Quantity = 23423},
                new Stock() { InventoryCheckId=7, StockId=7,  ProductId = 7, Quantity = 23423},
                new Stock() { InventoryCheckId=8, StockId=8,  ProductId = 8, Quantity = 23423}
            };

            modelBuilder.Entity<Stock>().HasData(myStocks);
        }
    }
}