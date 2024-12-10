using Microsoft.AspNetCore.Components.Authorization;
using ScreenSound.Web.Response;
using System.Net.Http.Json;
using System.Security.Claims;

namespace ScreenSound.Web.Services;

public class AuthAPI : AuthenticationStateProvider
{
    private readonly HttpClient _httpClient;
    private bool autenticado = false; // Novo campo para rastrear o estado de autenticação.

    public AuthAPI(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("API");
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        // Inicializa com um estado "não autenticado".
        autenticado = false;
        var pessoa = new ClaimsPrincipal();

        try
        {
            // Faz a requisição para obter informações do usuário autenticado.
            var response = await _httpClient.GetAsync("auth/manage/info");

            if (response.IsSuccessStatusCode)
            {
                // Lê os dados do usuário autenticado.
                var info = await response.Content.ReadFromJsonAsync<InfoPessoaResponse>();

                if (info is not null)
                {
                    // Monta os dados (claims) do usuário.
                    Claim[] dados =
                    {
                        new Claim(ClaimTypes.Name, info.Email),
                        new Claim(ClaimTypes.Email, info.Email)
                    };

                    // Cria a identidade do usuário autenticado.
                    var identity = new ClaimsIdentity(dados, "Cookies");

                    // Atualiza a pessoa autenticada e o estado.
                    pessoa = new ClaimsPrincipal(identity);
                    autenticado = true;
                }
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Erro de rede ao verificar autenticação: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro inesperado ao verificar autenticação: {ex.Message}");
        }

        // Retorna o estado de autenticação.
        return new AuthenticationState(pessoa);
    }

    public async Task<AuthResponse> LoginAsync(string email, string senha)
    {
        try
        {
            // Envia a requisição de login para a API.
            var response = await _httpClient.PostAsJsonAsync("auth/login", new
            {
                email,
                password = senha
            });

            if (response.IsSuccessStatusCode)
            {
                NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
                return new AuthResponse { Sucesso = true };
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            return new AuthResponse
            {
                Sucesso = false,
                Erros = new List<string> { $"Erro de autenticação: {errorContent}" }
            };
        }
        catch (Exception ex)
        {
            return new AuthResponse
            {
                Sucesso = false,
                Erros = new List<string> { $"Erro inesperado: {ex.Message}" }
            };
        }
    }

    public async Task LogoutAsync()
    {
        try
        {
            // Envia a requisição de logout para a API.
            await _httpClient.PostAsync("auth/logout", null);

            // Notifica a aplicação para que ela saiba que o usuário foi deslogado.
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao realizar logout: {ex.Message}");
        }
    }

    public async Task<bool> VerificaAutenticado()
    {
        await GetAuthenticationStateAsync();
        return autenticado;
    }
}
