using BibliotecaMovil.Shared.DTOs;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace BibliotecaMovil.Services;

public class CloudinaryService
{
    private readonly HttpClient _http;

    public CloudinaryService(HttpClient http)
    {
        _http = http;
    }

    public async Task<CloudinaryUploadResultDto?> SubirImagenAsync(IBrowserFile archivo, string carpeta)
    {
        if (archivo == null)
            return null;

        using var content = new MultipartFormDataContent();

        var streamContent = new StreamContent(
            archivo.OpenReadStream(10 * 1024 * 1024)
        );

        streamContent.Headers.ContentType =
            new MediaTypeHeaderValue(archivo.ContentType);

        content.Add(streamContent, "archivo", archivo.Name);
        content.Add(new StringContent(carpeta), "carpeta");

        var response = await _http.PostAsync("api/cloudinary/upload", content);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<CloudinaryUploadResultDto>();
    }
}