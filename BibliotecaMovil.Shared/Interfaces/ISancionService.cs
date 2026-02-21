using BibliotecaMovil.Shared.DTOs;

namespace BibliotecaMovil.Shared.Interfaces;

public interface ISancionService
{
    Task<List<SancionDto>> GetSancionesByPrestamoIdAsync(int prestamoId);
    Task<SancionDto?> CrearSancionPorTardanzaAsync(int prestamoId);
    Task<bool> PagarSancionAsync(int idSancion);
    Task<bool> EliminarSancionAsync(int idSancion);
    Task<List<SancionDto>> GetSancionesByUsuarioIdAsync(int usuarioId);
    Task<SancionDto?> GetSancionByIdAsync(int idSancion);
    Task<List<SancionDto>> GetMisSancionesAsync();

}
