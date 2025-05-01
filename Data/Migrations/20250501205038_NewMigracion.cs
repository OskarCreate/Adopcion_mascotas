using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adopcion_mascotas.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "t_adoptante",
                columns: table => new
                {
                    AdoptanteId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NombreAdoptante = table.Column<string>(type: "TEXT", nullable: false),
                    CorreoElectronico = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_adoptante", x => x.AdoptanteId);
                });

            migrationBuilder.CreateTable(
                name: "t_mascota",
                columns: table => new
                {
                    MascotaId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NombreMascota = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Tipo = table.Column<string>(type: "TEXT", nullable: false),
                    Edad = table.Column<int>(type: "INTEGER", nullable: false),
                    EstadoAdopcion = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_mascota", x => x.MascotaId);
                });

            migrationBuilder.CreateTable(
                name: "t_adopcion",
                columns: table => new
                {
                    AdopcionId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MascotaId = table.Column<int>(type: "INTEGER", nullable: false),
                    AdoptanteId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_adopcion", x => x.AdopcionId);
                    table.ForeignKey(
                        name: "FK_t_adopcion_t_adoptante_AdoptanteId",
                        column: x => x.AdoptanteId,
                        principalTable: "t_adoptante",
                        principalColumn: "AdoptanteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_adopcion_t_mascota_MascotaId",
                        column: x => x.MascotaId,
                        principalTable: "t_mascota",
                        principalColumn: "MascotaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_t_adopcion_AdoptanteId",
                table: "t_adopcion",
                column: "AdoptanteId");

            migrationBuilder.CreateIndex(
                name: "IX_t_adopcion_MascotaId",
                table: "t_adopcion",
                column: "MascotaId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_adopcion");

            migrationBuilder.DropTable(
                name: "t_adoptante");

            migrationBuilder.DropTable(
                name: "t_mascota");
        }
    }
}
