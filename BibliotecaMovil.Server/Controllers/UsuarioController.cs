using BibliotecaMovil.Server.Models;
using BibliotecaMovil.Server.Repositories;
using BibliotecaMovil.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BibliotecaMovil.Server.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")] // => api/Usuario
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioRepository _repo;

    public UsuarioController(IUsuarioRepository repo)
    {
        _repo = repo;
    }

    // GET api/Usuario/5/detalle
    [HttpGet("{id:int}/detalle")]
    public async Task<ActionResult<UsuarioDetalleDto>> GetDetalle(int id)
    {
        try
        {
            var dto = await _repo.GetDetalleAsync(id);
            if (dto is null) return NotFound();
            return Ok(dto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.ToString()); // stacktrace
        }
    }

    // GET api/Usuario
    [Authorize(Roles = "Admin,Bibliotecario")]
    [HttpGet]
    public async Task<ActionResult<List<UsuarioPublicoDto>>> GetAll()
        => Ok(await _repo.GetAllAsync());

    // GET api/Usuario/5
    [Authorize(Roles = "Admin,Bibliotecario")]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<UsuarioPublicoDto>> GetById(int id)
    {
        var u = await _repo.GetByIdAsync(id);
        return u is null ? NotFound() : Ok(u);
    }

    // PUT api/Usuario/5 ESTE ES EL QUE NECESITA /cuenta
    [Authorize(Roles = "Admin,Bibliotecario")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UsuarioActualizadoDto dto)
    {
        if (id != dto.IdUsuario) return BadRequest("Id mismatch");

        // Id del usuario logueado desde el token
        var claimId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                   ?? User.FindFirst("sub")?.Value
                   ?? User.FindFirst("IdUsuario")?.Value;

        if (!int.TryParse(claimId, out var userId))
            return Unauthorized("Token sin IdUsuario");

        var esAdmin = User.IsInRole("Admin");

        // Si NO es admin, solo puede editarse a sí mismo
        if (!esAdmin && userId != id)
            return Forbid();

        // (opcional pero recomendado) evitar que cambie Rol/Activo desde la cuenta
        dto.IdRol = dto.IdRol; // mejor: ignorarlo en repo y mantener el de DB
                               // dto.Activo idem

        var ok = await _repo.UpdateAsync(dto);
        return ok ? NoContent() : NotFound();
    }

    // DELETE api/Usuario/5
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _repo.DeleteAsync(id);
        return ok ? NoContent() : NotFound();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("{id:int}")] 
    public async Task<IActionResult> Create(UsuarioCreadoInterno dto)
    {
        var ok = await _repo.CreateUsuarioAsync(dto);
        return ok ? NoContent() : NotFound();
    }
}
