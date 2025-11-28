using BibliotecaMovil.Shared.DTOs;

namespace BibliotecaMovil.Shared.Interfaces;

public interface IRolService
{
    Task<List<RolDto>> GetRolesAsync();
    Task<RolDto?> GetRolByIdAsync(int id);
}
