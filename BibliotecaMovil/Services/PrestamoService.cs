using System.Net.Http.Json;
using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Shared.Interfaces;

namespace BibliotecaMovil.Services;

public class PrestamoService : IPrestamoService
{
    private readonly HttpClient _httpClient;

    public PrestamoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<PrestamoDto>> GetPrestamosByUsuarioIdAsync(int usuarioId)
    {
        return await _httpClient.GetFromJsonAsync<List<PrestamoDto>>($"api/prestamo/usuario/{usuarioId}") ?? new List<PrestamoDto>();
    }

    public async Task<bool> CreatePrestamoAsync(PrestamoDto prestamo)
    {
        var response = await _httpClient.PostAsJsonAsync("api/prestamo", prestamo);
        return response.IsSuccessStatusCode;
    }
}
