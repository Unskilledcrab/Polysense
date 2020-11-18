using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PS.Web.API.Migrations
{
    public partial class addingManyMoreModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Politician_Congress_CongressId",
                table: "Politician");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Congress");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Congress");

            migrationBuilder.RenameColumn(
                name: "CongressId",
                table: "Politician",
                newName: "SenateId");

            migrationBuilder.RenameIndex(
                name: "IX_Politician_CongressId",
                table: "Politician",
                newName: "IX_Politician_SenateId");

            migrationBuilder.RenameColumn(
                name: "NumberOfPeople",
                table: "Congress",
                newName: "CurrentCongress");

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDateTime",
                table: "Politician",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CongressionalCommitteId",
                table: "Politician",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrentOffice",
                table: "Politician",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Politician",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HouseOfRepresentativesId",
                table: "Politician",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Politician",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HouseId",
                table: "Congress",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SenateId",
                table: "Congress",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CongressionalCommitteId",
                table: "Bill",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HouseOfRepresentativesId",
                table: "Bill",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SenateId",
                table: "Bill",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BillStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransitionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    BillId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillStatus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillStatus_Bill_BillId",
                        column: x => x.BillId,
                        principalTable: "Bill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CongressionalCommitte",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChairId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CongressionalCommitte", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CongressionalCommitte_Politician_ChairId",
                        column: x => x.ChairId,
                        principalTable: "Politician",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HouseOfRepresentatives",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpeakerOfTheHouseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseOfRepresentatives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HouseOfRepresentatives_Politician_SpeakerOfTheHouseId",
                        column: x => x.SpeakerOfTheHouseId,
                        principalTable: "Politician",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Senate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PresidentOfTheSenateId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Senate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Senate_Politician_PresidentOfTheSenateId",
                        column: x => x.PresidentOfTheSenateId,
                        principalTable: "Politician",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SupremeCourt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChiefJusticeId = table.Column<int>(type: "int", nullable: true),
                    TransitionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransitionType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupremeCourt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Judge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AppointedById = table.Column<int>(type: "int", nullable: true),
                    SupremeCourtId = table.Column<int>(type: "int", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Judge", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Judge_Politician_AppointedById",
                        column: x => x.AppointedById,
                        principalTable: "Politician",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Judge_SupremeCourt_SupremeCourtId",
                        column: x => x.SupremeCourtId,
                        principalTable: "SupremeCourt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Politician_CongressionalCommitteId",
                table: "Politician",
                column: "CongressionalCommitteId");

            migrationBuilder.CreateIndex(
                name: "IX_Politician_HouseOfRepresentativesId",
                table: "Politician",
                column: "HouseOfRepresentativesId");

            migrationBuilder.CreateIndex(
                name: "IX_Congress_HouseId",
                table: "Congress",
                column: "HouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Congress_SenateId",
                table: "Congress",
                column: "SenateId");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_CongressionalCommitteId",
                table: "Bill",
                column: "CongressionalCommitteId");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_HouseOfRepresentativesId",
                table: "Bill",
                column: "HouseOfRepresentativesId");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_SenateId",
                table: "Bill",
                column: "SenateId");

            migrationBuilder.CreateIndex(
                name: "IX_BillStatus_BillId",
                table: "BillStatus",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_CongressionalCommitte_ChairId",
                table: "CongressionalCommitte",
                column: "ChairId");

            migrationBuilder.CreateIndex(
                name: "IX_HouseOfRepresentatives_SpeakerOfTheHouseId",
                table: "HouseOfRepresentatives",
                column: "SpeakerOfTheHouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Judge_AppointedById",
                table: "Judge",
                column: "AppointedById");

            migrationBuilder.CreateIndex(
                name: "IX_Judge_SupremeCourtId",
                table: "Judge",
                column: "SupremeCourtId");

            migrationBuilder.CreateIndex(
                name: "IX_Senate_PresidentOfTheSenateId",
                table: "Senate",
                column: "PresidentOfTheSenateId");

            migrationBuilder.CreateIndex(
                name: "IX_SupremeCourt_ChiefJusticeId",
                table: "SupremeCourt",
                column: "ChiefJusticeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bill_CongressionalCommitte_CongressionalCommitteId",
                table: "Bill",
                column: "CongressionalCommitteId",
                principalTable: "CongressionalCommitte",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bill_HouseOfRepresentatives_HouseOfRepresentativesId",
                table: "Bill",
                column: "HouseOfRepresentativesId",
                principalTable: "HouseOfRepresentatives",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bill_Senate_SenateId",
                table: "Bill",
                column: "SenateId",
                principalTable: "Senate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Congress_HouseOfRepresentatives_HouseId",
                table: "Congress",
                column: "HouseId",
                principalTable: "HouseOfRepresentatives",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Congress_Senate_SenateId",
                table: "Congress",
                column: "SenateId",
                principalTable: "Senate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Politician_CongressionalCommitte_CongressionalCommitteId",
                table: "Politician",
                column: "CongressionalCommitteId",
                principalTable: "CongressionalCommitte",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Politician_HouseOfRepresentatives_HouseOfRepresentativesId",
                table: "Politician",
                column: "HouseOfRepresentativesId",
                principalTable: "HouseOfRepresentatives",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Politician_Senate_SenateId",
                table: "Politician",
                column: "SenateId",
                principalTable: "Senate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SupremeCourt_Judge_ChiefJusticeId",
                table: "SupremeCourt",
                column: "ChiefJusticeId",
                principalTable: "Judge",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bill_CongressionalCommitte_CongressionalCommitteId",
                table: "Bill");

            migrationBuilder.DropForeignKey(
                name: "FK_Bill_HouseOfRepresentatives_HouseOfRepresentativesId",
                table: "Bill");

            migrationBuilder.DropForeignKey(
                name: "FK_Bill_Senate_SenateId",
                table: "Bill");

            migrationBuilder.DropForeignKey(
                name: "FK_Congress_HouseOfRepresentatives_HouseId",
                table: "Congress");

            migrationBuilder.DropForeignKey(
                name: "FK_Congress_Senate_SenateId",
                table: "Congress");

            migrationBuilder.DropForeignKey(
                name: "FK_Politician_CongressionalCommitte_CongressionalCommitteId",
                table: "Politician");

            migrationBuilder.DropForeignKey(
                name: "FK_Politician_HouseOfRepresentatives_HouseOfRepresentativesId",
                table: "Politician");

            migrationBuilder.DropForeignKey(
                name: "FK_Politician_Senate_SenateId",
                table: "Politician");

            migrationBuilder.DropForeignKey(
                name: "FK_Judge_SupremeCourt_SupremeCourtId",
                table: "Judge");

            migrationBuilder.DropTable(
                name: "BillStatus");

            migrationBuilder.DropTable(
                name: "CongressionalCommitte");

            migrationBuilder.DropTable(
                name: "HouseOfRepresentatives");

            migrationBuilder.DropTable(
                name: "Senate");

            migrationBuilder.DropTable(
                name: "SupremeCourt");

            migrationBuilder.DropTable(
                name: "Judge");

            migrationBuilder.DropIndex(
                name: "IX_Politician_CongressionalCommitteId",
                table: "Politician");

            migrationBuilder.DropIndex(
                name: "IX_Politician_HouseOfRepresentativesId",
                table: "Politician");

            migrationBuilder.DropIndex(
                name: "IX_Congress_HouseId",
                table: "Congress");

            migrationBuilder.DropIndex(
                name: "IX_Congress_SenateId",
                table: "Congress");

            migrationBuilder.DropIndex(
                name: "IX_Bill_CongressionalCommitteId",
                table: "Bill");

            migrationBuilder.DropIndex(
                name: "IX_Bill_HouseOfRepresentativesId",
                table: "Bill");

            migrationBuilder.DropIndex(
                name: "IX_Bill_SenateId",
                table: "Bill");

            migrationBuilder.DropColumn(
                name: "BirthDateTime",
                table: "Politician");

            migrationBuilder.DropColumn(
                name: "CongressionalCommitteId",
                table: "Politician");

            migrationBuilder.DropColumn(
                name: "CurrentOffice",
                table: "Politician");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Politician");

            migrationBuilder.DropColumn(
                name: "HouseOfRepresentativesId",
                table: "Politician");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Politician");

            migrationBuilder.DropColumn(
                name: "HouseId",
                table: "Congress");

            migrationBuilder.DropColumn(
                name: "SenateId",
                table: "Congress");

            migrationBuilder.DropColumn(
                name: "CongressionalCommitteId",
                table: "Bill");

            migrationBuilder.DropColumn(
                name: "HouseOfRepresentativesId",
                table: "Bill");

            migrationBuilder.DropColumn(
                name: "SenateId",
                table: "Bill");

            migrationBuilder.RenameColumn(
                name: "SenateId",
                table: "Politician",
                newName: "CongressId");

            migrationBuilder.RenameIndex(
                name: "IX_Politician_SenateId",
                table: "Politician",
                newName: "IX_Politician_CongressId");

            migrationBuilder.RenameColumn(
                name: "CurrentCongress",
                table: "Congress",
                newName: "NumberOfPeople");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Congress",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Congress",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Politician_Congress_CongressId",
                table: "Politician",
                column: "CongressId",
                principalTable: "Congress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
