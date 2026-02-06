using BibliotecaMovil.Shared.DTOs;

namespace BibliotecaMovil.Shared.Interfaces;

public interface IUsuarioRepository
{
    Task<UsuarioPublicoDto?> GetUsuarioByEmailAsync(string email);
    Task<bool> CreateUsuarioAsync(RegisterRequestDto usuario);
    Task<UsuarioAuthDto?> GetUsuarioAuthByEmailAsync(string email);
}
    