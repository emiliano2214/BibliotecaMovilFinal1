using Microsoft.AspNetCore.Mvc;
using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Server.Repositories;

namespace BibliotecaMovil.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolController : ControllerBase
{
    private readonly IRolRepository _rolRepository;

    public RolController(IRolRepository rolRepository)
    {
        _rolRepository = rolRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<RolDto>>> GetRoles()
    {
        return await _rolRepository.GetAllRolesAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RolDto>> GetRol(int id)
    {
        var rol = await _rolRepository.GetRolByIdAsync(id);
        if (rol == null) return NotFound();
        return rol;
    }
}
