using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ScreenSound.API.Endpoints;
using ScreenSound.Banco;
using ScreenSound.Modelos;
using ScreenSound.Shared.Modelos.Modelos;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Configuração do banco MySQL
builder.Services.AddDbContext<ScreenSoundContext>((options) =>
{
    options
        .UseMySql(
            builder.Configuration["ConnectionStrings:ScreenSoundDB"],
            new MySqlServerVersion(new Version(8, 0, 29)))
        .UseLazyLoadingProxies();
});

// Serviços de Repositório
builder.Services.AddTransient<DAL<Artista>>();
builder.Services.AddTransient<DAL<Musica>>();
builder.Services.AddTransient<DAL<Genero>>();

// Configuração de Serialização
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(
    options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Configuração de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:7015") // URL do frontend
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

// Configuração de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Ativar o CORS
app.UseCors("AllowFrontend");

app.UseStaticFiles();
app.AddEndPointsArtistas();
app.AddEndPointsMusicas();
app.AddEndPointGeneros();
app.UseSwagger();
app.UseSwaggerUI();

app.Run();
