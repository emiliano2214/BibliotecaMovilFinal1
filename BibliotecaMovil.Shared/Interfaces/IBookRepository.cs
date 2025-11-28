using BibliotecaMovil.Shared.DTOs;

namespace BibliotecaMovil.Shared.Interfaces;

public interface ILibroRepository
{
    Task<List<LibroDto>> GetAllLibrosAsync();
    Task<LibroDto?> GetLibroByIdAsync(int id);
    Task<List<LibroDto>> GetLibrosByCategoriaAsync(int categoriaId);
}
