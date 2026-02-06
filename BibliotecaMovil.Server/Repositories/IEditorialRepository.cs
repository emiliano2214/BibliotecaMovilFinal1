using BibliotecaMovil.Shared.DTOs;

namespace BibliotecaMovil.Server.Repositories;

public interface IEditorialRepository
{
    Task<List<EditorialDto>> GetAllEditorialesAsync();
    Task<EditorialDto?> GetEditorialByIdAsync(int id);
}
