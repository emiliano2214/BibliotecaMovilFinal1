using BibliotecaMovil.Shared.DTOs;

namespace BibliotecaMovil.Shared.Interfaces;

public interface IPrestamoService
{
    Task<List<PrestamoDto>> GetAllPrestamosAsync();
    Task<List<PrestamoDto>> GetPrestamosByUsuarioIdAsync(int usuarioId);
    Task<bool> CreatePrestamoAsync(PrestamoDto prestamo);
    Task<bool> RegistrarDevolucionAsync(int prestamoId);
}