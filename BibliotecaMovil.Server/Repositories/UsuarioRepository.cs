using BibliotecaMovil.Server.Data;
using BibliotecaMovil.Server.Models;
using BibliotecaMovil.Shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaMovil.Server.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly BibliotecaDbContext _context;

    public UsuarioRepository(BibliotecaDbContext context)
    {
        _context = context;
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

    // ✅ Crea usuario (recibe HASH ya generado)
    public async Task<bool> CreateUsuarioAsync(UsuarioCreadoInterno dto)
    {
        var usuario = new Biblioteca.Models.Usuario
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

    // ✅ Para Login (necesita hash para verificar)
    public async Task<UsuarioAuthData?> GetUsuarioAuthByEmailAsync(string email)
    {
        var usuario = await _context.Usuarios
            .Include(u => u.Rol)
            .FirstOrDefaultAsync(u => u.Email == email);

        if (usuario is null) return null;

        return new UsuarioAuthData
        {
            IdUsuario = usuario.IdUsuario,
            Email = usuario.Email,
            PasswordHash = usuario.PasswordHash,
            IdRol = usuario.IdRol,
            NombreRol = usuario.Rol?.Nombre ?? string.Empty,
            Nombre = usuario.Nombre,
            Apellido = usuario.Apellido
        };
    }
}
