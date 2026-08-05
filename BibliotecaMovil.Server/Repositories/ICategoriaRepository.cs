using BibliotecaMovil.Shared.DTOs;

namespace BibliotecaMovil.Server.Repositories;

public interface ICategoriaRepository
{
    Task<List<CategoriaDto>> GetAllCategoriasAsync();
    Task<CategoriaDto?> GetCategoriaByIdAsync(int id);
    Task<int> AddCategoriaAsync(CategoriaCreateDto dto);
    Task<bool> UpdateCategoriaAsync(int idCategoria, CategoriaUpdateDto dto);
    Task<bool> DeleteCategoriaAsync(int idCategoria);
}