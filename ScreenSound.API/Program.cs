using ScreenSound.db;
using ScreenSound.Modelos;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Configuração para ignorar referências cíclicas
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Configurações de serviços
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuração do Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Endpoint para adicionar um artista
app.MapPost("/artistas", ([FromBody] Artista artista) =>
{
    var dal = new DAL<Artista>(new ScreenSoundContext());
    dal.Adicionar(artista);
    return Results.Ok();
})
.WithName("AddArtista");

// Endpoint para listar todos os artistas
app.MapGet("/artistas", () =>
{
    var dal = new DAL<Artista>(new ScreenSoundContext());
    return Results.Ok(dal.Listar());
})
.WithName("GetArtistas");

// Endpoint para buscar um artista pelo nome
app.MapGet("/artistas/{nome}", (string nome) =>
{
    var dal = new DAL<Artista>(new ScreenSoundContext());
    var artista = dal.RecuperarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
    if (artista is null)
    {
        return Results.NotFound();
    }
    return Results.Ok(artista);
})
.WithName("GetArtistaByName");

app.Run();
