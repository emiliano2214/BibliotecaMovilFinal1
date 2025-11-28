using System.Net.Http.Json;
using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Shared.Interfaces;

namespace BibliotecaMovil.Services;

public class UsuarioService : IUsuarioService
{
    private readonly HttpClient _httpClient;

    public UsuarioService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> LoginAsync(UsuarioDto usuario)
    {
        var response = await _httpClient.PostAsJsonAsync("api/usuario/login", usuario);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> RegisterAsync(UsuarioDto usuario)
    {
        var response = await _httpClient.PostAsJsonAsync("api/usuario/register", usuario);
        return response.IsSuccessStatusCode;
    }
}
