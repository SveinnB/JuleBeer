using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JuleBeer.Migrations
{
    public partial class data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 4,
                column: "ABV",
                value: "8,0");

            migrationBuilder.UpdateData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImageUrl",
                value: "https://www.vinbudin.is/Portaldata/1/Resources/vorumyndir/medium/22261_r.jpg");

            migrationBuilder.InsertData(
                table: "Beers",
                columns: new[] { "Id", "ABV", "Description", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { 8, "5,6", "Rafbrúnn. Sætuvottur, meðalfylling, lítil beiskja. Ristað malt, þurrkaðir ávextir, kandís", "https://www.vinbudin.is/Portaldata/1/Resources/vorumyndir/medium/09452_r.jpg", "Tuborg Julebryg" },
                    { 9, "5,4", "Rafrauður. Ósætur, meðalfylltur, ferskur, miðlungsbeiskja. Karamella, þurrkaðir ávextir, barr, jörð", "https://www.vinbudin.is/Portaldata/1/Resources/vorumyndir/medium/13791_r.jpg", "Jóla Kaldi" },
                    { 10, "5,0", "Ljósgullinn. Ósætur, létt meðalfylling, lítil beiskja. Korn, blómlegir humlar", "https://www.vinbudin.is/Portaldata/1/Resources/vorumyndir/medium/25300_r.jpg", "Stella Artois" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.UpdateData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 4,
                column: "ABV",
                value: "8");

            migrationBuilder.UpdateData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImageUrl",
                value: "https://www.vinbudin.is/Portaldata/1/Resources/vorumyndir/medium/28823_r.jpg");
        }
    }
}
