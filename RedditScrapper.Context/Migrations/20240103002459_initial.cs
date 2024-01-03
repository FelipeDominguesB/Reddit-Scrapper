using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RedditScrapper.Context.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Routines",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubredditName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SyncRate = table.Column<int>(type: "int", nullable: false),
                    MaxPostsPerSync = table.Column<int>(type: "int", nullable: false),
                    PostSorting = table.Column<int>(type: "int", nullable: false),
                    NextRun = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoutinesHistory",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoutinesHistory");

            migrationBuilder.DropTable(
                name: "Routines");
        }
    }
}
