using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estudos.Migrations
{
    /// <inheritdoc />
    public partial class UserCharacterRelationShip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "userId",
                table: "Characters",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Characters_userId",
                table: "Characters",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_users_userId",
                table: "Characters",
                column: "userId",
                principalTable: "users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_users_userId",
                table: "Characters");

            migrationBuilder.DropIndex(
                name: "IX_Characters_userId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "Characters");
        }
    }
}
