using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Shared.Interfaces;

namespace BibliotecaMovil.Server.Repositories;

public class EjemplarRepository : IEjemplarRepository
{
    private readonly List<EjemplarDto> _ejemplares = new()
    {
        new EjemplarDto { IdEjemplar = 1, IdLibro = 1, CodigoInventario = "INV-001", Estado = "DISPONIBLE", Ubicacion = "Estante A1" },
        new EjemplarDto { IdEjemplar = 2, IdLibro = 2, CodigoInventario = "INV-002", Estado = "PRESTADO", Ubicacion = "Estante B2" }
    };

    public Task<List<EjemplarDto>> GetEjemplaresByLibroIdAsync(int libroId)
    {
        return Task.FromResult(_ejemplares.Where(e => e.IdLibro == libroId).ToList());
    }

    public Task<EjemplarDto?> GetEjemplarByIdAsync(int id)
    {
        return Task.FromResult(_ejemplares.FirstOrDefault(e => e.IdEjemplar == id));
    }
}
