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
            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "t_mascota",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "t_mascota");
        }
    }
}
