using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using ScreenSoundCore.Banco;
using ScreenSoundCore.Modelos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
);

builder.Services.AddDbContext<EntityContext>();
builder.Services.AddTransient<IDal, MyDataBaseEntity>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

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

app.Run();
