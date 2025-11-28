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
        return await _httpClient.GetFromJsonAsync<List<LibroDto>>("api/Libro") ?? new List<LibroDto>();
    }

    public async Task<LibroDto?> GetLibroByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<LibroDto>($"api/Libro/{id}");
    }

    public async Task<List<LibroDto>> GetLibrosByCategoriaAsync(int categoriaId)
    {
        return await _httpClient.GetFromJsonAsync<List<LibroDto>>($"api/Libro/categoria/{categoriaId}") ?? new List<LibroDto>();
    }
}
