using System.Net.Http.Json;
using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Shared.Interfaces;

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
        var resp = await _httpClient.PostAsJsonAsync("api/Usuario/login", req);
        if (!resp.IsSuccessStatusCode) return null;

        return await resp.Content.ReadFromJsonAsync<LoginResponseDto>();
    }


    public async Task<bool> RegisterAsync(LoginRequestDto usuarioDto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/usuario/register", usuarioDto);
        return response.IsSuccessStatusCode;
    }
}

