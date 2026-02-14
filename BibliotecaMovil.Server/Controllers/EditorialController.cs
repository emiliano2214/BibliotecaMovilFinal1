using Microsoft.AspNetCore.Mvc;
using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Server.Repositories;

namespace BibliotecaMovil.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EditorialController : ControllerBase
{
    private readonly IEditorialRepository _editorialRepository;

    public EditorialController(IEditorialRepository editorialRepository)
    {
        _editorialRepository = editorialRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<EditorialDto>>> GetEditoriales()
    {
        return await _editorialRepository.GetAllEditorialesAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EditorialDto>> GetEditorial(int id)
    {
        var editorial = await _editorialRepository.GetEditorialByIdAsync(id);
        if (editorial == null) return NotFound();
        return editorial;
    }
}
