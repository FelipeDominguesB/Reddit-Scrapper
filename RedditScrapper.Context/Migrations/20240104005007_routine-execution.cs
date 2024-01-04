using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RedditScrapper.Context.Migrations
{
    public partial class routineexecution : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoutinesHistory");

            migrationBuilder.CreateTable(
                name: "RoutinesExecutions",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoutinesExecutions");

            migrationBuilder.CreateTable(
                name: "RoutinesHistory",
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
                    table.PrimaryKey("PK_RoutinesHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoutinesHistory_Routines_RoutineId",
                        column: x => x.RoutineId,
                        principalTable: "Routines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoutinesHistory_RoutineId",
                table: "RoutinesHistory",
                column: "RoutineId");
        }
    }
}
