using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RedditScrapper.Context.Migrations
{
    public partial class routineexecutionproperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Succeded",
                table: "RoutinesExecutions");

            migrationBuilder.AddColumn<int>(
                name: "MaxPostsPerSync",
                table: "RoutinesExecutions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PostSorting",
                table: "RoutinesExecutions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SyncRate",
                table: "RoutinesExecutions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalLinksFound",
                table: "RoutinesExecutions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxPostsPerSync",
                table: "RoutinesExecutions");

            migrationBuilder.DropColumn(
                name: "PostSorting",
                table: "RoutinesExecutions");

            migrationBuilder.DropColumn(
                name: "SyncRate",
                table: "RoutinesExecutions");

            migrationBuilder.DropColumn(
                name: "TotalLinksFound",
                table: "RoutinesExecutions");

            migrationBuilder.AddColumn<bool>(
                name: "Succeded",
                table: "RoutinesExecutions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
