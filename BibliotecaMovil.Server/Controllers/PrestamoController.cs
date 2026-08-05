using BibliotecaMovil.Server.Repositories;
using BibliotecaMovil.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet]
    [Authorize(Roles = "Bibliotecario,Admin")]
    public async Task<ActionResult<List<PrestamoDto>>> GetAll()
    {
        var data = await _repo.GetAllPrestamosAsync();
        return Ok(data);
    }

    [HttpGet("usuario/{usuarioId:int}")]
    [Authorize(Roles = "Lector,Bibliotecario,Admin")]
    public async Task<ActionResult<List<PrestamoDto>>> GetByUsuario(int usuarioId)
    {
        var data = await _repo.GetPrestamosByUsuarioIdAsync(usuarioId);
        return Ok(data);
    }

    [HttpPost]
    [Authorize(Roles = "Bibliotecario,Admin")]
    public async Task<ActionResult> Create([FromBody] PrestamoDto dto)
    {
        var ok = await _repo.CreatePrestamoAsync(dto);
        return ok ? Ok() : BadRequest();
    }

    [HttpPut("{prestamoId:int}/devolver")]
    [Authorize(Roles = "Bibliotecario,Admin")]
    public async Task<ActionResult> Devolver(int prestamoId)
    {
        var ok = await _repo.DevolverPrestamoAsync(prestamoId);
        return ok ? NoContent() : BadRequest("No se pudo registrar la devolución.");
    }
}