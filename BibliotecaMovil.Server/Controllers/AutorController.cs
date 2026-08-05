using Microsoft.AspNetCore.Mvc;
using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Server.Repositories;
using Microsoft.AspNetCore.Authorization;


namespace BibliotecaMovil.Server.Controllers;

[Authorize(Roles = "Admin,Bibliotecario")]
[ApiController]
[Route("api/[controller]")]
public class AutorController : ControllerBase
{
    private readonly IAutorRepository _autorRepository;

    public AutorController(IAutorRepository autorRepository)
    {
        _autorRepository = autorRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<AutorDto>>> GetAutores()
    {
        return await _autorRepository.GetAllAutoresAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AutorDto>> GetAutor(int id)
    {
        var autor = await _autorRepository.GetAutorByIdAsync(id);
        if (autor == null) return NotFound();
        return autor;
    }

    [HttpPost]
    public async Task<ActionResult> AddAutor([FromBody] AutorCreateDto dto)
    {
        try
        {
            var ok = await _autorRepository.AddAutorAsync(dto);

            if (!ok)
                return BadRequest("No se pudo crear el autor.");

            return Ok();
        }
        catch (Exception ex)
        {
            // esto te muestra el error REAL (y el inner también)
            var msg = ex.InnerException?.Message is not null
                ? $"{ex.Message} | INNER: {ex.InnerException.Message}"
                : ex.Message;

            return StatusCode(500, msg);
        }
    }
}
