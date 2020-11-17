using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PS.Web.API.Migrations
{
    public partial class addingBillModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BillId",
                table: "Politician",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Bill",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IntroductionDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SponsorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bill_Politician_SponsorId",
                        column: x => x.SponsorId,
                        principalTable: "Politician",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Politician_BillId",
                table: "Politician",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_SponsorId",
                table: "Bill",
                column: "SponsorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Politician_Bill_BillId",
                table: "Politician",
                column: "BillId",
                principalTable: "Bill",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Politician_Bill_BillId",
                table: "Politician");

            migrationBuilder.DropTable(
                name: "Bill");

            migrationBuilder.DropIndex(
                name: "IX_Politician_BillId",
                table: "Politician");

            migrationBuilder.DropColumn(
                name: "BillId",
                table: "Politician");
        }
    }
}
