using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoApiApp.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTableToUserDemo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "UserDemo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserDemo",
                table: "UserDemo",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserDemo",
                table: "UserDemo");

            migrationBuilder.RenameTable(
                name: "UserDemo",
                newName: "Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");
        }
    }
}
