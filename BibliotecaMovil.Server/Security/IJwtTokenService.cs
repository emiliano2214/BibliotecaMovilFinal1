using BibliotecaMovil.Server.Models;
using BibliotecaMovil.Shared.DTOs;

namespace BibliotecaMovil.Server.Security;

public interface IJwtTokenService
{
    /// <summary>
    /// Genera un JWT firmado para el usuario autenticado.
    /// Debe incluir al menos: IdUsuario, Email y Role (NombreRol).
    /// </summary>
    string GenerateToken(UsuarioAuthData usuario);
}
