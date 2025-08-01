using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScreenSoundCore.Migrations
{
    /// <inheritdoc />
    public partial class AddDateTimeMusica : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataLancamento",
                table: "Musicas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataLancamento",
                table: "Musicas");
        }
    }
}
