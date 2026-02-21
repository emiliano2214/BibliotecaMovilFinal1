using BibliotecaMovil.Server.Repositories;
using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Shared.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BibliotecaMovil.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SancionController : ControllerBase
{
    private readonly ISancionRepository _sancionRepository;

    public SancionController(ISancionRepository sancionRepository)
    {
        _sancionRepository = sancionRepository;
    }

    // ✅ GET api/sancion/prestamo/5
    [HttpGet("prestamo/{prestamoId:int}")]
    public async Task<ActionResult<List<SancionDto>>> GetSancionesByPrestamo(int prestamoId)
    {
        var list = await _sancionRepository.GetSancionesByPrestamoIdAsync(prestamoId);
        return Ok(list);
    }

    // ✅ POST api/sancion/crear-por-tardanza/5
    [HttpPost("crear-por-tardanza/{prestamoId:int}")]
    public async Task<ActionResult<SancionDto?>> CrearSancionPorTardanza(int prestamoId)
    {
        var dto = await _sancionRepository.CrearSancionPorTardanzaAsync(prestamoId);

        // Si no corresponde (no devuelto / no tarde / ya existe), devolvemos 204
        if (dto is null) return NoContent();

        return Ok(dto);
    }

    // ✅ POST api/sancion/pagar/10
    [HttpPost("pagar/{idSancion:int}")]
    public async Task<ActionResult> Pagar(int idSancion)
    {
        var ok = await _sancionRepository.PagarSancionAsync(idSancion);
        return ok ? Ok(true) : NotFound(false);
    }

    // ✅ DELETE api/sancion/10
    [HttpDelete("{idSancion:int}")]
    public async Task<ActionResult> Eliminar(int idSancion)
    {
        var ok = await _sancionRepository.EliminarSancionAsync(idSancion);
        return ok ? Ok(true) : NotFound(false);
    }

    // ✅ GET api/sancion/usuario/3
    [HttpGet("usuario/{usuarioId:int}")]
    public async Task<ActionResult<List<SancionDto>>> GetByUsuario(int usuarioId)
    {
        var list = await _sancionRepository.GetSancionesByUsuarioIdAsync(usuarioId);
        return Ok(list);
    }

    // ✅ GET api/sancion/10
    [HttpGet("{idSancion:int}")]
    public async Task<ActionResult<SancionDto?>> GetById(int idSancion)
    {
        var dto = await _sancionRepository.GetSancionByIdAsync(idSancion);
        return dto is null ? NotFound() : Ok(dto);
    }

    // ✅ GET api/sancion/mis  (para tu vista general /sanciones)
    // Requiere JWT y que el token tenga el IdUsuario en una claim
    [Authorize]
    [HttpGet("mis")]
    public async Task<ActionResult<List<SancionDto>>> GetMisSanciones()
    {
        // Opción 1 (estándar): NameIdentifier
        var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);

        // Opción 2 (por si tu JWT usa otra claim)
        // var userIdStr = User.FindFirstValue("idUsuario") ?? User.FindFirstValue("id");

        if (!int.TryParse(userIdStr, out var userId))
            return Unauthorized("No se pudo leer el IdUsuario desde el token.");

        var list = await _sancionRepository.GetSancionesByUsuarioIdAsync(userId);
        return Ok(list);
    }
}