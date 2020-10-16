using Microsoft.EntityFrameworkCore.Migrations;

namespace WMAgility2.Data.Migrations
{
    public partial class UpdateUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Competitions_Dog_DogId",
                table: "Competitions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dog",
                table: "Dog");

            migrationBuilder.RenameTable(
                name: "Dog",
                newName: "Dogs");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Dogs",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dogs",
                table: "Dogs",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Dogs_ApplicationUserId",
                table: "Dogs",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Competitions_Dogs_DogId",
                table: "Competitions",
                column: "DogId",
                principalTable: "Dogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Dogs_AspNetUsers_ApplicationUserId",
                table: "Dogs",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Competitions_Dogs_DogId",
                table: "Competitions");

            migrationBuilder.DropForeignKey(
                name: "FK_Dogs_AspNetUsers_ApplicationUserId",
                table: "Dogs");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dogs",
                table: "Dogs");

            migrationBuilder.DropIndex(
                name: "IX_Dogs_ApplicationUserId",
                table: "Dogs");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Dogs");

            migrationBuilder.RenameTable(
                name: "Dogs",
                newName: "Dog");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dog",
                table: "Dog",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Competitions_Dog_DogId",
                table: "Competitions",
                column: "DogId",
                principalTable: "Dog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
