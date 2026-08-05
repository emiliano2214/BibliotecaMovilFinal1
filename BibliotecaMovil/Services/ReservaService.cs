using System.Net.Http.Json;
using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Shared.Interfaces;

namespace BibliotecaMovil.Services;

public class ReservaService : IReservaService
{
    private readonly HttpClient _httpClient;

    public ReservaService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ReservaDto>> GetReservasByUsuarioIdAsync(int usuarioId)
    {
        return await _httpClient.GetFromJsonAsync<List<ReservaDto>>($"api/reserva/usuario/{usuarioId}")
               ?? new List<ReservaDto>();
    }

    public async Task<List<ReservaDto>> GetAllReservasAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<ReservaDto>>("api/reserva")
               ?? new List<ReservaDto>();
    }

    public async Task<(bool ok, string? error)> CreateReservaAsync(int idLibro)
    {
        var dto = new ReservaCreateDto { IdLibro = idLibro };
        var response = await _httpClient.PostAsJsonAsync("api/reserva", dto);

        if (response.IsSuccessStatusCode) return (true, null);

        var msg = await response.Content.ReadAsStringAsync();
        return (false, string.IsNullOrWhiteSpace(msg) ? "No se pudo crear la reserva." : msg);
    }

    public async Task<bool> CancelarReservaAsync(int reservaId)
    {
        var response = await _httpClient.DeleteAsync($"api/reserva/{reservaId}");
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> CancelarReservaAdminAsync(int reservaId)
    {
        var response = await _httpClient.DeleteAsync($"api/reserva/{reservaId}/admin");
        return response.IsSuccessStatusCode;
    }
}
