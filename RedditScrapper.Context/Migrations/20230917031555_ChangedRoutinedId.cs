using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RedditScrapper.Context.Migrations
{
    public partial class ChangedRoutinedId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SyncHistory_SyncRoutines_routineId",
                table: "SyncHistory");

            migrationBuilder.RenameColumn(
                name: "routineId",
                table: "SyncHistory",
                newName: "RoutineId");

            migrationBuilder.RenameIndex(
                name: "IX_SyncHistory_routineId",
                table: "SyncHistory",
                newName: "IX_SyncHistory_RoutineId");

            migrationBuilder.AddForeignKey(
                name: "FK_SyncHistory_SyncRoutines_RoutineId",
                table: "SyncHistory",
                column: "RoutineId",
                principalTable: "SyncRoutines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SyncHistory_SyncRoutines_RoutineId",
                table: "SyncHistory");

            migrationBuilder.RenameColumn(
                name: "RoutineId",
                table: "SyncHistory",
                newName: "routineId");

            migrationBuilder.RenameIndex(
                name: "IX_SyncHistory_RoutineId",
                table: "SyncHistory",
                newName: "IX_SyncHistory_routineId");

            migrationBuilder.AddForeignKey(
                name: "FK_SyncHistory_SyncRoutines_routineId",
                table: "SyncHistory",
                column: "routineId",
                principalTable: "SyncRoutines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
