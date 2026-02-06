using BibliotecaMovil.Shared.DTOs;

namespace BibliotecaMovil.Server.Repositories;

public interface ICategoriaRepository
{
    Task<List<CategoriaDto>> GetAllCategoriasAsync();
    Task<CategoriaDto?> GetCategoriaByIdAsync(int id);
}
