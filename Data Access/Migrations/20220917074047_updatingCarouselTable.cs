using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access.Migrations
{
    public partial class updatingCarouselTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NavigationUrl",
                table: "Carousels",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NavigationUrl",
                table: "Carousels");
        }
    }
}
