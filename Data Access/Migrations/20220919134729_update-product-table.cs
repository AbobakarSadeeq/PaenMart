using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access.Migrations
{
    public partial class updateproducttable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "OnDiscount",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OnDiscount",
                table: "Products");
        }
    }
}
