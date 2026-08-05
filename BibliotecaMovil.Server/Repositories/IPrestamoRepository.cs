using BibliotecaMovil.Shared.DTOs;

namespace BibliotecaMovil.Server.Repositories;

public interface IPrestamoRepository
{
    Task<List<PrestamoDto>> GetAllPrestamosAsync();
    Task<List<PrestamoDto>> GetPrestamosByUsuarioIdAsync(int usuarioId);
    Task<bool> CreatePrestamoAsync(PrestamoDto prestamo);
    Task<bool> DevolverPrestamoAsync(int prestamoId);
}