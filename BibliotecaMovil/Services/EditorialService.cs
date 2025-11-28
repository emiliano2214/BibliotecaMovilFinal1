using System.Net.Http.Json;
using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Shared.Interfaces;

namespace BibliotecaMovil.Services;

public class EditorialService : IEditorialService
{
    private readonly HttpClient _httpClient;

    public EditorialService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<EditorialDto>> GetEditorialesAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<EditorialDto>>("api/editorial") ?? new List<EditorialDto>();
    }

    public async Task<EditorialDto?> GetEditorialByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<EditorialDto>($"api/editorial/{id}");
    }
}
