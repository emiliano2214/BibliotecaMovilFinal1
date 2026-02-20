using Microsoft.AspNetCore.Mvc;
using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Server.Repositories;

namespace BibliotecaMovil.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SancionController : ControllerBase
{
    private readonly ISancionRepository _sancionRepository;

    public SancionController(ISancionRepository sancionRepository)
    {
        _sancionRepository = sancionRepository;
    }

    [HttpGet("prestamo/{prestamoId}")]
    public async Task<ActionResult<List<SancionDto>>> GetSancionesByPrestamo(int prestamoId)
    {
        return await _sancionRepository.GetSancionesByPrestamoIdAsync(prestamoId);
    }
}
