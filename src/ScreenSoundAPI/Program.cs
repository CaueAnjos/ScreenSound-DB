using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using ScreenSoundAPI.endpoints;
using ScreenSoundCore.Banco;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
);

builder.Services.AddDbContext<MusicsContext>(options =>
{
    string connectionString =
        builder.Configuration.GetConnectionString("ScreenSoundDB")
        ?? throw new InvalidOperationException("ScreenSoundDB is not set!");

    options.UseSqlServer(connectionString);
    options.UseLazyLoadingProxies();
});

var app = builder.Build();

// NOTE: this aotomaticaly apply migrations to db
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MusicsContext>();
    await db.Database.MigrateAsync();
}

// Configure the HTTP request pipeline.
app.MapScalarApiReference();
app.MapOpenApi();

app.UseHttpsRedirection();

app.AddArtistEndpoints();
app.AddMusicsEndpoints();
app.AddGenresEndpoints();

app.Run();
