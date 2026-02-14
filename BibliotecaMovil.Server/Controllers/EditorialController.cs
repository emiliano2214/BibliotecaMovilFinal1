using BibliotecaMovil.Server.Repositories;
using BibliotecaMovil.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaMovil.Server.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/[controller]")]
public class EditorialController : ControllerBase
{
    private readonly IEditorialRepository _editorialRepository;

    public EditorialController(IEditorialRepository editorialRepository)
    {
        _editorialRepository = editorialRepository;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<EditorialDto>>> GetEditoriales()
    {
        return await _editorialRepository.GetAllEditorialesAsync();
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<EditorialDto>> GetEditorial(int id)
    {
        var editorial = await _editorialRepository.GetEditorialByIdAsync(id);
        if (editorial == null) return NotFound();
        return editorial;
    }
}
