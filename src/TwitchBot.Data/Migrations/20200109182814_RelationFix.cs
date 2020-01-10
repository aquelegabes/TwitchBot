using Microsoft.EntityFrameworkCore.Migrations;

namespace TwitchBot.Data.Migrations
{
    public partial class RelationFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commands_Channels_Id",
                table: "Commands");

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Commands",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Commands_CreatedById",
                table: "Commands",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Commands_Channels_CreatedById",
                table: "Commands",
                column: "CreatedById",
                principalTable: "Channels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commands_Channels_CreatedById",
                table: "Commands");

            migrationBuilder.DropIndex(
                name: "IX_Commands_CreatedById",
                table: "Commands");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Commands");

            migrationBuilder.AddForeignKey(
                name: "FK_Commands_Channels_Id",
                table: "Commands",
                column: "Id",
                principalTable: "Channels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
