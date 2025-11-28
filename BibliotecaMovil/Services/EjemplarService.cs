using System.Net.Http.Json;
using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Shared.Interfaces;

namespace BibliotecaMovil.Services;

public class EjemplarService : IEjemplarService
{
    private readonly HttpClient _httpClient;

    public EjemplarService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<EjemplarDto>> GetEjemplaresByLibroIdAsync(int libroId)
    {
        return await _httpClient.GetFromJsonAsync<List<EjemplarDto>>($"api/ejemplar/libro/{libroId}") ?? new List<EjemplarDto>();
    }

    public async Task<EjemplarDto?> GetEjemplarByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<EjemplarDto>($"api/ejemplar/{id}");
    }
}
