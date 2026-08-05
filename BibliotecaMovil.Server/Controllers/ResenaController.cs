using BibliotecaMovil.Server.Repositories;
using BibliotecaMovil.Server.Services.Security;
using BibliotecaMovil.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaMovil.Server.Controllers;

[Authorize(Roles = "Lector,Admin,Bibliotecario")]
[ApiController]
[Route("api/[controller]")]
public class ResenaController : ControllerBase
{
    private readonly IResenaRepository _resenaRepository;

    public ResenaController(IResenaRepository resenaRepository)
    {
        _resenaRepository = resenaRepository;
    }

    [HttpGet("libro/{libroId}")]
    public async Task<ActionResult<List<ResenaDto>>> GetResenasByLibro(int libroId)
    {
        var resenas = await _resenaRepository.GetResenasByLibroIdAsync(libroId);
        return Ok(resenas);
    }

    [HttpPost]
    public async Task<IActionResult> CreateResena([FromBody] ResenaDto resena)
    {
        // IdUsuario real desde el token
        resena.IdUsuario = User.GetUsuarioIdOrThrow();

        resena.FechaResena = DateTime.Now;

        var ok = await _resenaRepository.CreateResenaAsync(resena);
        return ok ? Ok() : BadRequest();
    }

    [HttpGet("libroId-por-titulo")]
    public async Task<ActionResult<int>> GetLibroIdPorTitulo([FromQuery] string titulo)
    {
        var id = await _resenaRepository.GetLibroIdByTituloAsync(titulo);
        if (id is null) return NotFound("No se encontró un libro con ese título.");
        return Ok(id.Value);
    }
}