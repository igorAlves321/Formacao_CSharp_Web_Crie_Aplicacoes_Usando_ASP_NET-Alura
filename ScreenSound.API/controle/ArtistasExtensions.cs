using ScreenSound.db;
using ScreenSound.Modelos;
using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;

namespace ScreenSound.API.Controle;

public static class ArtistasExtensions
{
    public static void AddEndPointsArtistas(this WebApplication app)
    {
        #region Endpoint Artistas
        app.MapGet("/Artistas", ([FromServices] DAL<Artista> dal) =>
        {
            return Results.Ok(dal.Listar());
        });

        app.MapGet("/Artistas/{nome}", ([FromServices] DAL<Artista> dal, string nome) =>
        {
            var artista = dal.RecuperarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
            if (artista is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(artista);
        });

        // Modificação: Usando ArtistaRequest em vez de Artista
        app.MapPost("/Artistas", ([FromServices] DAL<Artista> dal, [FromBody] ArtistaRequest artistaRequest) =>
        {
            // Criando um novo objeto Artista usando os dados de ArtistaRequest
            var novoArtista = new Artista(artistaRequest.nome, artistaRequest.bio);

            // Adicionando o novo artista na base de dados
            dal.Adicionar(novoArtista);
            return Results.Ok();
        });

        app.MapDelete("/Artistas/{id}", ([FromServices] DAL<Artista> dal, int id) => {
            var artista = dal.RecuperarPor(a => a.Id == id);
            if (artista is null)
            {
                return Results.NotFound();
            }
            dal.Deletar(artista);
            return Results.NoContent();
        });

        app.MapPut("/Artistas", ([FromServices] DAL<Artista> dal, [FromBody] Artista artista) => {
            var artistaAAtualizar = dal.RecuperarPor(a => a.Id == artista.Id);
            if (artistaAAtualizar is null)
            {
                return Results.NotFound();
            }
            artistaAAtualizar.Nome = artista.Nome;
            artistaAAtualizar.Bio = artista.Bio;
            artistaAAtualizar.FotoPerfil = artista.FotoPerfil;

            dal.Atualizar(artistaAAtualizar);
            return Results.Ok();
        });
        #endregion
    }
}
