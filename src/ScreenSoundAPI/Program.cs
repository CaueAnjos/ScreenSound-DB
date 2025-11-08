using System.Text.Json.Serialization;
using Scalar.AspNetCore;
using ScreenSoundAPI.endpoints;
using ScreenSoundCore.Banco;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
);

builder.Services.AddDbContext<MusicsContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.AddArtistEndpoints();
app.AddMusicsEndpoints();
app.AddGenresEndpoints();

app.Run();
