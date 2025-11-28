using System.Net.Http.Json;
using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Shared.Interfaces;

namespace BibliotecaMovil.Services;

public class AutorService : IAutorService
{
    private readonly HttpClient _httpClient;

    public AutorService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<AutorDto>> GetAutoresAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<AutorDto>>("api/autor") ?? new List<AutorDto>();
    }

    public async Task<AutorDto?> GetAutorByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<AutorDto>($"api/autor/{id}");
    }
}
