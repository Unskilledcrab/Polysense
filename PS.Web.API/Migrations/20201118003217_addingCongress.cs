using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PS.Web.API.Migrations
{
    public partial class addingCongress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CongressId",
                table: "Politician",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Congress",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Congress", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Politician_CongressId",
                table: "Politician",
                column: "CongressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Politician_Congress_CongressId",
                table: "Politician",
                column: "CongressId",
                principalTable: "Congress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Politician_Congress_CongressId",
                table: "Politician");

            migrationBuilder.DropTable(
                name: "Congress");

            migrationBuilder.DropIndex(
                name: "IX_Politician_CongressId",
                table: "Politician");

            migrationBuilder.DropColumn(
                name: "CongressId",
                table: "Politician");
        }
    }
}
