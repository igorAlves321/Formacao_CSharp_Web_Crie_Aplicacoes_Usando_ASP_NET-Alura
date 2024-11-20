using ScreenSound.Web.Response;
using System.Net.Http.Json;

namespace ScreenSound.Web.Services;
public class ArtistaAPI
{
    private readonly HttpClient _httpClient;

    public ArtistaAPI(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("API");
    }

    public async Task<ICollection<ArtistaResponse>?> GetArtistasAsync()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<ICollection<ArtistaResponse>>("artistas");
        }
        catch (Exception ex)
        {
            // Logar o erro ou exibir uma mensagem de erro para o usuário
            Console.WriteLine($"Erro ao buscar artistas: {ex.Message}");
            return null;
        }
    }
}
