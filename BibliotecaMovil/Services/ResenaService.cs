using System.Net.Http.Json;
using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Shared.Interfaces;

namespace BibliotecaMovil.Services;

public class ResenaService : IResenaService
{
    private readonly HttpClient _httpClient;

    public ResenaService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ResenaDto>> GetResenasByLibroIdAsync(int libroId)
    {
        return await _httpClient.GetFromJsonAsync<List<ResenaDto>>($"api/resena/libro/{libroId}") ?? new List<ResenaDto>();
    }

    public async Task<int?> GetLibroIdByTituloAsync(string titulo)
    {
        var resp = await _httpClient.GetAsync($"api/Resena/libroId-por-titulo?titulo={Uri.EscapeDataString(titulo)}");
        if (!resp.IsSuccessStatusCode) return null;

        return await resp.Content.ReadFromJsonAsync<int>();
    }

    public async Task<bool> CreateResenaAsync(ResenaDto resena)
    {
        var response = await _httpClient.PostAsJsonAsync("api/resena", resena);
        return response.IsSuccessStatusCode;
    }
}
