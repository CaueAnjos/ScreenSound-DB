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
                async ([FromServices] MusicsContext db, [FromBody] DefaultArtistRequest request) =>
                {
                    bool isValid = request.Validate(out string message);
                    Artist artistToAdd = request;
                    if (!isValid)
                        return Results.BadRequest(message);

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
                "/Artistas/{id}",
                async ([FromServices] MusicsContext db, [FromBody] UpdateArtistRequest request, int id) =>
                {
                    Artist? artist = await db.Artists.FirstOrDefaultAsync(a => a.Id == id);

                    if (artist is null)
                        return Results.NotFound();

                    artist.Name = request.Name ?? artist.Name;
                    artist.Bio = request.Bio ?? artist.Bio;
                    artist.PerfilPhoto = request.PerfilPhoto ?? artist.PerfilPhoto;

                    if (request.MusicsId is not null)
                    {
                        var newMusics = await db.Musics.Where(m => request.MusicsId.Contains(m.Id)).ToListAsync();

                        if (newMusics.Count != request.MusicsId.Count)
                            return Results.BadRequest("One or more musicIds are not correct!");

                        artist.Musics ??= [];
                        artist.Musics.Clear();

                        foreach (var music in newMusics)
                            artist.Musics.Add(music);
                    }

                    await db.SaveChangesAsync();

                    return Results.Ok<DefaultArtistResponse>(artist);
                }
            )
            .WithName("UpdateArtista")
            .WithOpenApi();
    }
}
