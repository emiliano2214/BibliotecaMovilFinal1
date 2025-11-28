using BibliotecaMovil.Shared.DTOs;

namespace BibliotecaMovil.Shared.Interfaces;

public interface IEjemplarRepository
{
    Task<List<EjemplarDto>> GetEjemplaresByLibroIdAsync(int libroId);
    Task<EjemplarDto?> GetEjemplarByIdAsync(int id);
}
