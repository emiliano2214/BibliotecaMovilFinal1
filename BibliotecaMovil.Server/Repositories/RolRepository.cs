using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Shared.Interfaces;

namespace BibliotecaMovil.Server.Repositories;

public class RolRepository : IRolRepository
{
    private readonly List<RolDto> _roles = new()
    {
        new RolDto { IdRol = 1, NombreRol = "Admin" },
        new RolDto { IdRol = 2, NombreRol = "Usuario" }
    };

    public Task<List<RolDto>> GetAllRolesAsync()
    {
        return Task.FromResult(_roles);
    }

    public Task<RolDto?> GetRolByIdAsync(int id)
    {
        return Task.FromResult(_roles.FirstOrDefault(r => r.IdRol == id));
    }
}
