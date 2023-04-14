using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace proyecto.API.Migrations
{
    /// <inheritdoc />
    public partial class actualizacionusuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "productos",
                columns: table => new
                {
                    idproducto = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    descripcion = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    precio = table.Column<decimal>(type: "numeric", nullable: false),
                    activo = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("productos_pkey", x => x.idproducto);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    idrol = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descripcion = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("roles_pkey", x => x.idrol);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    idusuario = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    username = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    clave = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    estado = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    idrol = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("usuarios_pkey", x => x.idusuario);
                    table.ForeignKey(
                        name: "usuarios_idrol_fkey",
                        column: x => x.idrol,
                        principalTable: "roles",
                        principalColumn: "idrol");
                });

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_idrol",
                table: "usuarios",
                column: "idrol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "productos");

            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "roles");
        }
    }
}
