using Microsoft.EntityFrameworkCore.Migrations;

namespace TwitchBot.Data.Migrations
{
    public partial class UpdatedOperatorsBadges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Operators",
                table: "Commands",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldDefaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Operators",
                table: "Commands",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(int),
                oldDefaultValue: 0);
        }
    }
}
