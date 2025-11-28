using System.Net.Http.Json;
using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Shared.Interfaces;

namespace BibliotecaMovil.Services;

public class SancionService : ISancionService
{
    private readonly HttpClient _httpClient;

    public SancionService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<SancionDto>> GetSancionesByPrestamoIdAsync(int prestamoId)
    {
        return await _httpClient.GetFromJsonAsync<List<SancionDto>>($"api/sancion/prestamo/{prestamoId}") ?? new List<SancionDto>();
    }
}
