using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Metas.Migrations
{
    /// <inheritdoc />
    public partial class _107_20240726 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdMeta",
                table: "Tareas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdMeta",
                table: "Tareas");
        }
    }
}
