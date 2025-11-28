using System.Net.Http.Json;
using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Shared.Interfaces;

namespace BibliotecaMovil.Services;

public class FavoritoService : IFavoritoService
{
    private readonly HttpClient _httpClient;

    public FavoritoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<FavoritoDto>> GetFavoritosByUsuarioIdAsync(int usuarioId)
    {
        return await _httpClient.GetFromJsonAsync<List<FavoritoDto>>($"api/favorito/usuario/{usuarioId}") ?? new List<FavoritoDto>();
    }

    public async Task<bool> AddFavoritoAsync(FavoritoDto favorito)
    {
        var response = await _httpClient.PostAsJsonAsync("api/favorito", favorito);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> RemoveFavoritoAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/favorito/{id}");
        return response.IsSuccessStatusCode;
    }
}
