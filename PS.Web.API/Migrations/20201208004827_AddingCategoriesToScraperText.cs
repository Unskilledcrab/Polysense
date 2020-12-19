using Microsoft.EntityFrameworkCore.Migrations;

namespace PS.Web.API.Migrations
{
    public partial class AddingCategoriesToScraperText : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TextCategoryFinalized");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "ScraperText",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BillVotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CongressId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillVotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillVotes_Congress_CongressId",
                        column: x => x.CongressId,
                        principalTable: "Congress",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScraperText_CategoryId",
                table: "ScraperText",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BillVotes_CongressId",
                table: "BillVotes",
                column: "CongressId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScraperText_TextCategory_CategoryId",
                table: "ScraperText",
                column: "CategoryId",
                principalTable: "TextCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScraperText_TextCategory_CategoryId",
                table: "ScraperText");

            migrationBuilder.DropTable(
                name: "BillVotes");

            migrationBuilder.DropIndex(
                name: "IX_ScraperText_CategoryId",
                table: "ScraperText");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "ScraperText");

            migrationBuilder.CreateTable(
                name: "TextCategoryFinalized",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextCategoryFinalized", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TextCategoryFinalized_TextCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "TextCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TextCategoryFinalized_CategoryId",
                table: "TextCategoryFinalized",
                column: "CategoryId");
        }
    }
}
