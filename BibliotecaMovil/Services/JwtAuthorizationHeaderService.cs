using System.Net.Http.Headers;

namespace BibliotecaMovil.Services;

public sealed class JwtAuthorizationHeaderService : DelegatingHandler
{
    private readonly UsuarioSesionService _sesion;

    public JwtAuthorizationHeaderService(UsuarioSesionService sesion)
    {
        _sesion = sesion;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var token = await _sesion.ObtenerTokenAsync();

        if (!string.IsNullOrWhiteSpace(token))
        {
            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
