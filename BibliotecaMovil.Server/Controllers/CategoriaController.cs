using Microsoft.AspNetCore.Mvc;
using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Server.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace BibliotecaMovil.Server.Controllers;

[Authorize(Roles = "Admin,Bibliotecario")]
[ApiController]
[Route("api/[controller]")]
public class CategoriaController : ControllerBase
{
    private readonly ICategoriaRepository _categoriaRepository;

    public CategoriaController(ICategoriaRepository categoriaRepository)
    {
        _categoriaRepository = categoriaRepository;
    }

    [Authorize(Roles = "Admin, Bibliotecario, Lector")]
    [HttpGet]
    public async Task<ActionResult<List<CategoriaDto>>> GetCategorias()
    {
        return await _categoriaRepository.GetAllCategoriasAsync();
    }

    [Authorize(Roles = "Admin, Bibliotecario, Lector")]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CategoriaDto>> GetCategoria(int id)
    {
        var categoria = await _categoriaRepository.GetCategoriaByIdAsync(id);
        if (categoria == null) return NotFound();
        return Ok(categoria);
    }

    // CREATE
    [HttpPost]
    public async Task<ActionResult> AddCategoria([FromBody] CategoriaCreateDto dto)
    {
        try
        {
            var id = await _categoriaRepository.AddCategoriaAsync(dto);
            return CreatedAtAction(nameof(GetCategoria), new { id }, new { idCategoria = id });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // UPDATE
    [HttpPut("{idCategoria:int}")]
    public async Task<ActionResult> UpdateCategoria(int idCategoria, [FromBody] CategoriaUpdateDto dto)
    {
        try
        {
            var ok = await _categoriaRepository.UpdateCategoriaAsync(idCategoria, dto);
            if (!ok) return NotFound();
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE
    [HttpDelete("{idCategoria:int}")]
    public async Task<ActionResult> DeleteCategoria(int idCategoria)
    {
        try
        {
            var ok = await _categoriaRepository.DeleteCategoriaAsync(idCategoria);
            if (!ok) return NotFound();
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}