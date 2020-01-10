using Microsoft.EntityFrameworkCore.Migrations;

namespace TwitchBot.Data.Migrations
{
    public partial class UpdatedOperatorsBadgesv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Operators",
                table: "Commands",
                nullable: false,
                defaultValue: 256,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Operators",
                table: "Commands",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldDefaultValue: 256);
        }
    }
}
