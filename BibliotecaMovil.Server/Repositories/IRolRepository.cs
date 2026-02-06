using BibliotecaMovil.Shared.DTOs;

namespace BibliotecaMovil.Server.Repositories;

public interface IRolRepository
{
    Task<List<RolDto>> GetAllRolesAsync();
    Task<RolDto?> GetRolByIdAsync(int id);
}
