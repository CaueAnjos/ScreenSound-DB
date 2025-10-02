using Microsoft.AspNetCore.Mvc;
using ScreenSoundAPI.Request;
using ScreenSoundCore.Banco;

namespace ScreenSoundAPI.endpoints;

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
                    var artistaAdded = artista.TryGetObject(db);
                    if (artistaAdded is not null)
                        return Results.Conflict();

                    artistaAdded = artista.ConvertToObject(db);
                    return Results.Created($"/Artistas/{artistaAdded.Id}", artistaAdded);
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
                        return Results.NotFound();

                    db.Artistas.Remove(artista);
                    return Results.NoContent();
                }
            )
            .WithName("DeleteArtista")
            .WithOpenApi();

        app.MapPut(
                "/Artistas",
                ([FromServices] IDal db, [FromBody] UpdateArtistaRequest artista) =>
                {
                    bool result = artista.TryUpdateObject(db);
                    if (result == true)
                        return Results.Ok();
                    return Results.NotFound();
                }
            )
            .WithName("UpdateArtista")
            .WithOpenApi();
    }
}
