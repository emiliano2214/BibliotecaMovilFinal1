using System.Text.Json;
using BibliotecaMovil.Shared.DTOs;

namespace BibliotecaMovil.Services;

public class UsuarioSesionService
{
    private const string TOKEN_KEY = "authToken";
    private const string USER_KEY = "authUser";

    public async Task GuardarSesionAsync(LoginResponseDto login)
    {
        await SecureStorage.SetAsync(TOKEN_KEY, login.Token);

        var userJson = JsonSerializer.Serialize(login.User);
        await SecureStorage.SetAsync(USER_KEY, userJson);
    }

    public async Task<string?> ObtenerTokenAsync()
        => await SecureStorage.GetAsync(TOKEN_KEY);

    public async Task<UsuarioPublicoDto?> ObtenerUsuarioAsync()
    {
        var json = await SecureStorage.GetAsync(USER_KEY);
        if (string.IsNullOrWhiteSpace(json)) return null;

        return JsonSerializer.Deserialize<UsuarioPublicoDto>(json);
    }

    public void CerrarSesion()
    {
        SecureStorage.Remove("USER");
        SecureStorage.Remove("TOKEN");
    }
}
