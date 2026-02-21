using BibliotecaMovil.Shared.DTOs;

namespace BibliotecaMovil.Server.Repositories;

public interface ISancionRepository
{
    Task<List<SancionDto>> GetSancionesByPrestamoIdAsync(int prestamoId);
    Task<List<SancionDto>> GetSancionesByUsuarioIdAsync(int usuarioId);
    Task<SancionDto?> GetSancionByIdAsync(int idSancion);

    Task<SancionDto?> CrearSancionPorTardanzaAsync(int prestamoId);
    Task<bool> PagarSancionAsync(int idSancion);
    Task<bool> EliminarSancionAsync(int idSancion);
}