using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access.Migrations
{
    public partial class addingProductsDiscountDealTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DiscountDeals",
                columns: table => new
                {
                    DiscountDealID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DealName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DealStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DealExpireAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DealCreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountDeals", x => x.DiscountDealID);
                });

            migrationBuilder.CreateTable(
                name: "ProductDiscountDeals",
                columns: table => new
                {
                    ProductDiscountDealID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductBeforePrice = table.Column<int>(type: "int", nullable: false),
                    ProductAfterDiscountPrice = table.Column<int>(type: "int", nullable: false),
                    ProductPercentage = table.Column<int>(type: "int", nullable: false),
                    DiscountDealId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDiscountDeals", x => x.ProductDiscountDealID);
                    table.ForeignKey(
                        name: "FK_ProductDiscountDeals_DiscountDeals_DiscountDealId",
                        column: x => x.DiscountDealId,
                        principalTable: "DiscountDeals",
                        principalColumn: "DiscountDealID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductDiscountDeals_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductDiscountDeals_DiscountDealId",
                table: "ProductDiscountDeals",
                column: "DiscountDealId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDiscountDeals_ProductId",
                table: "ProductDiscountDeals",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductDiscountDeals");

            migrationBuilder.DropTable(
                name: "DiscountDeals");
        }
    }
}
