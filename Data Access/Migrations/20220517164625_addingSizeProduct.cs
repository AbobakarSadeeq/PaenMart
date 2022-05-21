using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access.Migrations
{
    public partial class addingSizeProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductSize",
                table: "Products");

            migrationBuilder.AddColumn<bool>(
                name: "ProductSize",
                table: "DynamicFormStructures",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductSize",
                table: "DynamicFormStructures");

            migrationBuilder.AddColumn<bool>(
                name: "ProductSize",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
