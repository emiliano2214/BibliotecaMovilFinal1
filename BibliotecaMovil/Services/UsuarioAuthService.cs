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

    public async Task<bool> LoginAsync(LoginRequestDto usuario)
    {
        var response = await _httpClient.PostAsJsonAsync("api/usuario/login", usuario);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> RegisterAsync(UsuarioDto usuarioDto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/usuario/register", usuarioDto);
        return response.IsSuccessStatusCode;
    }
}

