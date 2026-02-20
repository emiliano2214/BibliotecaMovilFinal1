using Microsoft.AspNetCore.Mvc;
using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Server.Repositories;

namespace BibliotecaMovil.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EjemplarController : ControllerBase
{
    private readonly IEjemplarRepository _ejemplarRepository;

    public EjemplarController(IEjemplarRepository ejemplarRepository)
    {
        _ejemplarRepository = ejemplarRepository;
    }

    [HttpGet("libro/{libroId}")]
    public async Task<ActionResult<List<EjemplarDto>>> GetEjemplaresByLibro(int libroId)
    {
        return await _ejemplarRepository.GetEjemplaresByLibroIdAsync(libroId);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EjemplarDto>> GetEjemplar(int id)
    {
        var ejemplar = await _ejemplarRepository.GetEjemplarByIdAsync(id);
        if (ejemplar == null) return NotFound();
        return ejemplar;
    }
}
