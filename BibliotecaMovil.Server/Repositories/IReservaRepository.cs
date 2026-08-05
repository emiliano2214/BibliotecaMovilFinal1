using BibliotecaMovil.Shared.DTOs;

namespace BibliotecaMovil.Server.Repositories;

public interface IReservaRepository
{
    /// <summary>Reservas del usuario autenticado.</summary>
    Task<List<ReservaDto>> GetReservasByUsuarioIdAsync(int usuarioId);

    /// <summary>Todas las reservas (solo Admin).</summary>
    Task<List<ReservaDto>> GetAllReservasAsync();

    /// <summary>
    /// Crea una reserva en cola para el libro.
    /// Devuelve null si el usuario ya tiene una reserva activa para ese libro.
    /// </summary>
    Task<ReservaDto?> CreateReservaAsync(int usuarioId, int idLibro);

    /// <summary>
    /// Cancela una reserva.
    /// Si estaba DISPONIBLE o PENDIENTE, activa la siguiente en cola.
    /// </summary>
    Task<bool> CancelarReservaAsync(int reservaId, int usuarioId, bool esAdmin = false);

    /// <summary>
    /// Llamado por PrestamoRepository al devolver un ejemplar:
    /// activa la primera reserva PENDIENTE del libro, si existe.
    /// </summary>
    Task ActivarSiguienteReservaAsync(int idLibro);
}
