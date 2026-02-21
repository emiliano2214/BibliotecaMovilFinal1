using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Server.Models;

namespace BibliotecaMovil.Server.Repositories;

public interface IUsuarioRepository
{
    Task<UsuarioPublicoDto?> GetUsuarioByEmailAsync(string email);

    // creación ya hasheada
    Task<List<UsuarioPublicoDto>> GetAllAsync();
    Task<UsuarioPublicoDto?> GetByIdAsync(int id);
    Task<bool> CreateUsuarioAsync(UsuarioCreadoInterno dto);
    Task<bool> UpdateAsync(UsuarioActualizadoDto dto);
    Task<bool> DeleteAsync(int id);
    Task<UsuarioDetalleDto?> GetDetalleAsync(int idUsuario);
}
