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

    // READ
    public async Task<List<CategoriaDto>> GetCategoriasAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<CategoriaDto>>("api/categoria")
               ?? new List<CategoriaDto>();
    }

    public async Task<CategoriaDto?> GetCategoriaByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<CategoriaDto>($"api/categoria/{id}");
    }

    // CREATE
    public async Task<bool> AddCategoriaAsync(CategoriaCreateDto dto)
    {
        var resp = await _httpClient.PostAsJsonAsync("api/categoria", dto);
        if (resp.IsSuccessStatusCode) return true;

        var body = await resp.Content.ReadAsStringAsync();
        throw new Exception(string.IsNullOrWhiteSpace(body)
            ? $"Error creando categoría: {resp.StatusCode}"
            : body);
    }

    // UPDATE
    public async Task<bool> UpdateCategoriaAsync(int idCategoria, CategoriaUpdateDto dto)
    {
        var resp = await _httpClient.PutAsJsonAsync($"api/categoria/{idCategoria}", dto);
        if (resp.IsSuccessStatusCode) return true;

        var body = await resp.Content.ReadAsStringAsync();
        throw new Exception(string.IsNullOrWhiteSpace(body)
            ? $"Error actualizando categoría: {resp.StatusCode}"
            : body);
    }

    // DELETE
    public async Task<bool> DeleteCategoriaAsync(int idCategoria)
    {
        var resp = await _httpClient.DeleteAsync($"api/categoria/{idCategoria}");
        if (resp.IsSuccessStatusCode) return true;

        var body = await resp.Content.ReadAsStringAsync();
        throw new Exception(string.IsNullOrWhiteSpace(body)
            ? $"Error eliminando categoría: {resp.StatusCode}"
            : body);
    }
}