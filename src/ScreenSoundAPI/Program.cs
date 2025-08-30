using System.Text.Json.Serialization;
using Endpoints;
using ScreenSoundAPI.endpoints;
using ScreenSoundCore.Banco;

var builder = WebApplication.CreateBuilder(args);

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

app.AddArtistEndpoints();
app.AddMusicasEndpoints();

app.Run();
