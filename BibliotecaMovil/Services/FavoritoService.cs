using System.Net.Http.Json;
using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Shared.Interfaces;

namespace BibliotecaMovil.Services;

public sealed class FavoritoService : IFavoritoService
{
    private readonly HttpClient _http;
    public FavoritoService(HttpClient http) => _http = http;

    public async Task<List<FavoritoDto>> GetFavoritosByUsuarioAsync(int usuarioId)
        => await _http.GetFromJsonAsync<List<FavoritoDto>>($"api/Favorito/usuario/{usuarioId}") ?? new();

    public async Task<bool> AddFavoritoAsync(FavoritoDto favorito)
    {
        var resp = await _http.PostAsJsonAsync("api/Favorito", favorito);
        return resp.IsSuccessStatusCode;
    }

    // ✅ PK compuesta (usuarioId + libroId)
    public async Task<bool> RemoveFavoritoAsync(int usuarioId, int libroId)
    {
        var resp = await _http.DeleteAsync($"api/Favorito/usuario/{usuarioId}/libro/{libroId}");
        return resp.IsSuccessStatusCode;
    }
}
