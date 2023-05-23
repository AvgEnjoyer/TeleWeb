using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeleWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class Fourth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PostId",
                table: "Posts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "dislikes",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "likes",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_PostId",
                table: "Posts",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Posts_PostId",
                table: "Posts",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Posts_PostId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_PostId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "dislikes",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "likes",
                table: "Posts");
        }
    }
}
