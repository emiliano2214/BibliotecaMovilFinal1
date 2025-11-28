using Microsoft.AspNetCore.Mvc;
using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Shared.Interfaces;

namespace BibliotecaMovil.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrestamoController : ControllerBase
{
    private readonly IPrestamoRepository _prestamoRepository;

    public PrestamoController(IPrestamoRepository prestamoRepository)
    {
        _prestamoRepository = prestamoRepository;
    }

    [HttpGet("usuario/{usuarioId}")]
    public async Task<ActionResult<List<PrestamoDto>>> GetPrestamosByUsuario(int usuarioId)
    {
        return await _prestamoRepository.GetPrestamosByUsuarioIdAsync(usuarioId);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePrestamo([FromBody] PrestamoDto prestamo)
    {
        var result = await _prestamoRepository.CreatePrestamoAsync(prestamo);
        if (result) return Ok();
        return BadRequest();
    }
}
