using BibliotecaMovil.Shared.DTOs;

namespace BibliotecaMovil.Shared.Interfaces;

public interface IEditorialRepository
{
    Task<List<EditorialDto>> GetAllEditorialesAsync();
    Task<EditorialDto?> GetEditorialByIdAsync(int id);
}
