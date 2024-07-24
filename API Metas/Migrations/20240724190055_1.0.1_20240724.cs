﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Metas.Migrations
{
    /// <inheritdoc />
    public partial class _101_20240724 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Porcentaje",
                table: "Estatus",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Porcentaje",
                table: "Estatus");
        }
    }
}
