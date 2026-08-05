using BibliotecaMovil.Shared.DTOs;

namespace BibliotecaMovil.Shared.Interfaces;

public interface ICategoriaService
{
    Task<List<CategoriaDto>> GetCategoriasAsync();
    Task<CategoriaDto?> GetCategoriaByIdAsync(int id);
    Task<bool> AddCategoriaAsync(CategoriaCreateDto dto);
    Task<bool> UpdateCategoriaAsync(int idCategoria, CategoriaUpdateDto dto);
    Task<bool> DeleteCategoriaAsync(int idCategoria);
}