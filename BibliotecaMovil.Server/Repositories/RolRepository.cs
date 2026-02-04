using BibliotecaMovil.Server.Data;
using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaMovil.Server.Repositories;

public class RolRepository : IRolRepository
{
    private readonly BibliotecaDbContext _context;

    public RolRepository(BibliotecaDbContext context)
    {
        _context = context;
    }

    public async Task<List<RolDto>> GetAllRolesAsync()
    {
        return await _context.Roles
            .Select(r => new RolDto
            {
                IdRol = r.IdRol,
                NombreRol = r.NombreRol
            })
            .ToListAsync();
    }

    public async Task<RolDto?> GetRolByIdAsync(int id)
    {
        var rol = await _context.Roles.FindAsync(id);
        if (rol == null) return null;

        return new RolDto
        {
            IdRol = rol.IdRol,
            NombreRol = rol.NombreRol
        };
    }
}
