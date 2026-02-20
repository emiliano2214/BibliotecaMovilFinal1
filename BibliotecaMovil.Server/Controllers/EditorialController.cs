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

    [HttpGet]
    public async Task<ActionResult<List<EditorialDto>>> GetEditoriales()
        => await _editorialRepository.GetAllEditorialesAsync();

    [HttpGet("{id:int}")]
    public async Task<ActionResult<EditorialDto>> GetEditorial(int id)
    {
        var editorial = await _editorialRepository.GetEditorialByIdAsync(id);
        if (editorial == null) return NotFound();
        return editorial;
    }

    // ✅ CREATE
    [HttpPost]
    public async Task<ActionResult<EditorialDto>> Create([FromBody] EditorialDto dto)
    {
        // Repositorio debe crear y devolver el creado (idealmente con Id)
        var created = await _editorialRepository.CreateEditorialAsync(dto);

        // Si tu DTO tiene IdEditorial:
        return CreatedAtAction(nameof(GetEditorial), new { id = created.IdEditorial }, created);
    }

    // ✅ UPDATE
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] EditorialDto dto)
    {
        var ok = await _editorialRepository.UpdateEditorialAsync(id, dto);
        if (!ok) return NotFound();

        return NoContent(); // 204
    }
}
