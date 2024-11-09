using ScreenSound.db;
using ScreenSound.Modelos;
using ScreenSound.API.Requests;
using ScreenSound.API.Responses; // Importando os responses
using Microsoft.AspNetCore.Mvc;

namespace ScreenSound.API.Controle;

public static class ArtistasExtensions
{
    public static void AddEndPointsArtistas(this WebApplication app)
    {
        #region Endpoint Artistas
        app.MapGet("/Artistas", ([FromServices] DAL<Artista> dal) =>
        {
            var artistas = dal.Listar().Select(ArtistaResponse.FromEntity).ToList();
            return Results.Ok(artistas);
        });

        app.MapGet("/Artistas/{nome}", ([FromServices] DAL<Artista> dal, string nome) =>
        {
            var artista = dal.RecuperarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
            if (artista is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(ArtistaResponse.FromEntity(artista));
        });

        app.MapPost("/Artistas", ([FromServices] DAL<Artista> dal, [FromBody] ArtistaRequest artistaRequest) =>
        {
            var novoArtista = new Artista(artistaRequest.Nome, artistaRequest.Bio);
            dal.Adicionar(novoArtista);
            return Results.Ok(ArtistaResponse.FromEntity(novoArtista));
        });

        app.MapPut("/Artistas/{id}", ([FromServices] DAL<Artista> dal, int id, [FromBody] ArtistaRequestEdit artistaRequestEdit) =>
        {
            var artistaAEditar = dal.RecuperarPor(a => a.Id == id);
            if (artistaAEditar is null)
            {
                return Results.NotFound();
            }

            // Atualizando apenas os campos fornecidos
            if (!string.IsNullOrEmpty(artistaRequestEdit.Nome))
                artistaAEditar.Nome = artistaRequestEdit.Nome;
            if (!string.IsNullOrEmpty(artistaRequestEdit.Bio))
                artistaAEditar.Bio = artistaRequestEdit.Bio;

            dal.Atualizar(artistaAEditar);
            return Results.Ok(ArtistaResponse.FromEntity(artistaAEditar));
        });

        app.MapDelete("/Artistas/{id}", ([FromServices] DAL<Artista> dal, int id) =>
        {
            var artista = dal.RecuperarPor(a => a.Id == id);
            if (artista is null)
            {
                return Results.NotFound();
            }
            dal.Deletar(artista);
            return Results.NoContent();
        });
        #endregion
    }
}
