using BibliotecaMovil.Server.Models;
using BibliotecaMovil.Server.Repositories;
using BibliotecaMovil.Server.Security;
using BibliotecaMovil.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaMovil.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IPasswordService _passwordService;

    public UsuarioController(
        IUsuarioRepository usuarioRepository,
        IJwtTokenService jwtTokenService,
        IPasswordService passwordService)
    {
        _usuarioRepository = usuarioRepository;
        _jwtTokenService = jwtTokenService;
        _passwordService = passwordService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto req)
    {
        var existing = await _usuarioRepository.GetUsuarioAuthByEmailAsync(req.Email);

        if (existing is null)
            return Unauthorized(new { message = "Credenciales inválidas" });

        var ok = _passwordService.Verify(req.Password, existing.PasswordHash);
        if (!ok)
            return Unauthorized(new { message = "Credenciales inválidas" });

        var token = _jwtTokenService.GenerateToken(existing);

        // ✅ DTO público para UI
        var userPublico = new UsuarioPublicoDto
        {
            IdUsuario = existing.IdUsuario,
            Email = existing.Email,
            IdRol = existing.IdRol,
            NombreRol = existing.NombreRol,
            Nombre = existing.Nombre,
            Apellido = existing.Apellido,
            Activo = true
        };

        return Ok(new LoginResponseDto
        {
            Token = token,
            ExpiresInMinutes = 60,
            User = userPublico
        });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto req)
    {
        var hash = _passwordService.Hash(req.Password);

        var interno = new UsuarioCreadoInterno
        {
            Nombre = req.Nombre,
            Apellido = req.Apellido,
            Email = req.Email,
            PasswordHash = hash,
            IdRol = req.RolId,
            ImgUrl = req.ImgUrl
        };

        await _usuarioRepository.CreateUsuarioAsync(interno);
        return Ok();
    }
}
