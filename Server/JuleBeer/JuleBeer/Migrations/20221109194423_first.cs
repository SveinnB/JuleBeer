using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JuleBeer.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Beers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ABV = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BeerReviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BeerId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Starts = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeerReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BeerReviews_Beers_BeerId",
                        column: x => x.BeerId,
                        principalTable: "Beers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BeerReviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Beers",
                columns: new[] { "Id", "ABV", "Description", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { 1, "4,2", "Ljósgullinn, skýjaður. Ósætur, léttur, ferskur, meðalbeiskja. Mangó, grösugur, sítrusbörkur", "https://www.vinbudin.is/Portaldata/1/Resources/vorumyndir/medium/28823_r.jpg", "Jólasopi Session IPA" },
                    { 2, "8,5", "Rafbrúnn, óskír. Smásætur, þéttur, hverfandi beiskja. Ristað malt, kandís, þurrkaðar apríkósur, karamella", "https://www.vinbudin.is/Portaldata/1/Resources/vorumyndir/medium/26336_r.jpg", "Þriðji í jólum belgian tripel" },
                    { 3, "5,8", "Rafbrúnn. Sætuvottur, meðalfylltur, miðlungsbeiskja. Þurrkaðir ávextir, kandís, kóla", "https://www.vinbudin.is/Portaldata/1/Resources/vorumyndir/medium/27673_r.jpg", "Okkara Jól" },
                    { 4, "8", "Rauður, skýjaður. Hálfsætur, þéttur, meðalbeiskja, sýruríkur. Bláber, jarðarber, skyr", "https://www.vinbudin.is/Portaldata/1/Resources/vorumyndir/medium/28778_r.jpg", "Skyrjarmur Skyrjarmur nr. 99" },
                    { 5, "4,5", "Ljósgullinn, skýjaður. Ósætur, léttur, meðalbeiskja. Appelsínubörkur, kóríander, blómlegur", "https://www.vinbudin.is/Portaldata/1/Resources/vorumyndir/medium/26170_r.jpg", "Frostrósir White Ale" },
                    { 6, "6,5", "Svarbrúnn. Sætuvottur, þéttur, lítil beiskja. Ristað malt, kakó, kaffi, súkkulaði", "https://www.vinbudin.is/Portaldata/1/Resources/vorumyndir/medium/28823_r.jpg", "Jóla Kaldi Súkkulaði Porter" },
                    { 7, "5,2", "Ljósgullinn, skýjaður. Ósætur, meðalfylltur, beiskur. Sítrus, mangó, barr, grösugir humlar", "https://www.vinbudin.is/Portaldata/1/Resources/vorumyndir/medium/25300_r.jpg", "Bjúgnakrækir nr. 71" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BeerReviews_BeerId",
                table: "BeerReviews",
                column: "BeerId");

            migrationBuilder.CreateIndex(
                name: "IX_BeerReviews_UserId",
                table: "BeerReviews",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BeerReviews");

            migrationBuilder.DropTable(
                name: "Beers");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
