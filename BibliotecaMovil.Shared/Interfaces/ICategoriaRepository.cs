using BibliotecaMovil.Shared.DTOs;

namespace BibliotecaMovil.Shared.Interfaces;

public interface ICategoriaRepository
{
    Task<List<CategoriaDto>> GetAllCategoriasAsync();
    Task<CategoriaDto?> GetCategoriaByIdAsync(int id);
}
