using Microsoft.AspNetCore.Mvc;
using ScreenSoundAPI.Request;
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

                    var responce = artistas.Select(a => a.GetResponse()).ToArray();

                    return Results.Ok(responce);
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
                        return Results.Ok(artista.GetResponse());
                }
            )
            .WithName("ArtistaPorId")
            .WithOpenApi();

        app.MapPost(
                "/Artistas",
                ([FromServices] IDal db, [FromBody] ArtistaRequest artista) =>
                {
                    var artistAdded = new Artista(artista.name, artista.bio);
                    db.Artistas.Add(artistAdded);
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
