using Microsoft.EntityFrameworkCore.Migrations;

namespace TwitchBot.Data.Migrations
{
    public partial class WhoUsesCommand : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Operators",
                table: "Commands",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Operators",
                table: "Commands");
        }
    }
}
