using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JuleBeer.Migrations
{
    public partial class stelluMynd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 10,
                column: "ImageUrl",
                value: "https://www.vinbudin.is/Portaldata/1/Resources/vorumyndir/medium/03902_r.jpg");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 10,
                column: "ImageUrl",
                value: "https://www.vinbudin.is/Portaldata/1/Resources/vorumyndir/medium/25300_r.jpg");
        }
    }
}
