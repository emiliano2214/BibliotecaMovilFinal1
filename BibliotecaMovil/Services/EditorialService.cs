using System.Net.Http.Json;
using System.Text.Json;
using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Shared.Interfaces;

namespace BibliotecaMovil.Services;

public class EditorialService : IEditorialService
{
    private readonly HttpClient _httpClient;

    public EditorialService(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<List<EditorialDto>> GetEditorialesAsync()
    {
        var resp = await _httpClient.GetAsync("api/editorial");
        var body = await resp.Content.ReadAsStringAsync();

        if (!resp.IsSuccessStatusCode)
            throw new Exception($"HTTP {(int)resp.StatusCode} {resp.ReasonPhrase}\n\n{body}");

        // Si el server devuelve vacío por alguna razón
        if (string.IsNullOrWhiteSpace(body))
            return new List<EditorialDto>();

        // Deserializar “a mano” para que si hay error de JSON te lo diga
        try
        {
            var data = JsonSerializer.Deserialize<List<EditorialDto>>(body,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return data ?? new List<EditorialDto>();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error parseando JSON.\n\nBody:\n{body}\n\nEx:\n{ex}");
        }
    }

    public async Task<EditorialDto?> GetEditorialByIdAsync(int id)
        => await _httpClient.GetFromJsonAsync<EditorialDto>($"api/editorial/{id}");
}
