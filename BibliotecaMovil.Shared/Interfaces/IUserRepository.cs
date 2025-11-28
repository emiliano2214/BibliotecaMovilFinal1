using BibliotecaMovil.Shared.DTOs;

namespace BibliotecaMovil.Shared.Interfaces;

public interface IUsuarioRepository
{
    Task<UsuarioDto?> GetUsuarioByEmailAsync(string email);
    Task<bool> CreateUsuarioAsync(UsuarioDto usuario);
}
