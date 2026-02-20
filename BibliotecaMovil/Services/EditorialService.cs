using System.Net.Http.Json;
using System.Text;
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

        if (string.IsNullOrWhiteSpace(body))
            return new List<EditorialDto>();

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

    public async Task<EditorialDto> CreateEditorialAsync(EditorialDto dto)
    {
        var json = JsonSerializer.Serialize(dto);
        var resp = await _httpClient.PostAsync("api/editorial",
            new StringContent(json, Encoding.UTF8, "application/json"));

        var body = await resp.Content.ReadAsStringAsync();

        if (!resp.IsSuccessStatusCode)
            throw new Exception($"HTTP {(int)resp.StatusCode} {resp.ReasonPhrase}\n\n{body}");

        // si tu API devuelve el objeto creado:
        if (!string.IsNullOrWhiteSpace(body))
        {
            try
            {
                var created = JsonSerializer.Deserialize<EditorialDto>(body,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (created != null) return created;
            }
            catch { /* si no parsea, devolvemos dto */ }
        }

        return dto;
    }

    public async Task UpdateEditorialAsync(int id, EditorialDto dto)
    {
        var json = JsonSerializer.Serialize(dto);
        var resp = await _httpClient.PutAsync($"api/editorial/{id}",
            new StringContent(json, Encoding.UTF8, "application/json"));

        var body = await resp.Content.ReadAsStringAsync();

        if (!resp.IsSuccessStatusCode)
            throw new Exception($"HTTP {(int)resp.StatusCode} {resp.ReasonPhrase}\n\n{body}");
    }
}
