using BibliotecaMovil.Shared.DTOs;

namespace BibliotecaMovil.Shared.Interfaces;

public interface IReservaService
{
    Task<List<ReservaDto>> GetReservasByUsuarioIdAsync(int usuarioId);
    Task<bool> CreateReservaAsync(ReservaDto reserva);
}
