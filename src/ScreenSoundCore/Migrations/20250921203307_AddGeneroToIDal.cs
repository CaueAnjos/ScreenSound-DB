using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScreenSoundCore.Migrations
{
    /// <inheritdoc />
    public partial class AddGeneroToIDal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GeneroMusica_Genero_GenerosId",
                table: "GeneroMusica"
            );

            migrationBuilder.DropPrimaryKey(name: "PK_Genero", table: "Genero");

            migrationBuilder.RenameTable(name: "Genero", newName: "Generos");

            migrationBuilder.AddPrimaryKey(name: "PK_Generos", table: "Generos", column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GeneroMusica_Generos_GenerosId",
                table: "GeneroMusica",
                column: "GenerosId",
                principalTable: "Generos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GeneroMusica_Generos_GenerosId",
                table: "GeneroMusica"
            );

            migrationBuilder.DropPrimaryKey(name: "PK_Generos", table: "Generos");

            migrationBuilder.RenameTable(name: "Generos", newName: "Genero");

            migrationBuilder.AddPrimaryKey(name: "PK_Genero", table: "Genero", column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GeneroMusica_Genero_GenerosId",
                table: "GeneroMusica",
                column: "GenerosId",
                principalTable: "Genero",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );
        }
    }
}
