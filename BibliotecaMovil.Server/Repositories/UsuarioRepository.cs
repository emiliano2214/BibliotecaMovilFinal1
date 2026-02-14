using BibliotecaMovil.Server.Data;
using BibliotecaMovil.Server.Models;
using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaMovil.Server.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly BibliotecaDbContext _context;

    public UsuarioRepository(BibliotecaDbContext context)
    {
        _context = context;
    }

    // ✅ GET ALL
    public async Task<List<UsuarioPublicoDto>> GetAllAsync()
    {
        return await _context.Usuarios
            .Include(u => u.Rol)
            .Select(u => new UsuarioPublicoDto
            {
                IdUsuario = u.IdUsuario,
                IdRol = u.IdRol,
                NombreRol = u.Rol != null ? u.Rol.Nombre : string.Empty,
                Nombre = u.Nombre,
                Apellido = u.Apellido,
                Email = u.Email,
                ImgUrl = u.ImgUrl,
                FechaAlta = u.FechaAlta,
                Activo = u.Activo
            })
            .ToListAsync();
    }

    // ✅ GET BY ID
    public async Task<UsuarioPublicoDto?> GetByIdAsync(int id)
    {
        var u = await _context.Usuarios
            .Include(x => x.Rol)
            .FirstOrDefaultAsync(x => x.IdUsuario == id);

        if (u is null) return null;

        return new UsuarioPublicoDto
        {
            IdUsuario = u.IdUsuario,
            IdRol = u.IdRol,
            NombreRol = u.Rol?.Nombre ?? string.Empty,
            Nombre = u.Nombre,
            Apellido = u.Apellido,
            Email = u.Email,
            ImgUrl = u.ImgUrl,
            FechaAlta = u.FechaAlta,
            Activo = u.Activo
        };
    }

    // ✅ UPDATE (para /cuenta)
    public async Task<bool> UpdateAsync(UsuarioActualizadoDto dto)
    {
        var u = await _context.Usuarios.FirstOrDefaultAsync(x => x.IdUsuario == dto.IdUsuario);
        if (u is null) return false;

        u.Nombre = dto.Nombre;
        u.Apellido = dto.Apellido;
        u.ImgUrl = dto.ImgUrl;
        u.Activo = dto.Activo;

        // 🔒 Recomendado: NO cambiar rol desde /cuenta
        // u.IdRol = dto.IdRol;

        await _context.SaveChangesAsync();
        return true;
    }

    // ✅ DELETE
    public async Task<bool> DeleteAsync(int id)
    {
        var u = await _context.Usuarios.FirstOrDefaultAsync(x => x.IdUsuario == id);
        if (u is null) return false;

        _context.Usuarios.Remove(u);
        await _context.SaveChangesAsync();
        return true;
    }

    // ✅ Para UI (NO incluye PasswordHash)
    public async Task<UsuarioPublicoDto?> GetUsuarioByEmailAsync(string email)
    {
        var usuario = await _context.Usuarios
            .Include(u => u.Rol)
            .FirstOrDefaultAsync(u => u.Email == email);

        if (usuario is null) return null;

        return new UsuarioPublicoDto
        {
            IdUsuario = usuario.IdUsuario,
            IdRol = usuario.IdRol,
            NombreRol = usuario.Rol?.Nombre ?? string.Empty,
            Nombre = usuario.Nombre,
            Apellido = usuario.Apellido,
            Email = usuario.Email,
            FechaAlta = usuario.FechaAlta,
            Activo = usuario.Activo,
            ImgUrl = usuario.ImgUrl
        };
    }

    // ✅ Crea usuario (recibe HASH ya generado) - usado en register
    public async Task<bool> CreateUsuarioAsync(UsuarioCreadoInterno dto)
    {
        var usuario = new BibliotecaMovil.Server.Models.Usuario
        {
            Nombre = dto.Nombre,
            Apellido = dto.Apellido,
            Email = dto.Email,
            PasswordHash = dto.PasswordHash,
            IdRol = dto.IdRol,
            FechaAlta = DateTime.UtcNow,
            Activo = true,
            ImgUrl = dto.ImgUrl
        };

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
        return true;
    }
}
