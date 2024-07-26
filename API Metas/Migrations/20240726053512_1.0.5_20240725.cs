using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Metas.Migrations
{
    /// <inheritdoc />
    public partial class _105_20240725 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Porcentaje",
                table: "Estatus");

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Estatus",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Estatus");

            migrationBuilder.AddColumn<int>(
                name: "Porcentaje",
                table: "Estatus",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
