using System.Security.Claims;

namespace BibliotecaMovil.Server.Services.Security;

public static class ClaimsExtensions
{
    public static int GetUsuarioIdOrThrow(this ClaimsPrincipal user)
    {
        var raw = user.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(raw))
            throw new UnauthorizedAccessException("Token sin NameIdentifier.");

        return int.Parse(raw);
    }

    public static string GetRolOrThrow(this ClaimsPrincipal user)
    {
        var rol = user.FindFirstValue(ClaimTypes.Role);
        if (string.IsNullOrWhiteSpace(rol))
            throw new UnauthorizedAccessException("Token sin Role.");

        return rol;
    }
}
