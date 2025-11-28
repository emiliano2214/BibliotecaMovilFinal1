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
        return await _httpClient.GetFromJsonAsync<List<ReservaDto>>($"api/reserva/usuario/{usuarioId}") ?? new List<ReservaDto>();
    }

    public async Task<bool> CreateReservaAsync(ReservaDto reserva)
    {
        var response = await _httpClient.PostAsJsonAsync("api/reserva", reserva);
        return response.IsSuccessStatusCode;
    }
}
