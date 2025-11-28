using BibliotecaMovil.Shared.DTOs;

namespace BibliotecaMovil.Shared.Interfaces;

public interface ISancionService
{
    Task<List<SancionDto>> GetSancionesByPrestamoIdAsync(int prestamoId);
}
