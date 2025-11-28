using BibliotecaMovil.Shared.DTOs;

namespace BibliotecaMovil.Shared.Interfaces;

public interface ILibroService
{
    Task<List<LibroDto>> GetLibrosAsync();
    Task<LibroDto?> GetLibroByIdAsync(int id);
    Task<List<LibroDto>> GetLibrosByCategoriaAsync(int categoriaId);
}
