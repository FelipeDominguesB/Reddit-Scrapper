using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RedditScrapper.Context.Migrations
{
    public partial class routineexecutionfiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoutinesExecutions");

            migrationBuilder.CreateTable(
                name: "RoutineExecution",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoutineId = table.Column<long>(type: "bigint", nullable: false),
                    Succeded = table.Column<bool>(type: "bit", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoutineExecution", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoutineExecution_Routines_RoutineId",
                        column: x => x.RoutineId,
                        principalTable: "Routines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoutineExecutionFile",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExecutionId = table.Column<long>(type: "bigint", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SourceUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DownloadDirectory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Classification = table.Column<int>(type: "int", nullable: false),
                    Succeded = table.Column<bool>(type: "bit", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoutineExecutionFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoutineExecutionFile_RoutineExecution_ExecutionId",
                        column: x => x.ExecutionId,
                        principalTable: "RoutineExecution",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoutineExecution_RoutineId",
                table: "RoutineExecution",
                column: "RoutineId");

            migrationBuilder.CreateIndex(
                name: "IX_RoutineExecutionFile_ExecutionId",
                table: "RoutineExecutionFile",
                column: "ExecutionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoutineExecutionFile");

            migrationBuilder.DropTable(
                name: "RoutineExecution");

            migrationBuilder.CreateTable(
                name: "RoutinesExecutions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoutineId = table.Column<long>(type: "bigint", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Succeded = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoutinesExecutions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoutinesExecutions_Routines_RoutineId",
                        column: x => x.RoutineId,
                        principalTable: "Routines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoutinesExecutions_RoutineId",
                table: "RoutinesExecutions",
                column: "RoutineId");
        }
    }
}
