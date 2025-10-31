using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScreenSoundAPI.dto;
using ScreenSoundCore.Banco;
using ScreenSoundCore.Modelos;

namespace ScreenSoundAPI.endpoints;

internal static class ArtistEndpoints
{
    public static void AddArtistEndpoints(this WebApplication app)
    {
        app.MapGet(
                "/Artistas",
                async ([FromServices] MusicsContext db) =>
                {
                    var artists = await db.Artists.ToListAsync();
                    return Results.Ok(artists.Select(a => (DefaultArtistResponse)a));
                }
            )
            .WithName("Artistas")
            .WithOpenApi();

        app.MapGet(
                "/Artistas/{id}",
                async ([FromServices] MusicsContext db, int id) =>
                {
                    var artist = await db.Artists.FirstOrDefaultAsync(a => a.Id == id);
                    if (artist is null)
                        return Results.NotFound();
                    else
                        return Results.Ok((DefaultArtistResponse)artist);
                }
            )
            .WithName("ArtistaPorId")
            .WithOpenApi();

        app.MapPost(
                "/Artistas",
                async ([FromServices] MusicsContext db, [FromBody] DefaultArtistRequest resquest) =>
                {
                    Artist artistToAdd = resquest;

                    if (await db.Artists.AnyAsync(a => string.Equals(a.Name.ToLower(), artistToAdd.Name.ToLower())))
                        return Results.Conflict();

                    await db.Artists.AddAsync(artistToAdd);
                    await db.SaveChangesAsync();
                    return Results.Created($"/Artistas/{artistToAdd.Id}", (DefaultArtistResponse)artistToAdd);
                }
            )
            .WithName("AddArtista")
            .WithOpenApi();

        app.MapDelete(
                "/Artistas/{id}",
                async ([FromServices] MusicsContext db, int id) =>
                {
                    var artist = await db.Artists.FirstOrDefaultAsync(a => a.Id == id);
                    if (artist is null)
                        return Results.NotFound();

                    db.Artists.Remove(artist);
                    await db.SaveChangesAsync();
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
