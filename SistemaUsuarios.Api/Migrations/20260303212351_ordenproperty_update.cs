using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaUsuarios.Api.Migrations
{
    /// <inheritdoc />
    public partial class ordenproperty_update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "usuarios",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "usuarios",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "usuarios");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "usuarios");
        }
    }
}
