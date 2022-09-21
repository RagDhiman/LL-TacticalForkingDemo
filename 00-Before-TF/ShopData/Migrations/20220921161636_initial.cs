using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopData.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.AccountId);
                });

            migrationBuilder.CreateTable(
                name: "InventoryCheck",
                columns: table => new
                {
                    InventoryCheckId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StockId = table.Column<int>(type: "int", nullable: false),
                    CheckDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InspectorName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryCheck", x => x.InventoryCheckId);
                });

            migrationBuilder.CreateTable(
                name: "OrderHistory",
                columns: table => new
                {
                    OrderHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderHistory", x => x.OrderHistoryId);
                });

            migrationBuilder.CreateTable(
                name: "Supplier",
                columns: table => new
                {
                    SupplierId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressLine3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CityTown = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelephoneNo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => x.SupplierId);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    AddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressLine3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CityTown = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_Address_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CreditCard",
                columns: table => new
                {
                    CreditCardId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    CreditCardName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreditCardNumber = table.Column<int>(type: "int", nullable: false),
                    CreditDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCard", x => x.CreditCardId);
                    table.ForeignKey(
                        name: "FK_CreditCard_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductCategoryId = table.Column<int>(type: "int", nullable: false),
                    ProductPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Product_Supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Supplier",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BillingAddress",
                columns: table => new
                {
                    BillingAddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreditCardId = table.Column<int>(type: "int", nullable: false),
                    AddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressLine3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CityTown = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillingAddress", x => x.BillingAddressId);
                    table.ForeignKey(
                        name: "FK_BillingAddress_CreditCard_CreditCardId",
                        column: x => x.CreditCardId,
                        principalTable: "CreditCard",
                        principalColumn: "CreditCardId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Order_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Order_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stock",
                columns: table => new
                {
                    StockId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    InventoryCheckId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stock", x => x.StockId);
                    table.ForeignKey(
                        name: "FK_Stock_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Delivery",
                columns: table => new
                {
                    DeliveryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    DeliveryDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressLine3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CityTown = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Delivery", x => x.DeliveryId);
                    table.ForeignKey(
                        name: "FK_Delivery_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "AccountId", "CreatedDate", "EmailAddress", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Smith@tacticalforking.com", "Mr Smith" },
                    { 2, new DateTime(2023, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jones@tacticalforking.com", "Mr Jones" }
                });

            migrationBuilder.InsertData(
                table: "InventoryCheck",
                columns: new[] { "InventoryCheckId", "CheckDateTime", "InspectorName", "StockId" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "B Bob", 1 },
                    { 2, new DateTime(2021, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "J Singh", 2 },
                    { 3, new DateTime(2021, 12, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "R Khan", 3 },
                    { 4, new DateTime(2019, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "E Kumar", 4 },
                    { 5, new DateTime(2018, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "E Jones", 5 },
                    { 6, new DateTime(2017, 9, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "A Albert", 6 },
                    { 7, new DateTime(2019, 10, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "D Montford", 7 },
                    { 8, new DateTime(2018, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "S Watson", 8 }
                });

            migrationBuilder.InsertData(
                table: "OrderHistory",
                columns: new[] { "OrderHistoryId", "OrderDate", "OrderId" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, new DateTime(2020, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 3, new DateTime(2020, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 4, new DateTime(2020, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 5, new DateTime(2020, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 6, new DateTime(2020, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6 },
                    { 7, new DateTime(2020, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7 },
                    { 8, new DateTime(2020, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 8 }
                });

            migrationBuilder.InsertData(
                table: "Supplier",
                columns: new[] { "SupplierId", "AddressLine1", "AddressLine2", "AddressLine3", "CityTown", "PostCode", "SupplierName", "TelephoneNo" },
                values: new object[,]
                {
                    { 1, "304 Coventory Road", "Smethwick", "West Midlands", "Birmingham", "B12345", "Bell", "23423424" },
                    { 2, "504 Wolverhampton Road", "Smethwick", "West Midlands", "Birmingham", "B12345", "Sapple", "23423424" },
                    { 3, "873 Beachway Road", "Smethwick", "West Midlands", "Birmingham", "B12345", "Hamsung", "23423424" },
                    { 4, "490 Lowson Road", "Smethwick", "West Midlands", "Birmingham", "B12345", "Rony", "23423424" },
                    { 5, "222 Rawlings Road", "Blue Gates", "West Midlands", "Birmingham", "B67890", "Vega", "23423424" },
                    { 6, "321 Gillot Road", "Blue Gates", "West Midlands", "Birmingham", "B67890", "Jeto", "23423424" },
                    { 7, "403 Hagley Road", "Blue Gates", "West Midlands", "Birmingham", "B67890", "Ketol", "23423424" },
                    { 8, "302 Broad Street", "Blue Gates", "West Midlands", "Birmingham", "B67890", "Airr", "23423424" }
                });

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "AddressId", "AccountId", "AddressLine1", "AddressLine2", "AddressLine3", "CityTown", "PostCode" },
                values: new object[,]
                {
                    { 1, 1, "403 Florence Road", "Smethwick", "West Midlands", "Birmingham", "B12345" },
                    { 2, 2, "501 Parkhill Road", "Blue Gates", "West Midlands", "Birmingham", "B67890" }
                });

            migrationBuilder.InsertData(
                table: "CreditCard",
                columns: new[] { "CreditCardId", "AccountId", "CreditCardName", "CreditCardNumber", "CreditDate" },
                values: new object[,]
                {
                    { 1, 1, "Mr A Smith", 123456789, new DateTime(2023, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, "Mr B Jones", 987654321, new DateTime(2023, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "CreatedDate", "ProductCategoryId", "ProductName", "ProductPrice", "SupplierId" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, "Phone Case", 12.5m, 1 },
                    { 2, new DateTime(2020, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, "Phone Charger", 22.5m, 1 },
                    { 3, new DateTime(2020, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, "Phone Lead", 2.5m, 1 },
                    { 4, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 13, "Phone Mount", 17.5m, 1 },
                    { 5, new DateTime(2020, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 14, "Phone Car Mount", 20.5m, 2 },
                    { 6, new DateTime(2020, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, "Phone Battery Bank", 30.5m, 2 },
                    { 7, new DateTime(2020, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 16, "Phone Screen Protector", 9.5m, 2 },
                    { 8, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 17, "Phone Wallet", 15.5m, 2 }
                });

            migrationBuilder.InsertData(
                table: "BillingAddress",
                columns: new[] { "BillingAddressId", "AddressLine1", "AddressLine2", "AddressLine3", "CityTown", "CreditCardId", "PostCode" },
                values: new object[,]
                {
                    { 1, "403 Florence Road", "Smethwick", "West Midlands", "Birmingham", 1, "B12345" },
                    { 2, "501 Parkhill Road", "Blue Gates", "West Midlands", "Birmingham", 2, "B67890" }
                });

            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "OrderId", "AccountId", "Price", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 15.5m, 1, 3 },
                    { 2, 1, 10m, 2, 1 },
                    { 3, 1, 10m, 3, 1 },
                    { 4, 1, 10m, 4, 1 },
                    { 5, 2, 15.5m, 5, 3 },
                    { 6, 2, 10m, 6, 1 },
                    { 7, 2, 10m, 7, 1 },
                    { 8, 2, 10m, 8, 1 }
                });

            migrationBuilder.InsertData(
                table: "Stock",
                columns: new[] { "StockId", "InventoryCheckId", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 1, 23423 },
                    { 2, 2, 2, 23423 },
                    { 3, 3, 3, 23423 },
                    { 4, 4, 4, 23423 },
                    { 5, 5, 5, 23423 },
                    { 6, 6, 6, 23423 },
                    { 7, 7, 7, 23423 },
                    { 8, 8, 8, 23423 }
                });

            migrationBuilder.InsertData(
                table: "Delivery",
                columns: new[] { "DeliveryId", "AddressLine1", "AddressLine2", "AddressLine3", "CityTown", "DeliveryDateTime", "OrderId", "PostCode" },
                values: new object[,]
                {
                    { 1, "403 Florence Road", "Smethwick", "West Midlands", "Birmingham", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "B12345" },
                    { 2, "403 Florence Road", "Smethwick", "West Midlands", "Birmingham", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "B12345" },
                    { 3, "403 Florence Road", "Smethwick", "West Midlands", "Birmingham", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "B12345" },
                    { 4, "403 Florence Road", "Smethwick", "West Midlands", "Birmingham", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "B12345" },
                    { 5, "501 Parkhill Road", "Blue Gates", "West Midlands", "Birmingham", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "B67890" },
                    { 6, "501 Parkhill Road", "Blue Gates", "West Midlands", "Birmingham", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "B67890" },
                    { 7, "501 Parkhill Road", "Blue Gates", "West Midlands", "Birmingham", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, "B67890" },
                    { 8, "501 Parkhill Road", "Blue Gates", "West Midlands", "Birmingham", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, "B67890" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_AccountId",
                table: "Address",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BillingAddress_CreditCardId",
                table: "BillingAddress",
                column: "CreditCardId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditCard_AccountId",
                table: "CreditCard",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Delivery_OrderId",
                table: "Delivery",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_AccountId",
                table: "Order",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_ProductId",
                table: "Order",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_SupplierId",
                table: "Product",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_ProductId",
                table: "Stock",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "BillingAddress");

            migrationBuilder.DropTable(
                name: "Delivery");

            migrationBuilder.DropTable(
                name: "InventoryCheck");

            migrationBuilder.DropTable(
                name: "OrderHistory");

            migrationBuilder.DropTable(
                name: "Stock");

            migrationBuilder.DropTable(
                name: "CreditCard");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Supplier");
        }
    }
}
