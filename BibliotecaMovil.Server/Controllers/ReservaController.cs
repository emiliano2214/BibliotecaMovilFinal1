using BibliotecaMovil.Server.Services.Security;
using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Server.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaMovil.Server.Controllers;

[Authorize(Roles = "Lector")]
[ApiController]
[Route("api/[controller]")]
public class ReservaController : ControllerBase
{
    private readonly IReservaRepository _reservaRepository;

    public ReservaController(IReservaRepository reservaRepository)
    {
        _reservaRepository = reservaRepository;
    }

    [HttpGet("usuario/{usuarioId}")]
    public async Task<ActionResult<List<ReservaDto>>> GetReservasByUsuario(int usuarioId)
    {
        return await _reservaRepository.GetReservasByUsuarioIdAsync(usuarioId);
    }

    [HttpPost]
    public async Task<IActionResult> CreateReserva([FromBody] ReservaDto reserva)
    {
        var result = await _reservaRepository.CreateReservaAsync(reserva);
        if (result) return Ok();
        return BadRequest();
    }

    [Authorize(Roles = "Lector")]
    [HttpDelete("{reservaId:int}")]
    public async Task<IActionResult> Cancelar(int reservaId)
    {
        var userId = User.GetUsuarioIdOrThrow();

        // repo debe validar que la reserva sea del userId y esté activa
        var ok = await _reservaRepository.CancelarReservaAsync(reservaId, userId);
        return ok ? NoContent() : BadRequest();
    }
}
