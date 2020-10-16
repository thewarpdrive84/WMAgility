using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WMAgility2.Data.Migrations
{
    public partial class AddTablesToDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dog",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DogName = table.Column<string>(nullable: false),
                    Breed = table.Column<string>(nullable: true),
                    DOB = table.Column<DateTime>(nullable: false),
                    Image = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Faults",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Competitions",
                columns: table => new
                {
                    CompId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompName = table.Column<string>(nullable: false),
                    Location = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Length = table.Column<string>(nullable: true),
                    Surface = table.Column<int>(nullable: false),
                    Placement = table.Column<int>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    DogId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competitions", x => x.CompId);
                    table.ForeignKey(
                        name: "FK_Competitions_Dog_DogId",
                        column: x => x.DogId,
                        principalTable: "Dog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompFaults",
                columns: table => new
                {
                    CompId = table.Column<int>(nullable: false),
                    FaultId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompFaults", x => new { x.CompId, x.FaultId });
                    table.ForeignKey(
                        name: "FK_CompFaults_Competitions_CompId",
                        column: x => x.CompId,
                        principalTable: "Competitions",
                        principalColumn: "CompId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompFaults_Faults_FaultId",
                        column: x => x.FaultId,
                        principalTable: "Faults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Competitions_DogId",
                table: "Competitions",
                column: "DogId");

            migrationBuilder.CreateIndex(
                name: "IX_CompFaults_FaultId",
                table: "CompFaults",
                column: "FaultId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompFaults");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Competitions");

            migrationBuilder.DropTable(
                name: "Faults");

            migrationBuilder.DropTable(
                name: "Dog");
        }
    }
}
