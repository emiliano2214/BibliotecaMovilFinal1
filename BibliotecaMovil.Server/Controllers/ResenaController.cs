using Microsoft.AspNetCore.Mvc;
using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Shared.Interfaces;

namespace BibliotecaMovil.Server.Controllers;

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
        return await _resenaRepository.GetResenasByLibroIdAsync(libroId);
    }

    [HttpPost]
    public async Task<IActionResult> CreateResena([FromBody] ResenaDto resena)
    {
        var result = await _resenaRepository.CreateResenaAsync(resena);
        if (result) return Ok();
        return BadRequest();
    }
}
