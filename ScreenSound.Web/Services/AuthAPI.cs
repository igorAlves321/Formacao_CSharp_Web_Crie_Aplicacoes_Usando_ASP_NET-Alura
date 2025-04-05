using Microsoft.AspNetCore.Components.Authorization;
using ScreenSound.Web.Response;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;

namespace ScreenSound.Web.Services;

public class AuthAPI : AuthenticationStateProvider
{
    private readonly HttpClient _httpClient;
    private bool autenticado = false;
    private string? _accessToken;

    public AuthAPI(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("API");
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        // Inicializa com um estado "não autenticado"
        var pessoa = new ClaimsPrincipal(new ClaimsIdentity());

        try
        {
            // Verifica se temos um token
            if (!string.IsNullOrEmpty(_accessToken))
            {
                // Garante que o token está configurado no cabeçalho
                _httpClient.DefaultRequestHeaders.Authorization = 
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _accessToken);

                // Tenta acessar um endpoint protegido para verificar se o token é válido
                var response = await _httpClient.GetAsync("musicas");
                
                if (response.IsSuccessStatusCode)
                {
                    autenticado = true;
                    
                    // Como a resposta foi bem-sucedida, podemos tentar obter informações do usuário
                    try
                    {
                        var infoResponse = await _httpClient.GetAsync("auth/manage/info");
                        if (infoResponse.IsSuccessStatusCode)
                        {
                            var info = await infoResponse.Content.ReadFromJsonAsync<InfoPessoaResponse>();
                            
                            if (info != null)
                            {
                                // Monta os dados do usuário
                                Claim[] dados =
                                {
                                    new Claim(ClaimTypes.Name, info.Email ?? "usuário"),
                                    new Claim(ClaimTypes.Email, info.Email ?? "email@exemplo.com")
                                };

                                var identity = new ClaimsIdentity(dados, "JWT");
                                pessoa = new ClaimsPrincipal(identity);
                            }
                        }
                        else
                        {
                            // Mesmo sem informações detalhadas, ainda consideramos autenticado
                            // já que a requisição para musicas foi bem-sucedida
                            var identity = new ClaimsIdentity(new[] 
                            { 
                                new Claim(ClaimTypes.Name, "usuário") 
                            }, "JWT");
                            
                            pessoa = new ClaimsPrincipal(identity);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro ao obter informações do usuário: {ex.Message}");
                        
                        // Mesmo com erro, consideramos autenticado
                        var identity = new ClaimsIdentity(new[] 
                        { 
                            new Claim(ClaimTypes.Name, "usuário") 
                        }, "JWT");
                        
                        pessoa = new ClaimsPrincipal(identity);
                    }
                }
                else
                {
                    Console.WriteLine($"Erro ao verificar token: {(int)response.StatusCode}");
                    autenticado = false;
                    _accessToken = null;
                    _httpClient.DefaultRequestHeaders.Authorization = null;
                }
            }
            else
            {
                autenticado = false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao verificar autenticação: {ex.Message}");
            autenticado = false;
            _accessToken = null;
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        return new AuthenticationState(pessoa);
    }

    public async Task<AuthResponse> LoginAsync(string email, string senha)
    {
        try
        {
            Console.WriteLine($"Iniciando login para {email}");
            
            // Limpa qualquer autenticação anterior
            _accessToken = null;
            _httpClient.DefaultRequestHeaders.Authorization = null;
            
            // Formato exato esperado pelo Identity API
            var loginData = new
            {
                email = email,
                password = senha
            };
            
            // Envia a requisição de login
            var response = await _httpClient.PostAsJsonAsync("auth/login", loginData);
            var statusCode = (int)response.StatusCode;
            var responseContent = await response.Content.ReadAsStringAsync();
            
            Console.WriteLine($"Resposta do login: Status {statusCode}");
            Console.WriteLine($"Conteúdo da resposta: {responseContent}");

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    // Tenta extrair o token JWT da resposta
                    var jsonResponse = JsonDocument.Parse(responseContent);
                    
                    if (jsonResponse.RootElement.TryGetProperty("accessToken", out var tokenElement))
                    {
                        _accessToken = tokenElement.GetString();
                        
                        // Configura o token para todas as requisições futuras
                        _httpClient.DefaultRequestHeaders.Authorization = 
                            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _accessToken);
                        
                        Console.WriteLine($"Token capturado: {_accessToken?.Substring(0, 30)}...");
                    }
                    else
                    {
                        Console.WriteLine("Token não encontrado na resposta. Campos disponíveis:");
                        foreach (var prop in jsonResponse.RootElement.EnumerateObject())
                        {
                            Console.WriteLine($"- {prop.Name}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao processar token: {ex.Message}");
                }
                
                // Notifica mudança no estado de autenticação
                autenticado = true;
                NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
                
                return new AuthResponse { Sucesso = true };
            }

            return new AuthResponse
            {
                Sucesso = false,
                Erros = new[] { $"Erro de autenticação ({statusCode}): {responseContent}" }
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exceção durante login: {ex.Message}");
            
            return new AuthResponse
            {
                Sucesso = false,
                Erros = new[] { $"Erro inesperado: {ex.Message}" }
            };
        }
    }

    public async Task LogoutAsync()
    {
        try
        {
            // Envia a requisição de logout apenas se estiver autenticado
            if (!string.IsNullOrEmpty(_accessToken))
            {
                var response = await _httpClient.PostAsync("auth/logout", null);
                Console.WriteLine($"Logout status: {(int)response.StatusCode}");
            }
            
            // Limpa o token e o cabeçalho de autorização
            _accessToken = null;
            _httpClient.DefaultRequestHeaders.Authorization = null;
            
            // Atualiza o estado de autenticação
            autenticado = false;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao realizar logout: {ex.Message}");
            // Mesmo com erro, considera deslogado
            _accessToken = null;
            _httpClient.DefaultRequestHeaders.Authorization = null;
            autenticado = false;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }

    public async Task<bool> VerificaAutenticado()
    {
        // Obtém o estado de autenticação atual
        await GetAuthenticationStateAsync();
        return autenticado;
    }
}