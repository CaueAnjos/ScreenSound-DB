using System.Text.Json.Serialization;
using ScreenSoundCore.Banco;

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
            return db.Artistas.GetAll().ToArray();
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
            return Results.NotFound();
        }
    )
    .WithName("ArtistaPorId")
    .WithOpenApi();

app.Run();
