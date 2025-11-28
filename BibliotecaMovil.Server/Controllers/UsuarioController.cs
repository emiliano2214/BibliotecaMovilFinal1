using Microsoft.AspNetCore.Mvc;
using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Shared.Interfaces;

namespace BibliotecaMovil.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioController(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UsuarioDto usuario)
    {
        var existingUsuario = await _usuarioRepository.GetUsuarioByEmailAsync(usuario.Email);
        if (existingUsuario == null || existingUsuario.PasswordHash != usuario.PasswordHash)
        {
            return Unauthorized();
        }
        return Ok();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UsuarioDto usuario)
    {
        var existingUsuario = await _usuarioRepository.GetUsuarioByEmailAsync(usuario.Email);
        if (existingUsuario != null)
        {
            return BadRequest("Usuario already exists");
        }

        var result = await _usuarioRepository.CreateUsuarioAsync(usuario);
        if (result) return Ok();
        return BadRequest("Could not create usuario");
    }
}
