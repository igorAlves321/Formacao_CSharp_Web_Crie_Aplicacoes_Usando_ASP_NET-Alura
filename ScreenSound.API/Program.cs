using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity; // Necessário para SignInManager
using ScreenSound.API.Endpoints;
using ScreenSound.Banco;
using ScreenSound.Modelos;
using ScreenSound.Shared.Dados.Modelos; // Para PessoaComAcesso e PerfilDeAcesso
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

// Configuração de ASP.NET Core Identity
builder.Services
    .AddIdentityApiEndpoints<PessoaComAcesso>()
    .AddEntityFrameworkStores<ScreenSoundContext>();

// Configuração de Autorização
builder.Services.AddAuthorization(); // Adiciona suporte à autorização

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

// Middleware de autenticação e autorização
app.UseAuthentication(); // Necessário para processar tokens de autenticação
app.UseAuthorization();  // Necessário para verificar permissões

app.UseStaticFiles();

// Endpoints de artistas, músicas e gêneros
app.AddEndPointsArtistas();
app.AddEndPointsMusicas();
app.AddEndPointGeneros();

// Mapeamento de endpoints de identidade
app.MapGroup("auth")
   .MapIdentityApi<PessoaComAcesso>() // Configura os endpoints padrão do Identity
   .WithTags("Autorização");          // Adiciona a tag "Autorização" para organização no Swagger

// Endpoint de Logout
app.MapPost("auth/logout", async ([FromServices] SignInManager<PessoaComAcesso> signInManager) =>
{
    await signInManager.SignOutAsync(); // Remove o cookie de autenticação
    return Results.Ok();
})
.RequireAuthorization() // Requer que o usuário esteja autenticado para acessar
.WithTags("Autorização"); // Tag para organização no Swagger

// Configuração de Swagger
app.UseSwagger();
app.UseSwaggerUI();

app.Run();
