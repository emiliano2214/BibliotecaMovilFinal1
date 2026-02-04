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
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        if (usuario == null) return null;

        return new UsuarioDto
        {
            IdUsuario = usuario.IdUsuario,
            IdRol = usuario.RolId,
            Nombre = usuario.NombreUsuario,
            Apellido = null,
            Email = usuario.Email,
            PasswordHash = usuario.HashPassword,
            FechaAlta = usuario.FechaAlta,
            Activo = usuario.Activo
        };
    }

    public async Task<bool> CreateUsuarioAsync(UsuarioDto usuarioDto)
    {
        var usuario = new Biblioteca.Models.Usuario
        {
            NombreUsuario = usuarioDto.Nombre + (string.IsNullOrEmpty(usuarioDto.Apellido) ? "" : " " + usuarioDto.Apellido),
            Email = usuarioDto.Email,
            HashPassword = usuarioDto.PasswordHash,
            RolId = usuarioDto.IdRol,
            FechaAlta = usuarioDto.FechaAlta,
            Activo = usuarioDto.Activo,
            ImgUrl = null
        };

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
        return true;
    }
}
