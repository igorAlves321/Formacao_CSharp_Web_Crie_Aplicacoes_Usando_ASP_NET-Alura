namespace ScreenSound.API.Requests;

    public record ArtistaRequestEdit(string? Nome, string? Bio);
public record MusicaRequestEdit(string? Nome, int? AnoLancamento, int? ArtistaId);