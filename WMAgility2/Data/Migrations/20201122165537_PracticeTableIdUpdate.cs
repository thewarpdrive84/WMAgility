using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WMAgility2.Data.Migrations
{
    public partial class PracticeTableIdUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Email",
                columns: table => new
                {
                    From = table.Column<string>(nullable: false),
                    To = table.Column<string>(nullable: false),
                    Subject = table.Column<string>(nullable: true),
                    EventName = table.Column<string>(nullable: true),
                    EventDetails = table.Column<string>(nullable: true),
                    EventLocation = table.Column<string>(nullable: true),
                    EventDate = table.Column<DateTime>(nullable: false),
                    EventTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Practices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    Rounds = table.Column<double>(nullable: false),
                    Score = table.Column<double>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    DogId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Practices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Practices_Dogs_DogId",
                        column: x => x.DogId,
                        principalTable: "Dogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PracticeSkills",
                columns: table => new
                {
                    PracticeId = table.Column<int>(nullable: false),
                    SkillId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PracticeSkills", x => new { x.PracticeId, x.SkillId });
                    table.ForeignKey(
                        name: "FK_PracticeSkills_Practices_PracticeId",
                        column: x => x.PracticeId,
                        principalTable: "Practices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PracticeSkills_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Practices_DogId",
                table: "Practices",
                column: "DogId");

            migrationBuilder.CreateIndex(
                name: "IX_PracticeSkills_SkillId",
                table: "PracticeSkills",
                column: "SkillId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Email");

            migrationBuilder.DropTable(
                name: "PracticeSkills");

            migrationBuilder.DropTable(
                name: "Practices");
        }
    }
}
