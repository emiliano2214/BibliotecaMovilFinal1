using BibliotecaMovil.Shared.DTOs;

namespace BibliotecaMovil.Shared.Interfaces;

public interface ICategoriaService
{
    Task<List<CategoriaDto>> GetCategoriasAsync();
    Task<CategoriaDto?> GetCategoriaByIdAsync(int id);
}
