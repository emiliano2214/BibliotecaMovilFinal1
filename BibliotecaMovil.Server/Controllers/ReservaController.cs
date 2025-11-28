using Microsoft.AspNetCore.Mvc;
using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Shared.Interfaces;

namespace BibliotecaMovil.Server.Controllers;

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
}
