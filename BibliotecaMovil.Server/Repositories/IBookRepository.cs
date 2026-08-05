using BibliotecaMovil.Shared.DTOs;

namespace BibliotecaMovil.Server.Repositories;

public interface ILibroRepository
{

    Task<List<LibroDto>> GetAllLibrosAsync();
    Task<LibroDto?> GetLibroByIdAsync(int id);
    Task<List<LibroDto>> GetLibrosByCategoriaAsync(int categoriaId);

    Task<int> AddLibroAsync(LibroCreateDto dto);

    Task<bool> UpdateLibroAsync(int idLibro, LibroUpdateDto dto);

    Task<bool> DeleteLibroAsync(int idLibro);
}