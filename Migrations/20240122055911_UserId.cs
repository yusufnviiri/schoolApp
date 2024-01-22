using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace schoolApp.Migrations
{
    public partial class UserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LibUsers_LibItems_LibItemId",
                table: "LibUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_LibUsers_Students_StudentId",
                table: "LibUsers");

            migrationBuilder.DropIndex(
                name: "IX_LibUsers_LibItemId",
                table: "LibUsers");

            migrationBuilder.DropIndex(
                name: "IX_LibUsers_StudentId",
                table: "LibUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_LibUsers_LibItemId",
                table: "LibUsers",
                column: "LibItemId");

            migrationBuilder.CreateIndex(
                name: "IX_LibUsers_StudentId",
                table: "LibUsers",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_LibUsers_LibItems_LibItemId",
                table: "LibUsers",
                column: "LibItemId",
                principalTable: "LibItems",
                principalColumn: "LibItemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LibUsers_Students_StudentId",
                table: "LibUsers",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
