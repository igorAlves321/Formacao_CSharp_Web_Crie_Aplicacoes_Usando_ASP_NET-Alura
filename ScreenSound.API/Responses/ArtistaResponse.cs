using ScreenSound.Modelos;
namespace ScreenSound.API.Responses;

public record ArtistaResponse(int Id, string Nome, string Bio, List<MusicaResponse> Musicas)
    {
        // Método que converte a entidade Artista em ArtistaResponse
        public static ArtistaResponse FromEntity(Artista artista)
        {
            return new ArtistaResponse(
                artista.Id,
                artista.Nome,
                artista.Bio,
                artista.Musicas?.Select(m => MusicaResponse.FromEntity(m)).ToList() ?? new List<MusicaResponse>()
            );
        }
    }

    public record MusicaResponse(int Id, string Nome, int? AnoLancamento)
    {
        // Método que converte a entidade Musica em MusicaResponse
        public static MusicaResponse FromEntity(Musica musica)
        {
            return new MusicaResponse(
                musica.Id,
                musica.Nome,
                musica.AnoLancamento
            );
        }
    }

