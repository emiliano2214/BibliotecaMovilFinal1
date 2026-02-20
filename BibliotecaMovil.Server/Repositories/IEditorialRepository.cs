using BibliotecaMovil.Shared.DTOs;

namespace BibliotecaMovil.Server.Repositories;

public interface IEditorialRepository
{
    Task<List<EditorialDto>> GetAllEditorialesAsync();
    Task<EditorialDto?> GetEditorialByIdAsync(int id);
    Task<EditorialDetalleDto?> GetDetalleEditorialAsync(int id);

    Task<EditorialDto> CreateEditorialAsync(EditorialDto dto);
    Task<bool> UpdateEditorialAsync(int id, EditorialDto dto);
}
