using Microsoft.AspNetCore.Mvc;
using ScreenSoundCore.Banco;
using ScreenSoundCore.Modelos;

namespace Endpoints;

internal static class ArtistEndpoints
{
    public static void AddArtistEndpoints(this WebApplication app)
    {
        app.MapGet(
                "/Artistas",
                ([FromServices] IDal db) =>
                {
                    var artistas = db.Artistas.GetAll().ToArray();
                    if (artistas is null)
                        return Results.NotFound();
                    else
                        return Results.Ok(artistas);
                }
            )
            .WithName("Artistas")
            .WithOpenApi();

        app.MapGet(
                "/Artistas/{id}",
                ([FromServices] IDal db, int id) =>
                {
                    var artista = db.Artistas.GetById(id);
                    if (artista is null)
                        return Results.NotFound();
                    else
                        return Results.Ok(artista);
                }
            )
            .WithName("ArtistaPorId")
            .WithOpenApi();

        app.MapPost(
                "/Artistas",
                ([FromServices] IDal db, [FromBody] Artista artista) =>
                {
                    db.Artistas.Add(artista);
                }
            )
            .WithName("AddArtista")
            .WithOpenApi();

        app.MapDelete(
                "/Artistas/{id}",
                ([FromServices] IDal db, int id) =>
                {
                    var artista = db.Artistas.GetById(id);
                    if (artista is null)
                    {
                        return Results.NotFound();
                    }
                    else
                    {
                        db.Artistas.Remove(artista);
                        return Results.NoContent();
                    }
                }
            )
            .WithName("DeleteArtista")
            .WithOpenApi();

        app.MapPut(
                "/Artistas",
                ([FromServices] IDal db, [FromBody] Artista artista) =>
                {
                    int id = artista.Id;
                    var artistToUpdate = db.Artistas.GetById(id);
                    if (artistToUpdate is null)
                    {
                        return Results.NotFound();
                    }
                    else
                    {
                        artistToUpdate.Musicas = artista.Musicas;
                        artistToUpdate.Nome = artista.Nome;
                        artistToUpdate.FotoPerfil = artista.FotoPerfil;
                        artistToUpdate.Bio = artista.Bio;

                        db.Artistas.Update(artistToUpdate);
                        return Results.Ok();
                    }
                }
            )
            .WithName("UpdateArtista")
            .WithOpenApi();
    }
}
