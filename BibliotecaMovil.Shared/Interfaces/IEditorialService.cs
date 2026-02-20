using BibliotecaMovil.Shared.DTOs;

namespace BibliotecaMovil.Shared.Interfaces;

public interface IEditorialService
{
    Task<List<EditorialDto>> GetEditorialesAsync();
    Task<EditorialDto?> GetEditorialByIdAsync(int id);
    Task<EditorialDto> CreateEditorialAsync(EditorialDto dto);
    Task UpdateEditorialAsync(int id, EditorialDto dto);
}
