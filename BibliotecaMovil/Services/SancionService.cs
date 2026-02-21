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
    public async Task<SancionDto?> CrearSancionPorTardanzaAsync(int prestamoId)
    {
        var response = await _httpClient.PostAsync($"api/sancion/crear-por-tardanza/{prestamoId}", null);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<SancionDto>();
        }
        return null;
    }
    public async Task<bool> PagarSancionAsync(int idSancion)
    {
        var response = await _httpClient.PostAsync($"api/sancion/pagar/{idSancion}", null);
        return response.IsSuccessStatusCode;
    }
    public async Task<bool> EliminarSancionAsync(int idSancion)
    {
        var response = await _httpClient.DeleteAsync($"api/sancion/{idSancion}");
        return response.IsSuccessStatusCode;
    }
    public async Task<List<SancionDto>> GetSancionesByUsuarioIdAsync(int usuarioId)
    {
        return await _httpClient.GetFromJsonAsync<List<SancionDto>>($"api/sancion/usuario/{usuarioId}") ?? new List<SancionDto>();
    }
    public async Task<SancionDto?> GetSancionByIdAsync(int idSancion)
    {
        return await _httpClient.GetFromJsonAsync<SancionDto>($"api/sancion/{idSancion}");
    }
    public async Task<List<SancionDto>> GetMisSancionesAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<SancionDto>>("api/sancion/mis")
               ?? new List<SancionDto>();
    }
    public async Task<SancionDto?> GetSancionByPrestamoIdAsync(int prestamoId)
    {
        var sanciones = await GetSancionesByPrestamoIdAsync(prestamoId);
        return sanciones.FirstOrDefault();
    }
}
