using BibliotecaMovil.Server.Repositories;
using BibliotecaMovil.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaMovil.Server.Controllers;
[Authorize(Roles = "Lector,Admin")]
[ApiController]
[Route("api/[controller]")]
public class ResenaController : ControllerBase
{
    private readonly IResenaRepository _resenaRepository;

    public ResenaController(IResenaRepository resenaRepository)
    {
        _resenaRepository = resenaRepository;
    }

    [HttpGet("libro/{libroId}")]
    public async Task<ActionResult<List<ResenaDto>>> GetResenasByLibro(int libroId)
    {
        var resenas = await _resenaRepository.GetResenasByLibroIdAsync(libroId);
        return Ok(resenas);
    }

    [HttpPost]
    public async Task<IActionResult> CreateResena([FromBody] ResenaDto resena)
    {
        var ok = await _resenaRepository.CreateResenaAsync(resena);
        return ok ? Ok() : BadRequest();
    }
}
