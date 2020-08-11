using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace backend.Migrations
{
    public partial class Creacióndetablaclientes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(maxLength: 300, nullable: false),
                    TelefonoMovil = table.Column<string>(maxLength: 50, nullable: false),
                    Direccion = table.Column<string>(maxLength: 600, nullable: false),
                    Complemento = table.Column<string>(maxLength: 600, nullable: false),
                    Email = table.Column<string>(maxLength: 600, nullable: true),
                    TelefonoSec = table.Column<string>(maxLength: 50, nullable: true),
                    Documento = table.Column<string>(maxLength: 25, nullable: true),
                    Observacion = table.Column<string>(maxLength: 600, nullable: true),
                    Imagen = table.Column<string>(maxLength: 600, nullable: true),
                    Credito = table.Column<bool>(nullable: false),
                    MontoCredito = table.Column<decimal>(type: "numeric(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cliente");
        }
    }
}
