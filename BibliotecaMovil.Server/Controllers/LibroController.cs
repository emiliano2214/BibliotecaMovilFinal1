using Microsoft.AspNetCore.Mvc;
using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Server.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace BibliotecaMovil.Server.Controllers;

[Authorize(Roles = "Lector,Admin,Bibliotecario")]
[ApiController]
[Route("api/[controller]")]
public class LibroController : ControllerBase
{
    private readonly ILibroRepository _LibroRepository;

    public LibroController(ILibroRepository LibroRepository)
    {
        _LibroRepository = LibroRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<LibroDto>>> GetLibros()
    {
        return await _LibroRepository.GetAllLibrosAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LibroDto>> GetLibro(int id)
    {
        var Libro = await _LibroRepository.GetLibroByIdAsync(id);
        if (Libro == null) return NotFound();
        return Libro;
    }

    [HttpGet("categoria/{categoriaId}")]
    public async Task<ActionResult<List<LibroDto>>> GetLibrosByCategoria(int categoriaId)
    {
        return await _LibroRepository.GetLibrosByCategoriaAsync(categoriaId);
    }
}
