using BibliotecaMovil.Server.Repositories;
using BibliotecaMovil.Server.Services.Security;
using BibliotecaMovil.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaMovil.Server.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ReservaController : ControllerBase
{
    private readonly IReservaRepository _reservaRepository;

    public ReservaController(IReservaRepository reservaRepository)
    {
        _reservaRepository = reservaRepository;
    }

    // GET api/reserva/usuario/{usuarioId}  — el propio usuario o Admin/Bibliotecario
    [HttpGet("usuario/{usuarioId:int}")]
    [Authorize(Roles = "Lector,Admin,Bibliotecario")]
    public async Task<ActionResult<List<ReservaDto>>> GetReservasByUsuario(int usuarioId)
    {
        var lista = await _reservaRepository.GetReservasByUsuarioIdAsync(usuarioId);
        return Ok(lista);
    }

    // GET api/reserva  — solo Admin
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<List<ReservaDto>>> GetAll()
    {
        var lista = await _reservaRepository.GetAllReservasAsync();
        return Ok(lista);
    }

    // POST api/reserva  — cualquier usuario autenticado puede reservar
    [HttpPost]
    public async Task<ActionResult<ReservaDto>> CreateReserva([FromBody] ReservaCreateDto dto)
    {
        var usuarioId = User.GetUsuarioIdOrThrow();
        var resultado = await _reservaRepository.CreateReservaAsync(usuarioId, dto.IdLibro);

        if (resultado is null)
            return BadRequest("Ya tenés una reserva activa para este libro.");

        return Ok(resultado);
    }

    // DELETE api/reserva/{reservaId}  — el dueño cancela su reserva
    [HttpDelete("{reservaId:int}")]
    public async Task<IActionResult> Cancelar(int reservaId)
    {
        var usuarioId = User.GetUsuarioIdOrThrow();
        var ok = await _reservaRepository.CancelarReservaAsync(reservaId, usuarioId, esAdmin: false);
        return ok ? NoContent() : BadRequest("No se pudo cancelar la reserva.");
    }

    // DELETE api/reserva/{reservaId}/admin  — Admin cancela cualquier reserva
    [HttpDelete("{reservaId:int}/admin")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CancelarAdmin(int reservaId)
    {
        var ok = await _reservaRepository.CancelarReservaAsync(reservaId, 0, esAdmin: true);
        return ok ? NoContent() : BadRequest("No se pudo cancelar la reserva.");
    }
}
