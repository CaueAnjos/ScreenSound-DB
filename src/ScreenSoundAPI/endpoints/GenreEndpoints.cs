using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScreenSoundAPI.dto;
using ScreenSoundCore.Banco;
using ScreenSoundCore.Modelos;

namespace ScreenSoundAPI.endpoints;

internal static class GenreEndpoints
{
    public static void AddGenresEndpoints(this WebApplication app)
    {
        app.MapGet(
                "/generos",
                async ([FromServices] MusicsContext db) =>
                {
                    var genres = await db.Genres.ToListAsync();
                    return Results.Ok(genres.Select(a => (DefaultGenreResponse)a));
                }
            )
            .WithName("Generos")
            .WithOpenApi();

        app.MapGet(
                "/generos/{id}",
                async ([FromServices] MusicsContext db, int id = -1) =>
                {
                    var genres = await db.Genres.FirstOrDefaultAsync(m => m.Id == id);
                    if (genres is null)
                        return Results.NotFound();
                    else
                        return Results.Ok((DefaultGenreResponse)genres);
                }
            )
            .WithName("GenerosPorId")
            .WithOpenApi();

        app.MapPost(
                "/generos",
                async ([FromServices] MusicsContext db, [FromBody] DefaultGenreRequest request) =>
                {
                    bool isValid = request.Validate(out string message);
                    Genre GenreToAdd = request.ToGenre(db);
                    if (!isValid)
                        return Results.BadRequest(message);

                    if (
                        await db.Genres.AnyAsync(m =>
                            string.Equals(m.Name.ToLower(), GenreToAdd.Name.ToLower())
                        )
                    )
                        return Results.Conflict();

                    await db.Genres.AddAsync(GenreToAdd);
                    await db.SaveChangesAsync();
                    return Results.Created(
                        $"/Generos/{GenreToAdd.Id}",
                        (DefaultGenreResponse)GenreToAdd
                    );
                }
            )
            .WithName("AddGeneros")
            .WithOpenApi();

        app.MapDelete(
                "/generos/{id}",
                async ([FromServices] MusicsContext db, int id) =>
                {
                    var genre = await db.Genres.FirstOrDefaultAsync(m => m.Id == id);
                    if (genre is null)
                        return Results.NotFound();

                    db.Genres.Remove(genre);
                    await db.SaveChangesAsync();
                    return Results.NoContent();
                }
            )
            .WithName("DeleteGenero")
            .WithOpenApi();

        app.MapPut(
                "/generos/{id}",
                async (
                    [FromServices] MusicsContext db,
                    [FromBody] UpdateGenreRequest request,
                    int id
                ) =>
                {
                    Genre? genre = await db.Genres.FirstOrDefaultAsync(m => m.Id == id);
                    if (genre is null)
                        return Results.NotFound();

                    genre.Name = request.Name ?? genre.Name;
                    genre.Description = request.Description ?? genre.Description;

                    await db.SaveChangesAsync();
                    return Results.Ok<DefaultGenreResponse>(genre);
                }
            )
            .WithName("UpdateGenero")
            .WithOpenApi();
    }
}
