using BibliotecaMovil.Shared.DTOs;

namespace BibliotecaMovil.Shared.Interfaces;

public interface IRolRepository
{
    Task<List<RolDto>> GetAllRolesAsync();
    Task<RolDto?> GetRolByIdAsync(int id);
}
