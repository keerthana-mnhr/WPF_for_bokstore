using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WPF_for_bokstore.Migrations
{
    /// <inheritdoc />
    public partial class BookStoreCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Förnamn = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Efternamn = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Födelsedatum = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookFormat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    FormatName = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BookForm__3214EC07141D5944", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "varchar(320)", unicode: false, maxLength: 320, nullable: true),
                    Phone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    ShippingCity = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ShippingCountry = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Zipcode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Customer__3214EC07F38A3B51", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Genre__3214EC076A7DD8DD", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Publisher",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublisherName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Website = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Publishe__3214EC07AF2CCC02", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Store",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    City = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Store__3214EC07C9A4D427", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    DateOfPurchase = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    ShippedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ShipVia = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Order__3214EC0714614790", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Customer",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    ISBN13 = table.Column<string>(type: "varchar(13)", unicode: false, maxLength: 13, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Language = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Release_Date = table.Column<DateOnly>(type: "date", nullable: true),
                    PublisherId = table.Column<int>(type: "int", nullable: true),
                    GenreId = table.Column<int>(type: "int", nullable: true),
                    BookFormatId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Books__3BF79E031FF9534D", x => x.ISBN13);
                    table.ForeignKey(
                        name: "FK__Books__GenreId__48CFD27E",
                        column: x => x.GenreId,
                        principalTable: "Genre",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Books__Publisher__628FA481",
                        column: x => x.PublisherId,
                        principalTable: "Publisher",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "Fk_BookFormatId",
                        column: x => x.BookFormatId,
                        principalTable: "BookFormat",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BooksAuthor",
                columns: table => new
                {
                    BooksISBN13 = table.Column<string>(type: "varchar(13)", unicode: false, maxLength: 13, nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BooksAut__B3586DCC5303EBB0", x => new { x.BooksISBN13, x.AuthorId });
                    table.ForeignKey(
                        name: "FK__BooksAuth__Autho__5535A963",
                        column: x => x.AuthorId,
                        principalTable: "Author",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__BooksAuth__Books__6477ECF3",
                        column: x => x.BooksISBN13,
                        principalTable: "Books",
                        principalColumn: "ISBN13");
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: true),
                    BookISBN13 = table.Column<string>(type: "varchar(13)", unicode: false, maxLength: 13, nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OrderDet__3214EC0747B0B598", x => x.Id);
                    table.ForeignKey(
                        name: "FK__OrderDeta__BookI__03F0984C",
                        column: x => x.BookISBN13,
                        principalTable: "Books",
                        principalColumn: "ISBN13");
                    table.ForeignKey(
                        name: "FK__OrderDeta__Order__02FC7413",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StockBalance",
                columns: table => new
                {
                    StoreID = table.Column<int>(type: "int", nullable: false),
                    BookISBN13 = table.Column<string>(type: "varchar(13)", unicode: false, maxLength: 13, nullable: false),
                    Count = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Stock", x => new { x.StoreID, x.BookISBN13 });
                    table.ForeignKey(
                        name: "FK_ISBN_Books",
                        column: x => x.BookISBN13,
                        principalTable: "Books",
                        principalColumn: "ISBN13");
                    table.ForeignKey(
                        name: "FK__StockBala__Store__5629CD9C",
                        column: x => x.StoreID,
                        principalTable: "Store",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_BookFormatId",
                table: "Books",
                column: "BookFormatId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_GenreId",
                table: "Books",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_PublisherId",
                table: "Books",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_BooksAuthor_AuthorId",
                table: "BooksAuthor",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerId",
                table: "Order",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_BookISBN13",
                table: "OrderDetails",
                column: "BookISBN13");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_StockBalance_BookISBN13",
                table: "StockBalance",
                column: "BookISBN13");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BooksAuthor");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "StockBalance");

            migrationBuilder.DropTable(
                name: "Author");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Store");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropTable(
                name: "Publisher");

            migrationBuilder.DropTable(
                name: "BookFormat");
        }
    }
}
