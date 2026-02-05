using Biblioteca.Models;
using BibliotecaMovil.Server.Security;
using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace BibliotecaMovil.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IPasswordService _passwordService;

    public UsuarioController(IUsuarioRepository usuarioRepository, IJwtTokenService jwtTokenService, IPasswordService passwordService)
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

        var token = _jwtTokenService.GenerateToken(existing); // tu token usa rol/nombre/etc

        return Ok(new LoginResponseDto
        {
            Token = token,
            ExpiresInMinutes = 60,
            User = new UsuarioDto
            {
                IdUsuario = existing.IdUsuario,
                Email = existing.Email,
                IdRol = existing.IdRol,
                NombreRol = existing.NombreRol,
                Nombre = existing.Nombre,
                Apellido = existing.Apellido,
                Activo = true
            }
        });
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto req)
    {
        var existingUsuario = await _usuarioRepository.GetUsuarioByEmailAsync(req.Email);
        if (existingUsuario != null)
            return BadRequest("Usuario already exists");

        var usuarioDto = new UsuarioDto
        {
            Nombre = req.Nombre,
            Apellido = req.Apellido,
            Email = req.Email,
            IdRol = req.RolId,
            PasswordHash = _passwordService.Hash(req.Password), // ✅ ACÁ se hashea
            FechaAlta = DateTime.Now,
            Activo = true
        };

        var result = await _usuarioRepository.CreateUsuarioAsync(usuarioDto);
        return result ? Ok() : BadRequest("Could not create usuario");
    }

}
