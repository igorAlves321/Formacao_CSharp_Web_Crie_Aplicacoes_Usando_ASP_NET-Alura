using ScreenSound.db;
using ScreenSound.Modelos;
using ScreenSound.API.Requests; // Importando MusicaRequest
using Microsoft.AspNetCore.Mvc;

namespace ScreenSound.API.Controle;

public static class MusicasExtensions
{
    public static void AddEndPointsMusicas(this WebApplication app)
    {
        #region Endpoint Músicas
        app.MapGet("/Musicas", ([FromServices] DAL<Musica> dal) =>
        {
            return Results.Ok(dal.Listar());
        });

        app.MapGet("/Musicas/{nome}", ([FromServices] DAL<Musica> dal, string nome) =>
        {
            var musica = dal.RecuperarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
            if (musica is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(musica);
        });

        // Modificação: Usando MusicaRequest em vez de Musica
        app.MapPost("/Musicas", ([FromServices] DAL<Musica> dal, [FromBody] MusicaRequest musicaRequest) =>
        {
            // Criando um novo objeto Musica usando os dados de MusicaRequest
            var novaMusica = new Musica
            {
                Nome = musicaRequest.Nome,
                ArtistaId = musicaRequest.ArtistaId,
                AnoLancamento = musicaRequest.AnoLancamento
            };

            // Adicionando a nova música na base de dados
            dal.Adicionar(novaMusica);
            return Results.Ok();
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

        app.MapPut("/Musicas", ([FromServices] DAL<Musica> dal, [FromBody] Musica musica) =>
        {
            var musicaAAtualizar = dal.RecuperarPor(a => a.Id == musica.Id);
            if (musicaAAtualizar is null)
            {
                return Results.NotFound();
            }
            musicaAAtualizar.Nome = musica.Nome;
            musicaAAtualizar.AnoLancamento = musica.AnoLancamento;

            dal.Atualizar(musicaAAtualizar);
            return Results.Ok();
        });
        #endregion
    }
}
