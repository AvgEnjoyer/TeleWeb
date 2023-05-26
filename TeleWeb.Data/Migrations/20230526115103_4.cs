using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeleWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class _4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChannelAdmin_Channels_ChannelId",
                table: "ChannelAdmin");

            migrationBuilder.DropForeignKey(
                name: "FK_ChannelAdmin_Users_AdminId",
                table: "ChannelAdmin");

            migrationBuilder.AddForeignKey(
                name: "FK_ChannelAdmin_Channels_ChannelId",
                table: "ChannelAdmin",
                column: "ChannelId",
                principalTable: "Channels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChannelAdmin_Users_AdminId",
                table: "ChannelAdmin",
                column: "AdminId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChannelAdmin_Channels_ChannelId",
                table: "ChannelAdmin");

            migrationBuilder.DropForeignKey(
                name: "FK_ChannelAdmin_Users_AdminId",
                table: "ChannelAdmin");

            migrationBuilder.AddForeignKey(
                name: "FK_ChannelAdmin_Channels_ChannelId",
                table: "ChannelAdmin",
                column: "ChannelId",
                principalTable: "Channels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChannelAdmin_Users_AdminId",
                table: "ChannelAdmin",
                column: "AdminId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
