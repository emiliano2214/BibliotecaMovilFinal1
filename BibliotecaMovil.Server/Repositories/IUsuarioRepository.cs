using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Server.Models;

namespace BibliotecaMovil.Server.Repositories;

public interface IUsuarioRepository
{
    Task<UsuarioPublicoDto?> GetUsuarioByEmailAsync(string email);

    // interno server (hash)
    Task<UsuarioAuthData?> GetUsuarioAuthByEmailAsync(string email);

    // creación ya hasheada
    Task<bool> CreateUsuarioAsync(UsuarioCreadoInterno dto);
}
