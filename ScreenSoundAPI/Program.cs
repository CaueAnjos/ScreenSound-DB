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

builder.Services.AddSingleton<EntityContext, EntityContext>();
builder.Services.AddSingleton<IDal, MyDataBaseEntity>();

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
        () =>
        {
            IDal db = app.Services.GetRequiredService<IDal>();
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
        (int id) =>
        {
            IDal db = app.Services.GetRequiredService<IDal>();
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
        ([FromBody] Artista artista) =>
        {
            IDal db = app.Services.GetRequiredService<IDal>();
            db.Artistas.Add(artista);
        }
    )
    .WithName("AddArtista")
    .WithOpenApi();

app.Run();
