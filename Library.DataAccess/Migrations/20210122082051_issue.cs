using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.DataAccess.Migrations
{
    public partial class issue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issue_Admin_AdminUserId",
                table: "Issue");

            migrationBuilder.DropIndex(
                name: "IX_Issue_AdminUserId",
                table: "Issue");

            migrationBuilder.DropColumn(
                name: "AdminUserId",
                table: "Issue");

            migrationBuilder.DropColumn(
                name: "Fine",
                table: "Issue");

            migrationBuilder.CreateIndex(
                name: "IX_Issue_UserId",
                table: "Issue",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Issue_Admin_UserId",
                table: "Issue",
                column: "UserId",
                principalTable: "Admin",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issue_Admin_UserId",
                table: "Issue");

            migrationBuilder.DropIndex(
                name: "IX_Issue_UserId",
                table: "Issue");

            migrationBuilder.AddColumn<int>(
                name: "AdminUserId",
                table: "Issue",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Fine",
                table: "Issue",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Issue_AdminUserId",
                table: "Issue",
                column: "AdminUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Issue_Admin_AdminUserId",
                table: "Issue",
                column: "AdminUserId",
                principalTable: "Admin",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
