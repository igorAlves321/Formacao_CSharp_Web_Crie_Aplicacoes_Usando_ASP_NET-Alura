using ScreenSound.db;
using ScreenSound.Modelos;
using ScreenSound.API.Requests;
using ScreenSound.API.Responses;
using Microsoft.AspNetCore.Mvc;

namespace ScreenSound.API.Controle;

public static class MusicasExtensions
{
    public static void AddEndPointsMusicas(this WebApplication app)
    {
        #region Endpoint Músicas
        app.MapGet("/Musicas", ([FromServices] DAL<Musica> dal) =>
        {
            var musicas = dal.Listar().Select(MusicaResponse.FromEntity).ToList();
            return Results.Ok(musicas);
        });

        app.MapGet("/Musicas/{nome}", ([FromServices] DAL<Musica> dal, string nome) =>
        {
            var musica = dal.RecuperarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
            if (musica is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(MusicaResponse.FromEntity(musica));
        });

        app.MapPost("/Musicas", ([FromServices] DAL<Musica> dal, [FromServices] DAL<Artista> dalArtista, [FromBody] MusicaRequest musicaRequest) =>
        {
            var artista = dalArtista.RecuperarPor(a => a.Id == musicaRequest.ArtistaId);
            if (artista is null)
            {
                return Results.NotFound("Artista não encontrado.");
            }

            var novaMusica = new Musica(musicaRequest.Nome)
            {
                AnoLancamento = musicaRequest.AnoLancamento,
                Artista = artista
            };

            dal.Adicionar(novaMusica);
            return Results.Ok(MusicaResponse.FromEntity(novaMusica));
        });

        app.MapPut("/Musicas/{id}", ([FromServices] DAL<Musica> dal, [FromServices] DAL<Artista> dalArtista, int id, [FromBody] MusicaRequestEdit musicaRequestEdit) =>
        {
            var musicaAEditar = dal.RecuperarPor(m => m.Id == id);
            if (musicaAEditar is null)
            {
                return Results.NotFound();
            }

            // Atualizando apenas os campos fornecidos
            if (!string.IsNullOrEmpty(musicaRequestEdit.Nome))
                musicaAEditar.Nome = musicaRequestEdit.Nome;
            if (musicaRequestEdit.AnoLancamento.HasValue)
                musicaAEditar.AnoLancamento = musicaRequestEdit.AnoLancamento;

            if (musicaRequestEdit.ArtistaId.HasValue)
            {
                var artista = dalArtista.RecuperarPor(a => a.Id == musicaRequestEdit.ArtistaId.Value);
                if (artista is null)
                {
                    return Results.NotFound("Artista não encontrado.");
                }
                musicaAEditar.Artista = artista;
            }

            dal.Atualizar(musicaAEditar);
            return Results.Ok(MusicaResponse.FromEntity(musicaAEditar));
        });

        app.MapDelete("/Musicas/{id}", ([FromServices] DAL<Musica> dal, int id) =>
        {
            var musica = dal.RecuperarPor(a => a.Id == id);
            if (musica is null)
            {
                return Results.NotFound();
            }
            dal.Deletar(musica);
            return Results.NoContent();
        });
        #endregion
    }
}
