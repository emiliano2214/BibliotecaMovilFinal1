using Microsoft.AspNetCore.Mvc;
using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Shared.Interfaces;

namespace BibliotecaMovil.Server.Controllers;

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
}
