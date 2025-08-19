using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pacientes.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Citas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                    Tipo = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    PacienteCedula = table.Column<string>(type: "nvarchar(25)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pacientes",
                columns: table => new
                {
                    Cedula = table.Column<string>(type: "nvarchar(25)", nullable: false),
                    NombreCompleto = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Edad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.Cedula);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                    Usuario = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Contrasenia = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Citas");

            migrationBuilder.DropTable(
                name: "Pacientes");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
