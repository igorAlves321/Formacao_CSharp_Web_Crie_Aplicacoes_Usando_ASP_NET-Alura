using ScreenSound.db;
using ScreenSound.Modelos;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.controle;

var builder = WebApplication.CreateBuilder(args);

// Configuração para ignorar referências cíclicas
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Configurações de serviços e injeção de dependência
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ScreenSoundContext>();  // Injeção do contexto do banco de dados
builder.Services.AddTransient<DAL<Artista>>();        // Injeção do DAL para Artista
builder.Services.AddTransient<DAL<Musica>>();         // Injeção do DAL para Musica

var app = builder.Build();

// Configuração do Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Chamando os métodos de extensão para adicionar os endpoints
app.AddEndPointsArtistas();
app.AddEndPointsMusicas();

app.Run();