using BibliotecaMovil.Server.Data;
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

    public async Task<UsuarioDto?> GetUsuarioByEmailAsync(string email)
    {
        var usuario = await _context.Usuarios
            .Include(u => u.Rol)
            .FirstOrDefaultAsync(u => u.Email == email);
        if (usuario == null) return null;

        return new UsuarioDto
        {
            IdUsuario = usuario.IdUsuario,
            IdRol = usuario.IdRol,
            NombreRol = usuario.Rol?.Nombre ?? string.Empty,
            Nombre = usuario.Nombre,
            Apellido = usuario.Apellido,
            Email = usuario.Email,
            PasswordHash = usuario.PasswordHash,
            FechaAlta = usuario.FechaAlta,
            Activo = usuario.Activo
        };
    }

    public async Task<bool> CreateUsuarioAsync(UsuarioDto usuarioDto)
    {
        var usuario = new Biblioteca.Models.Usuario
        {
            Nombre = usuarioDto.Nombre,
            Apellido = usuarioDto.Apellido,
            Email = usuarioDto.Email,
            PasswordHash = usuarioDto.PasswordHash,
            IdRol = usuarioDto.IdRol,
            FechaAlta = usuarioDto.FechaAlta,
            Activo = usuarioDto.Activo,
            ImgUrl = null
        };

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<UsuarioAuthDto?> GetUsuarioAuthByEmailAsync(string email)
    {
        var usuario = await _context.Usuarios
            .Include(u => u.Rol)
            .FirstOrDefaultAsync(u => u.Email == email);

        if (usuario is null) return null;

        return new UsuarioAuthDto
        {
            IdUsuario = usuario.IdUsuario,
            Email = usuario.Email,
            PasswordHash = usuario.PasswordHash,
            IdRol = usuario.IdRol,
            NombreRol = usuario.Rol?.Nombre ?? "",
            Nombre = usuario.Nombre,
            Apellido = usuario.Apellido
        };
    }

}
