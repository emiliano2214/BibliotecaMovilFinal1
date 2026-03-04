using System.Net.Http.Json;
using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Shared.Interfaces;

namespace BibliotecaMovil.Services;

public class LibroService : ILibroService
{
    private readonly HttpClient _httpClient;

    public LibroService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<LibroDto>> GetLibrosAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<LibroDto>>("api/Libro")
               ?? new List<LibroDto>();
    }

    public async Task<LibroDto?> GetLibroByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<LibroDto>($"api/Libro/{id}");
    }

    public async Task<List<LibroDto>> GetLibrosByCategoriaAsync(int categoriaId)
    {
        return await _httpClient.GetFromJsonAsync<List<LibroDto>>($"api/Libro/categoria/{categoriaId}")
               ?? new List<LibroDto>();
    }

    // CREATE (incluye dto.CantidadEjemplares)
    public async Task<bool> AddLibroAsync(LibroCreateDto dto)
    {
        var resp = await _httpClient.PostAsJsonAsync("api/Libro", dto);

        if (resp.IsSuccessStatusCode) return true;

        // 👇 te deja ver el error real del backend
        var body = await resp.Content.ReadAsStringAsync();
        throw new Exception(string.IsNullOrWhiteSpace(body)
            ? $"Error creando libro: {resp.StatusCode}"
            : body);
    }

    // UPDATE (incluye dto.CantidadEjemplares)
    public async Task<bool> UpdateLibroAsync(int idLibro, LibroUpdateDto dto)
    {
        var resp = await _httpClient.PutAsJsonAsync($"api/Libro/{idLibro}", dto);

        if (resp.IsSuccessStatusCode) return true;

        var body = await resp.Content.ReadAsStringAsync();
        throw new Exception(string.IsNullOrWhiteSpace(body)
            ? $"Error actualizando libro: {resp.StatusCode}"
            : body);
    }

    public async Task<bool> DeleteLibroAsync(int idLibro)
    {
        var resp = await _httpClient.DeleteAsync($"api/Libro/{idLibro}");

        if (resp.IsSuccessStatusCode) return true;

        var body = await resp.Content.ReadAsStringAsync();
        throw new Exception(string.IsNullOrWhiteSpace(body)
            ? $"Error eliminando libro: {resp.StatusCode}"
            : body);
    }
}