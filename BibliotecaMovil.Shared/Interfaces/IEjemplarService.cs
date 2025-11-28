using BibliotecaMovil.Shared.DTOs;

namespace BibliotecaMovil.Shared.Interfaces;

public interface IEjemplarService
{
    Task<List<EjemplarDto>> GetEjemplaresByLibroIdAsync(int libroId);
    Task<EjemplarDto?> GetEjemplarByIdAsync(int id);
}
