using Microsoft.EntityFrameworkCore.Migrations;

namespace PS.Web.API.Migrations
{
    public partial class AddingTextCategoryItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TextCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextCategory", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TextCategoryFinalized");

            migrationBuilder.DropTable(
                name: "TextCategory");
        }
    }
}
