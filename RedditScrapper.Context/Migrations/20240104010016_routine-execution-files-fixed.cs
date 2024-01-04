using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RedditScrapper.Context.Migrations
{
    public partial class routineexecutionfilesfixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoutineExecution_Routines_RoutineId",
                table: "RoutineExecution");

            migrationBuilder.DropForeignKey(
                name: "FK_RoutineExecutionFile_RoutineExecution_ExecutionId",
                table: "RoutineExecutionFile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoutineExecutionFile",
                table: "RoutineExecutionFile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoutineExecution",
                table: "RoutineExecution");

            migrationBuilder.RenameTable(
                name: "RoutineExecutionFile",
                newName: "RoutineExecutionsFiles");

            migrationBuilder.RenameTable(
                name: "RoutineExecution",
                newName: "RoutinesExecutions");

            migrationBuilder.RenameIndex(
                name: "IX_RoutineExecutionFile_ExecutionId",
                table: "RoutineExecutionsFiles",
                newName: "IX_RoutineExecutionsFiles_ExecutionId");

            migrationBuilder.RenameIndex(
                name: "IX_RoutineExecution_RoutineId",
                table: "RoutinesExecutions",
                newName: "IX_RoutinesExecutions_RoutineId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoutineExecutionsFiles",
                table: "RoutineExecutionsFiles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoutinesExecutions",
                table: "RoutinesExecutions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoutineExecutionsFiles_RoutinesExecutions_ExecutionId",
                table: "RoutineExecutionsFiles",
                column: "ExecutionId",
                principalTable: "RoutinesExecutions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoutinesExecutions_Routines_RoutineId",
                table: "RoutinesExecutions",
                column: "RoutineId",
                principalTable: "Routines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoutineExecutionsFiles_RoutinesExecutions_ExecutionId",
                table: "RoutineExecutionsFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_RoutinesExecutions_Routines_RoutineId",
                table: "RoutinesExecutions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoutinesExecutions",
                table: "RoutinesExecutions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoutineExecutionsFiles",
                table: "RoutineExecutionsFiles");

            migrationBuilder.RenameTable(
                name: "RoutinesExecutions",
                newName: "RoutineExecution");

            migrationBuilder.RenameTable(
                name: "RoutineExecutionsFiles",
                newName: "RoutineExecutionFile");

            migrationBuilder.RenameIndex(
                name: "IX_RoutinesExecutions_RoutineId",
                table: "RoutineExecution",
                newName: "IX_RoutineExecution_RoutineId");

            migrationBuilder.RenameIndex(
                name: "IX_RoutineExecutionsFiles_ExecutionId",
                table: "RoutineExecutionFile",
                newName: "IX_RoutineExecutionFile_ExecutionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoutineExecution",
                table: "RoutineExecution",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoutineExecutionFile",
                table: "RoutineExecutionFile",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoutineExecution_Routines_RoutineId",
                table: "RoutineExecution",
                column: "RoutineId",
                principalTable: "Routines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoutineExecutionFile_RoutineExecution_ExecutionId",
                table: "RoutineExecutionFile",
                column: "ExecutionId",
                principalTable: "RoutineExecution",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
