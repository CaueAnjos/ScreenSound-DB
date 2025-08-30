using Microsoft.AspNetCore.Mvc;
using ScreenSoundAPI.Request;
using ScreenSoundCore.Banco;
using ScreenSoundCore.Modelos;

namespace ScreenSoundAPI.endpoints;

internal static class MusicasEndpoints
{
    public static void AddMusicasEndpoints(this WebApplication app)
    {
        app.MapGet(
                "/Musicas",
                ([FromServices] IDal db) =>
                {
                    var musicas = db.Musicas.GetAll().ToArray();
                    if (musicas is null)
                        return Results.NotFound();
                    else
                        return Results.Ok(musicas);
                }
            )
            .WithName("Musicas")
            .WithOpenApi();

        app.MapGet(
                "/Musicas/{id}",
                ([FromServices] IDal db, int id) =>
                {
                    var musica = db.Musicas.GetById(id);
                    if (musica is null)
                        return Results.NotFound();
                    else
                        return Results.Ok(musica);
                }
            )
            .WithName("MusicasPorId")
            .WithOpenApi();

        app.MapPost(
                "/Musicas",
                ([FromServices] IDal db, [FromBody] MusicaRequest artista) =>
                {
                    db.Musicas.Add(new Musica(artista.name));
                }
            )
            .WithName("AddMusicas")
            .WithOpenApi();

        app.MapDelete(
                "/Musicas/{id}",
                ([FromServices] IDal db, int id) =>
                {
                    var musica = db.Musicas.GetById(id);
                    if (musica is null)
                    {
                        return Results.NotFound();
                    }
                    else
                    {
                        db.Musicas.Remove(musica);
                        return Results.NoContent();
                    }
                }
            )
            .WithName("DeleteMusica")
            .WithOpenApi();

        app.MapPut(
                "/Musicas",
                ([FromServices] IDal db, [FromBody] Musica musica) =>
                {
                    int id = musica.Id;
                    var musicToUpdate = db.Musicas.GetById(id);
                    if (musicToUpdate is null)
                    {
                        return Results.NotFound();
                    }
                    else
                    {
                        musicToUpdate.Artista = musica.Artista;
                        musicToUpdate.DataLancamento = musica.DataLancamento;

                        db.Musicas.Update(musicToUpdate);
                        return Results.Ok();
                    }
                }
            )
            .WithName("UpdateMusica")
            .WithOpenApi();
    }
}
