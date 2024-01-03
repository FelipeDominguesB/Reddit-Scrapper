using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RedditScrapper.Context.Migrations
{
    public partial class usertoroutine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Routines",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Routines_UserId",
                table: "Routines",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Routines_Users_UserId",
                table: "Routines",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Routines_Users_UserId",
                table: "Routines");

            migrationBuilder.DropIndex(
                name: "IX_Routines_UserId",
                table: "Routines");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Routines");
        }
    }
}
