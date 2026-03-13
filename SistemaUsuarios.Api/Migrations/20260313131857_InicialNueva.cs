using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaUsuarios.Api.Migrations
{
    /// <inheritdoc />
    public partial class InicialNueva : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "usuarios",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50)
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "usuarios",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100)
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "usuarios",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100)
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaNacimiento",
                table: "usuarios",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2")
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<string>(
                name: "Correo",
                table: "usuarios",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255)
                .OldAnnotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "usuarios",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("Relational:ColumnOrder", 1)
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateTable(
                name: "categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "provedores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contacto = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_provedores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "productos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Stock = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    IdCategoria = table.Column<int>(type: "int", nullable: false),
                    IdProvedor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_productos_categorias_IdCategoria",
                        column: x => x.IdCategoria,
                        principalTable: "categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_productos_provedores_IdProvedor",
                        column: x => x.IdProvedor,
                        principalTable: "provedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_Username",
                table: "usuarios",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_categorias_Nombre",
                table: "categorias",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_productos_IdCategoria",
                table: "productos",
                column: "IdCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_productos_IdProvedor",
                table: "productos",
                column: "IdProvedor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "productos");

            migrationBuilder.DropTable(
                name: "categorias");

            migrationBuilder.DropTable(
                name: "provedores");

            migrationBuilder.DropIndex(
                name: "IX_usuarios_Username",
                table: "usuarios");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "usuarios",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)")
                .Annotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "usuarios",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)")
                .Annotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "usuarios",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)")
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaNacimiento",
                table: "usuarios",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2")
                .Annotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<string>(
                name: "Correo",
                table: "usuarios",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)")
                .Annotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "usuarios",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 1)
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");
        }
    }
}
