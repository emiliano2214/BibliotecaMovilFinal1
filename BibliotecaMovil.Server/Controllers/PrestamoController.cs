using Microsoft.AspNetCore.Mvc;
using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Server.Repositories;

namespace BibliotecaMovil.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrestamoController : ControllerBase
{
    private readonly IPrestamoRepository _repo;

    public PrestamoController(IPrestamoRepository repo)
    {
        _repo = repo;
    }

    [HttpGet("usuario/{usuarioId:int}")]
    public async Task<ActionResult<List<PrestamoDto>>> GetByUsuario(int usuarioId)
    {
        var data = await _repo.GetPrestamosByUsuarioIdAsync(usuarioId);
        return Ok(data);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] PrestamoDto dto)
    {
        var ok = await _repo.CreatePrestamoAsync(dto);
        return ok ? Ok() : BadRequest();
    }
}
