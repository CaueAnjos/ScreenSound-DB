using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScreenSoundAPI.dto;
using ScreenSoundAPI.Request;
using ScreenSoundCore.Banco;
using ScreenSoundCore.Modelos;

namespace ScreenSoundAPI.endpoints;

internal static class MusicEndpoints
{
    public static void AddMusicasEndpoints(this WebApplication app)
    {
        app.MapGet(
                "/Musicas",
                async ([FromServices] MusicsContext db) =>
                {
                    var musics = await db.Musics.ToListAsync();
                    return Results.Ok(musics.Select(a => (DefaultMusicResponse)a));
                }
            )
            .WithName("Musicas")
            .WithOpenApi();

        app.MapGet(
                    "/Musicas/{id}",
                    async ([FromServices] MusicsContext db, int id) =>
                    {
                        var music = await db.Musics.FirstOrDefaultAsync(m => m.Id == id);
                        if (music is null)
                            return Results.NotFound();
                        else
                            return Results.Ok((DefaultMusicResponse)music);
                    }
                )
                .WithName("MusicasPorId")
                .WithOpenApi();

        app.MapPost(
                        "/Musicas",
                        async ([FromServices] MusicsContext db, [FromBody] DefaultMusicRequest request) =>
                        {
                            Music musicToAdd = request;

                            if (await db.Musics.AnyAsync(m => string.Equals(m.Name.ToLower(), musicToAdd.Name.ToLower())))
                                return Results.Conflict();

                            await db.Musics.AddAsync(musicToAdd);
                            await db.SaveChangesAsync();
                            return Results.Created($"/Musicas/{musicToAdd}.Id", (DefaultMusicResponse)musicToAdd);
                        }
                    )
                    .WithName("AddMusicas")
                    .WithOpenApi();

        app.MapDelete(
                    "/Musicas/{id}",
                    async ([FromServices] MusicsContext db, int id) =>
                    {
                        var music = await db.Musics.FirstOrDefaultAsync(m => m.Id == id);
                        if (music is null)
                            return Results.NotFound();

                        db.Musics.Remove(music);
                        await db.SaveChangesAsync();
                        return Results.NoContent();
                    }
                )
                .WithName("DeleteMusica")
                .WithOpenApi();

        app.MapPut(
                "/Musicas/{id}",
                async ([FromServices] MusicsContext db, [FromBody] UpdateMusicRequest request, int id) =>
                {
                    Music? music = await db.Musics.FirstOrDefaultAsync(m => m.Id == id);
                    if (music is null)
                        return Results.NotFound();

                    music.Name = request.Name ?? music.Name;
                    music.ReleaseDate = request.ReleaseDate ?? music.ReleaseDate;

                    if (request.ArtistId is null)
                    {
                        music.Artist = null;
                        return Results.Ok<DefaultMusicResponse>(music);
                    }
                    else if (request.ArtistId >= 0)
                    {
                        Artist? artist = await db.Artists.FirstOrDefaultAsync(a => a.Id == request.ArtistId);

                        if (artist is null)
                            return Results.BadRequest("ArtistId is not correct!");

                        music.Artist = artist;
                    }

                    if (request.GenresId is not null)
                    {
                        var newGenres = await db.Genres.Where(g => request.GenresId.Contains(g.Id)).ToListAsync();

                        if (newGenres.Count != request.GenresId.Count)
                            return Results.BadRequest("One or more genresId are not correct!");

                        music.Genres ??= [];
                        music.Genres.Clear();

                        foreach (var genre in newGenres)
                        {
                            music.Genres.Add(genre);
                        }
                    }

                    await db.SaveChangesAsync();
                    return Results.Ok<DefaultMusicResponse>(music);
                }
            )
            .WithName("UpdateMusica")
            .WithOpenApi();
    }
}
