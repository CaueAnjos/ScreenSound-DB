using Microsoft.AspNetCore.Mvc;
using ScreenSoundAPI.dto;
using ScreenSoundAPI.Request;
using ScreenSoundCore.Banco;

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

                    var responce = musicas.Select(a => a.GetResponse()).ToArray();
                    return Results.Ok(responce);
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
                            return Results.Ok(musica.GetResponse());
                    }
                )
                .WithName("MusicasPorId")
                .WithOpenApi();

        app.MapPost(
                        "/Musicas",
                        ([FromServices] IDal db, [FromBody] DefaultMusicRequest musica) =>
                        {
                            var musicaAdded = musica.TryGetObject(db);
                            if (musicaAdded is not null)
                                return Results.Conflict();

                            musicaAdded = musica.ConvertToObject(db);
                            return Results.Created($"/Musicas/{musicaAdded.Id}", musicaAdded);
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
                            return Results.NotFound();

                        db.Musicas.Remove(musica);
                        return Results.NoContent();
                    }
                )
                .WithName("DeleteMusica")
                .WithOpenApi();

        app.MapPut(
                "/Musicas",
                ([FromServices] IDal db, [FromBody] UpdateMusicaRequest musica) =>
                {
                    bool result = musica.TryUpdateObject(db);
                    if (result == true)
                        return Results.Ok();
                    return Results.NotFound();
                }
            )
            .WithName("UpdateMusica")
            .WithOpenApi();
    }
}
