using BibliotecaMovil.Shared.DTOs;

namespace BibliotecaMovil.Server.Repositories;

public interface IEjemplarRepository
{
    Task<List<EjemplarDto>> GetEjemplaresByLibroIdAsync(int libroId);
    Task<EjemplarDto?> GetEjemplarByIdAsync(int id);
}
