using Microsoft.EntityFrameworkCore.Migrations;

namespace MoviesApiClient.Migrations
{
    public partial class ImdbIdAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImdbId",
                table: "Reviews",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImdbId",
                table: "Reviews");
        }
    }
}
