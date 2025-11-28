using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Shared.Interfaces;

namespace BibliotecaMovil.Server.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly List<UsuarioDto> _usuarios = new();

    public Task<UsuarioDto?> GetUsuarioByEmailAsync(string email)
    {
        var usuario = _usuarios.FirstOrDefault(u => u.Email == email);
        return Task.FromResult(usuario);
    }

    public Task<bool> CreateUsuarioAsync(UsuarioDto usuario)
    {
        usuario.IdUsuario = _usuarios.Count + 1;
        _usuarios.Add(usuario);
        return Task.FromResult(true);
    }
}
