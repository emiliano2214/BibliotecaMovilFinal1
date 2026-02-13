using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Shared.Interfaces;
using System.Net.Http.Json;

namespace BibliotecaMovil.Services;

public class UsuarioAuthService : IUsuarioAuthService
{
    private readonly HttpClient _httpClient;

    public UsuarioAuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto req)
    {
        var resp = await _httpClient.PostAsJsonAsync("api/UsuarioAuth/login", req);
        if (!resp.IsSuccessStatusCode) return null;

        return await resp.Content.ReadFromJsonAsync<LoginResponseDto>();
    }

    public async Task<bool> RegisterAsync(RegisterRequestDto req)
    {
        var resp = await _httpClient.PostAsJsonAsync("api/UsuarioAuth/register", req);
        return resp.IsSuccessStatusCode;
    }
}
