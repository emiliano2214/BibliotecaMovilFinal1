using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Server.Repositories;
using BibliotecaMovil.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaMovil.Server.Controllers;

[ApiController]
[Route("api/[controller]")] // => api/Usuario
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioRepository _repo;

    public UsuarioController(IUsuarioRepository repo)
    {
        _repo = repo;
    }

    // GET api/Usuario
    [HttpGet]
    public async Task<ActionResult<List<UsuarioPublicoDto>>> GetAll()
        => Ok(await _repo.GetAllAsync());

    // GET api/Usuario/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<UsuarioPublicoDto>> GetById(int id)
    {
        var u = await _repo.GetByIdAsync(id);
        return u is null ? NotFound() : Ok(u);
    }

    // PUT api/Usuario/5 ESTE ES EL QUE NECESITA /cuenta
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UsuarioActualizadoDto dto)
    {
        if (id != dto.IdUsuario) return BadRequest("Id mismatch");

        var ok = await _repo.UpdateAsync(dto);
        return ok ? NoContent() : NotFound();
    }

    // DELETE api/Usuario/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _repo.DeleteAsync(id);
        return ok ? NoContent() : NotFound();
    }

    [HttpPost("{id:int}")] 
    public async Task<IActionResult> Create(UsuarioCreadoInterno dto)
    {
        var ok = await _repo.CreateUsuarioAsync(dto);
        return ok ? NoContent() : NotFound();
    }
}
