using System.Net.Http.Json;
using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Shared.Interfaces;

namespace BibliotecaMovil.Services;

public class RolService : IRolService
{
    private readonly HttpClient _httpClient;

    public RolService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<RolDto>> GetRolesAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<RolDto>>("api/rol") ?? new List<RolDto>();
    }

    public async Task<RolDto?> GetRolByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<RolDto>($"api/rol/{id}");
    }
}
