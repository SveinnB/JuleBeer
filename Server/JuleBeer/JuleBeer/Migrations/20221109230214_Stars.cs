using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JuleBeer.Migrations
{
    public partial class Stars : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Starts",
                table: "BeerReviews",
                newName: "Stars");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Stars",
                table: "BeerReviews",
                newName: "Starts");
        }
    }
}
