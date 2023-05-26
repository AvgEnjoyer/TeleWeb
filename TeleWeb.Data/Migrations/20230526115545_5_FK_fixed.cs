using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeleWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class _5_FK_fixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserChannelSubscription_Channels_ChannelId",
                table: "UserChannelSubscription");

            migrationBuilder.DropForeignKey(
                name: "FK_UserChannelSubscription_Users_UserId",
                table: "UserChannelSubscription");

            migrationBuilder.AddForeignKey(
                name: "FK_UserChannelSubscription_Channels_ChannelId",
                table: "UserChannelSubscription",
                column: "ChannelId",
                principalTable: "Channels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserChannelSubscription_Users_UserId",
                table: "UserChannelSubscription",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserChannelSubscription_Channels_ChannelId",
                table: "UserChannelSubscription");

            migrationBuilder.DropForeignKey(
                name: "FK_UserChannelSubscription_Users_UserId",
                table: "UserChannelSubscription");

            migrationBuilder.AddForeignKey(
                name: "FK_UserChannelSubscription_Channels_ChannelId",
                table: "UserChannelSubscription",
                column: "ChannelId",
                principalTable: "Channels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserChannelSubscription_Users_UserId",
                table: "UserChannelSubscription",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
