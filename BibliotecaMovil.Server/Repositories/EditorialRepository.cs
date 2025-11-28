using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Shared.Interfaces;

namespace BibliotecaMovil.Server.Repositories;

public class EditorialRepository : IEditorialRepository
{
    private readonly List<EditorialDto> _editoriales = new()
    {
        new EditorialDto { IdEditorial = 1, Nombre = "Penguin Random House", Pais = "USA" },
        new EditorialDto { IdEditorial = 2, Nombre = "Planeta", Pais = "España" }
    };

    public Task<List<EditorialDto>> GetAllEditorialesAsync()
    {
        return Task.FromResult(_editoriales);
    }

    public Task<EditorialDto?> GetEditorialByIdAsync(int id)
    {
        return Task.FromResult(_editoriales.FirstOrDefault(e => e.IdEditorial == id));
    }
}
