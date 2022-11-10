using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JuleBeer.Migrations
{
    public partial class skyrjarmur : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Skyrjarmur nr. 99");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Skyrjarmur Skyrjarmur nr. 99");
        }
    }
}
