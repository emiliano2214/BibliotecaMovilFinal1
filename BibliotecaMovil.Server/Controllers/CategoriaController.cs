using Microsoft.AspNetCore.Mvc;
using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Server.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace BibliotecaMovil.Server.Controllers;
[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/[controller]")]
public class CategoriaController : ControllerBase
{
    private readonly ICategoriaRepository _categoriaRepository;

    public CategoriaController(ICategoriaRepository categoriaRepository)
    {
        _categoriaRepository = categoriaRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<CategoriaDto>>> GetCategorias()
    {
        return await _categoriaRepository.GetAllCategoriasAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoriaDto>> GetCategoria(int id)
    {
        var categoria = await _categoriaRepository.GetCategoriaByIdAsync(id);
        if (categoria == null) return NotFound();
        return categoria;
    }
}
