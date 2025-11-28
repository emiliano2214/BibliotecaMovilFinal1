using System.Net.Http.Json;
using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Shared.Interfaces;

namespace BibliotecaMovil.Services;

public class CategoriaService : ICategoriaService
{
    private readonly HttpClient _httpClient;

    public CategoriaService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<CategoriaDto>> GetCategoriasAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<CategoriaDto>>("api/categoria") ?? new List<CategoriaDto>();
    }

    public async Task<CategoriaDto?> GetCategoriaByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<CategoriaDto>($"api/categoria/{id}");
    }
}
