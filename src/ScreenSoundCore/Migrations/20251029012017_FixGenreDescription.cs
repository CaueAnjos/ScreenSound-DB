using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScreenSoundCore.Migrations
{
    /// <inheritdoc />
    public partial class FixGenreDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "Genres",
                newName: "Description");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Genres",
                newName: "Descricao");
        }
    }
}
