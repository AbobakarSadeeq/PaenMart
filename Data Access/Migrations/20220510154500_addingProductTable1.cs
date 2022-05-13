using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access.Migrations
{
    public partial class addingProductTable1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_NestSubCategories_NestSubCategoryId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductBrands_ProductBrandId",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "Products");

            migrationBuilder.RenameIndex(
                name: "IX_Product_ProductBrandId",
                table: "Products",
                newName: "IX_Products_ProductBrandId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_NestSubCategoryId",
                table: "Products",
                newName: "IX_Products_NestSubCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_NestSubCategories_NestSubCategoryId",
                table: "Products",
                column: "NestSubCategoryId",
                principalTable: "NestSubCategories",
                principalColumn: "NestSubCategoryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductBrands_ProductBrandId",
                table: "Products",
                column: "ProductBrandId",
                principalTable: "ProductBrands",
                principalColumn: "ProductBrandID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_NestSubCategories_NestSubCategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductBrands_ProductBrandId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Product");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ProductBrandId",
                table: "Product",
                newName: "IX_Product_ProductBrandId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_NestSubCategoryId",
                table: "Product",
                newName: "IX_Product_NestSubCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_NestSubCategories_NestSubCategoryId",
                table: "Product",
                column: "NestSubCategoryId",
                principalTable: "NestSubCategories",
                principalColumn: "NestSubCategoryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductBrands_ProductBrandId",
                table: "Product",
                column: "ProductBrandId",
                principalTable: "ProductBrands",
                principalColumn: "ProductBrandID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
